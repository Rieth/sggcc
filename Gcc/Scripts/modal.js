$(document).ready(function () {
    //$(document).on('click', '.open-modal', function () {
    //    var path = $(this).attr("data-path");
    //    $("#modal").load(path, function () {
    //        $("#modal").modal();
    //    })
    //});

    $(document).on('click', '.open-modal', function () {
        var path = $(this).attr("data-path");
        var modalDiv = $(this).parents('div').find('#modal');

        modalDiv.load(path, function () {
            modalDiv.modal();
        })
    });


    $(document).on('click', '.open-modal-next', function () {
        $(this).next('.my-modal').modal();
    });

});