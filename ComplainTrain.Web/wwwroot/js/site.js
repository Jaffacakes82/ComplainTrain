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
    '<label>Platform:</label>' +
    '<span>{platform}</span>' +
    '</li>' +
    '</li>' +
    '<li class="list-group-item">' +
    '<label>Operator:</label>' +
    '<span>{operator}</span>' +
    '</li>' +
    '</ul>' +
    '</div>' +
    '</div>';

var noTrains = '<div class="col-md-1"></div>' +
    '<div class="col-md-10">' +
    '<h4>There doesn\'t appear to be any trains running today...</h4>' +
    '<a id="search-again" href="/" class="btn btn-default">Search again</a>' +
    '</div>' +
    '<div class="col-md-1"></div>';

var searchAgainNoMessage = '<div class="col-md-12">' +
    '<a id="search-again" href="/" class="btn btn-default">Search again</a>' +
    '</div>';

var warningIcon = 'Warning <i class="fa fa-exclamation-circle" aria-hidden="true"></i>';
var alertIcon = 'Alert <i class="fa fa-exclamation-triangle" aria-hidden="true"></i>';
var allGoodIcon = 'All good! <i class="fa fa-check-circle-o" aria-hidden="true"></i>';
var loadingIcon = '<i class="fa fa-cog fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span>';

$(function () {
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

    var stationSearch = $("#station-search");
    $(stationSearch).empty();
    $(stationSearch).append(loadingIcon);


    $.get(url, { selectedStation: selectedStation }, function (data) {
        var departureSection = $('#departures');
        var departureBoard = "";

        if (data == null || data.departureModels == null) {
            $(stationSearch).hide();
            $(departureSection).append(noTrains);
        }
        else {
            $(stationSearch).hide();
            $.each(data.departureModels, function (index, value) {
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

                var departureBoard = boardPanel.replace('{destination}', value.destinationName).replace('{due}', value.due).replace('{expected}', value.expected).replace('{platform}', value.platform).replace('{operator}', value.operator);

                $(departureSection).append(departureBoard);
            })

            $(departureSection).append(searchAgainNoMessage);
        }
    });
}