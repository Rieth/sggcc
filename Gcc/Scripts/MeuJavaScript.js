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

    //$('.add').click(function () {
    //    alert("OI");
    //    var url = $(this).attr("data-url");
    //    var table = $(this).attr("data-table");
    //    var tableBody = $("#"+table+" tbody");
        
    //    $.ajax({
    //        url: url,
    //        type: 'GET',
    //        datatype: 'html',
    //        success: function (data) {
    //            tableBody.append(data);
    //        },
    //        error: function () {
    //        }
    //    });
    //});
});