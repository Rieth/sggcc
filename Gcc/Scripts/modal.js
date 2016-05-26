$(document).on('click', '.openModal', function () {
    var path = $(this).attr("data-path");
    $("#modal").load(path, function () {
        $("#modal").modal();
    })
});

//$(function () {
//    $(".openModal").click(function () {
//        var path = $(this).attr("data-path");
//        $("#modal").load(path, function () {
//            $("#modal").modal();
//        })
//    });
//});