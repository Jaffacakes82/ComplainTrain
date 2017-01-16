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
            var departure = '<div class="col-md-3"><label>Cancellation reason:</label><span>' + value.cancellationReason + '</span>' + 
            '<label>Delay reason:</label><span>' + value.delayReason + '</span>' + 
            '<label>Destination:</label><span>' + value.destinationName + '</span>' + 
            '<label>Due:</label><span>' + value.due + '</span>' + 
            '<label>Expected:</label><span>' + value.expected + '</span>' + 
            '<label>Operator:</label><span>' + value.operator + '</span></div>'

            departureSection.append(departure);
        })
    });
}