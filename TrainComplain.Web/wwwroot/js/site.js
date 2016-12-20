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
            $("#SelectedStation").val(ui.item.key);
        }
    });
});