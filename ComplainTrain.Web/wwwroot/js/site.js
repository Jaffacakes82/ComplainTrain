var departureBoardHtml = '<div class="col-md-3">' +
'<div class="panel panel-{panelColour} departure-board">' +
'<div class="panel-heading">' +
'<h3 class="panel-title">{headingText}</h3>' +
'</div>' +
'<ul class="list-group">' +
'<li class="list-group-item">' +
'<label>Destination:</label>' +
'<span>{destination}</span>' +
'</li>' +
'<li class="list-group-item">' +
'<label>Due:</label>' +
'<span>{due}</span>' +
'</li>' +
'<li class="list-group-item">' +
'<label>Expected:</label>' +
'<span>{expected}</span>' +
'</li>' +
'<li class="list-group-item">' +
'<label>Operator:</label>' +
'<span>{operator}</span>' +
'</li>' +
'</ul>' + 
'</div>' +
'</div>';

var warningIcon = 'Warning <i class="fa fa-exclamation-circle" aria-hidden="true"></i>';
var alertIcon = 'Alert <i class="fa fa-exclamation-circle" aria-hidden="true"></i>';
var allGoodIcon = 'All good! <i class="fa fa-check-circle-o" aria-hidden="true"></i>';

// Write your Javascript code.
$(document).ready(function () {
    $('#station').autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/Home/SearchStations",
                dataType: "json",
                data: {
                    term: request.term
                },
                success: function (data) {
                    response(data);
                }
            });
        },
        minLength: 1,
        select: function (event, ui) {
            $("#station").text(ui.item.value);
            $("#selected-station").val(ui.item.key);
        }
    });

    $('#submit-station').click(submitStation);
});

function submitStation() {
    var selectedStation = $('#selected-station').val();
    var url = $(this).data('url');
    $.get(url, { selectedStation: selectedStation }, function (data) {
        var departureSection = $('#departures');
        var departureBoard = "";

        $.each(data.departureModels, function (index, value){
            var cancellationReasonPresent = value.cancellationReason == null ? false : true;
            var delayReasonPresent = value.delayReason == null ? false : true;
            var trainOnTime = value.expected == 'On time' ? true : false;
            var boardPanel = '';

            if (trainOnTime) {
                boardPanel = departureBoardHtml.replace('{panelColour}', 'success').replace('{headingText}', allGoodIcon);
            }
            else if (cancellationReasonPresent || delayReasonPresent) {
                boardPanel = departureBoardHtml.replace('{panelColour}', 'danger').replace('{headingText}', warningIcon);
            }
            else {
                boardPanel = departureBoardHtml.replace('{panelColour}', 'warning').replace('{headingText}', alertIcon);
            }
            
            var departureBoard = boardPanel.replace('{destination}', value.destinationName).replace('{due}', value.due).replace('{expected}', value.expected).replace('{operator}', value.operator);

            departureSection.append(departureBoard);
        })
    });
}