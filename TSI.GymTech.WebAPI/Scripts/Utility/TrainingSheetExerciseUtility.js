﻿// Create new Training Sheet Exercise
$(function () {
    $(".create-trainingsheet-exercise").click(function () {
        var id = $(this).attr("data-id");
        $("#modal").load("/TrainingSheetExercise/Create?trainingSheetId=" + id, function () {
            $("#modal").modal();
        });

        return false;
    });
})

// Edit new Training Sheet Exercise
function EditTrainingsheetExercise(element) {
    var id = $(element).attr("data-id");
    $("#modal").load("/TrainingSheetExercise/Edit/" + id, function () {
        $("#modal").modal();
    });

    return false;
}

//$(function () {
//    $(".edit-trainingsheet-exercise").click(function () {
//        var id = $(this).attr("data-id");
//        $("#modal").load("/TrainingSheetExercise/Edit/" + id, function () {
//            $("#modal").modal();
//        });

//        return false;
//    });
//})

// Delete TrainingSheetExercise and showing toastr remove alert
function DeleteTrainingSheetExercise(id, tableName, formName) {
    var token = $('input[name=__RequestVerificationToken]').val();
    var tokenadr = $('form[action="/' + formName + '"] input[name=__RequestVerificationToken]').val();
    var headers = {};
    var headersadr = {};
    headers['__RequestVerificationToken'] = token;
    headersadr['__RequestVerificationToken'] = tokenadr;

    if (confirm('Tem certeza que deseja remover o exercício selecionado?')) {
        $.ajax({
            type: "POST",
            dataType: "json",
            headers: headersadr,
            url: '/TrainingSheetExercise/Delete',
            data: {
                __RequestVerificationToken: token,
                id: id
            },
            success: function (data) {
                if (data.Type == 'Success') {
                    toastr.success(data.Message);
                    RemoveDataTableRow(tableName, id);
                }
                else {
                    toastr.error(data.Message);
                }
            },
            error: function (data) {
                toastr.error(data.Message);
            }
        });
    }
}