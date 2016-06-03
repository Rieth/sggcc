$(document).ready(function () {
    $('.tabs a:first').tab('show');

    $(document).on('click', '.add', function () {
        var url = $(this).attr("data-url");
        var dataDivName = $(this).attr("data-divname");
        var dataElement = $("." + dataDivName);

        $.ajax({
            url: url,
            type: 'GET',
            datatype: 'html',
            success: function (data) {
                dataElement.append(data);
            },
            error: function () {
            }
        });
    });
});