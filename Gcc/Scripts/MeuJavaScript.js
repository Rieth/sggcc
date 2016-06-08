$(document).ready(function () {
    $('.tabs a:first').tab('show');

    //$(document).on('click', '.add', function () {
    //    var url = $(this).attr("data-url");
    //    var dataDivName = $(this).attr("data-divname");
    //    var dataElement = $("." + dataDivName);

    //    $.ajax({
    //        url: url,
    //        type: 'GET',
    //        datatype: 'html',
    //        success: function (data) {
    //            dataElement.append(data);
    //        },
    //        error: function () {
    //        }
    //    });
    //});
    $(document).on('click', '.add', function () {
        var addButton = $(this);
        var url = $(this).attr("data-url");
        var dataDivName = $(this).attr("data-divname");
        var dataElement = $("." + dataDivName);

        $.ajax({
            url: url,
            type: 'GET',
            datatype: 'html',
            data: { "containerPrefix": addButton.data("containerprefix") },
            success: function (data) {
                dataElement.append(data);
            },
            error: function () {
            }
        });
    });

    $(document).on('click', '.delete', function () {
        var id = $(this).attr("data-id");
        var parent = $(this).parent('div').parent('div');
        var url = $(this).attr("data-url");

        $.ajax({
            type: 'POST',
            url: url,
            data: { id: id },
            success: function () {
                parent.remove();
                alert("success");
            },
            error: function (data) {
                alert(data);
            }
        });
    });

});