var departureRowHtml = '<tr class="{context}">' +
    '<td class="status-body">{status}</td>' +
    '<td>{destination}</td>' +
    '<td>{due}</td>' +
    '<td>{expected}</td>' +
    '<td>{platform}</td>' +
    '<td class="operator-body">{operator}</td>' +
    '{reason}'
    '</tr>';

var noTrains = '<div class="col-md-1"></div>' +
    '<div class="col-md-10">' +
    '<h4>There doesn\'t appear to be any trains running today...</h4>' +
    '<a id="search-again" href="/" class="btn btn-default">Search again</a>' +
    '</div>' +
    '<div class="col-md-1"></div>';

var searchAgainNoMessage = '<div class="col-md-12">' +
    '<a id="search-again" href="/" class="btn btn-default">Search again</a>' +
    '</div>';

var warningIcon = '<i class="fa fa-exclamation-circle" aria-hidden="true"></i>';
var alertIcon = '<i class="fa fa-exclamation-triangle" aria-hidden="true"></i>';
var allGoodIcon = '<i class="fa fa-check-circle-o" aria-hidden="true"></i>';
var loadingIcon = '<i class="fa fa-cog fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span>';

var reasonInput = '<input type="hidden" value="{reason-string}" />'

$(function () {
    $('#departures').hide();
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
    $(document).on('click', '#departures-table > tbody > tr.danger, #departures-table > tbody > tr.warning', showComplainModal);
});

function showComplainModal() {
    var row = $(this);
    $('#complain-modal').modal();
    $('#complain-modal').show();
}

function submitStation() {
    var selectedStation = $('#selected-station').val();

    if (selectedStation == '') {
        // Catch this and show a popover
    }

    var url = $(this).data('url');

    var stationSearch = $("#station-search");
    $(stationSearch).empty();
    $(stationSearch).append(loadingIcon);


    $.get(url, { selectedStation: selectedStation }, function (data) {
        var departuresSection = $('#departures');
        var departuresTable = $('#departures-table > tbody');

        if (data == null || data.departureModels == null) {
            $(stationSearch).hide();
            $(departureSection).clear().append(noTrains);
        }
        else {
            $(stationSearch).hide();
            $.each(data.departureModels, function (index, value) {
                var cancellationReasonPresent = value.cancellationReason == null ? false : true;
                var delayReasonPresent = value.delayReason == null ? false : true;
                var trainOnTime = value.expected == 'On time' ? true : false;
                var row = '';

                if (trainOnTime) {
                    row = departureRowHtml.replace('{context}', 'success').replace('{status}', allGoodIcon);
                }
                else if (cancellationReasonPresent || delayReasonPresent) {
                    if (!cancellationReasonPresent && delayReasonPresent) {
                        reasonInput = reasonInput.replace('{reason-string}', value.delayReason);
                    }
                    else {
                        reasonInput = reasonInput.replace('{reason-string}', value.cancellationReason);
                    }
                    
                    row = departureRowHtml.replace('{context}', 'danger').replace('{status}', warningIcon).replace('{reason}', reasonInput);
                }
                else {
                    row = departureRowHtml.replace('{context}', 'warning').replace('{status}', alertIcon);
                }

                if (value.platform == null) {
                    value.platform = '';
                }

                var departure = row.replace('{destination}', value.destinationName).replace('{due}', value.due).replace('{expected}', value.expected).replace('{platform}', value.platform).replace('{operator}', value.operator);

                $(departuresTable).append(departure);
            });

            $(departuresSection).append(searchAgainNoMessage);
            $(departuresSection).show();
        }
    });
}