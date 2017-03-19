var departureRowHtml = '<tr class="{context}">' +
    '<td class="status-body">{status}</td>' +
    '<td class="destination-body">{destination}</td>' +
    '<td class="due-body">{due}<span class="expected-mob">{expected}</span></td>' +
    '<td class="expected-body">{expected}</td>' +
    '<td>{platform}</td>' +
    '<td class="operator-body">{operator}</td>' +
    '{reason}'
    '</tr>';

var warningIcon = '<i class="fa fa-exclamation-circle" aria-hidden="true"></i>';
var alertIcon = '<i class="fa fa-exclamation-triangle" aria-hidden="true"></i>';
var allGoodIcon = '<i class="fa fa-check-circle-o" aria-hidden="true"></i>';
var loadingIcon = '<i class="fa fa-cog fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span>';

var reasonInput = '<input class="delay-reason" type="hidden" value="{reason-string}" />';
var originalSearch = '';

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
            originalSearch = ui.item.value;
            $("#station").text(ui.item.value);
            $("#selected-station").val(ui.item.key);
        }
    });

    $('#submit-station').click(submitStation);
    $(document).on('click touchstart', '#departures-table > tbody > tr.danger, #departures-table > tbody > tr.warning', showComplainModal);
    $(document).on('click', '#complain-btn', postComplaint);
});

function postComplaint() {

}

function showComplainModal() {
    var complaintObj = extractComplaintInformation(this);
    var wording = complaintObj.expected == 'Cancelled' ? 'cancelled' : 'delayed';

    var body = '<p>You selected the ' + complaintObj.due + ' (' + complaintObj.expected + ') from ' + complaintObj.originalSearch + ' to ' + complaintObj.destination + ' operated by ' + complaintObj.operator + '.</p>' +
    '<p>' + complaintObj.delayReason.replace('{wording}', wording) + '</p>' +
    '<p>Think this is a good enough reason to complain?</p>';

    var modal = $('#complain-modal');
    $(modal).find('.modal-body').empty();
    $(modal).find('.modal-body').append(body);
    $(modal).modal();
    $(modal).show();

    return false;
}

function extractComplaintInformation(row) {
    var delayReason = '';
    var destination = $(row).find("td.destination-body").text();
    var expected = $(row).find("td.expected-body").text();
    var due = $(row).find("td.due-body").html();
    due = due.substr(0, due.indexOf('<'));
    var operator = $(row).find("td.operator-body").text();
    
    var complaintInfo = {};
    complaintInfo.originalSearch = originalSearch;
    complaintInfo.destination = destination;
    complaintInfo.due = due;
    complaintInfo.operator = operator;
    complaintInfo.expected = expected;

    if ($(row).has('input').length > 0) {
        delayReason = "Apparently this train is {wording} because '<strong>" + $(row).find('input').val() + "'</strong>."
    }
    else {
        delayReason = "It doesn't look like there's a formal reason why this train is delayed."
    }

    complaintInfo.delayReason = delayReason;
    return complaintInfo;
}

function submitStation() {
    var selectedStation = $('#selected-station').val();

    if (selectedStation == '') {
        $('#alert-no-station').show();
        return false;
    }

    var url = $(this).data('url');

    var stationSearch = $("#station-search");
    var searchAgain = $("#search-again");
    var noTrains = $("#no-trains");

    $(stationSearch).empty();
    $(stationSearch).append(loadingIcon);


    $.get(url, { selectedStation: selectedStation }, function (data) {
        var departuresSection = $('#departures');
        var body = $('.body-content');
        var departuresTable = $('#departures-table > tbody');

        if (data == null || data.departureModels == null) {
            $(stationSearch).hide();
            $(noTrains).show();
            $(searchAgain).show();
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

                var departure = row.replace('{destination}', value.destinationName).replace('{due}', value.due).replace('{expected}', value.expected).replace('{expected}', value.expected).replace('{platform}', value.platform).replace('{operator}', value.operator);

                $(departuresTable).append(departure);
            });

            $("#departures-note").text("Here are the departures from '" + originalSearch + "' in the next hour...");
            $(departuresSection).show();
            $(searchAgain).show();
        }
    });
}