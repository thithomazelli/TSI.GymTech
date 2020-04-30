// Load Datatable to Index page with ajax and controller 
// Create structure to DataTable show entries, search and export buttons
function LoadExerciseDataTable(orderingStatus) {
    var formAction = $("form").attr("action");
    var urlBase = formAction.substr(0, formAction.indexOf('Exercise'));

    var table = $(tblExercises).DataTable({
        language: {
            url: urlBase + 'Scripts/Utility/i18n/Portuguese-Brasil.json',
            search: '<div class="input-group col-md-12">' +
                ' _INPUT_ ' +
                '<span class= "input-group-append">' +
                '<button class="input-group-text btn btn-primary btn-dataTable-fixMargin" type="button">' +
                '<i class="fa fa-search"></i>' +
                '</button>' +
                '</span>' +
                '</div> ',
            searchPlaceholder: 'Pesquisar por...'
        },
        ordering: orderingStatus == null || orderingStatus == undefined ? true : orderingStatus,
        pagingType: 'simple_numbers',
        lengthChange: true,
        rowId: "Id",
        responsive: true,
        ajax: {
            url: urlBase + "Exercise/GetExercises",
            type: "GET",
            dataType: "json"
        },
        columns: [
            {
                data: "Name", autowidth: true, render: function (data, type, full, meta) {
                    return '<a href=' + urlBase + 'Exercise/Edit/' + full.Id + '>' + data + '</a>';
                }
            },
            { data: "Description", autowidth: true },
            { data: "Comments", autowidth: true },
            { data: "MuscleWorked", autowidth: true },
            { data: "MuscularGroup", autowidth: true },
            {
                data: null, render: function (data, type, full, meta) {
                    return "<a href=" + urlBase + "Exercise/Edit/" + full.Id + ">" +
                        "<i class='fas fa-edit'></i>" +
                        "</a>";
                }
            },
            {
                data: null, render: function (data, type, full, meta) {
                    return "<a href='#' onClick='DeleteExercise(&apos;" + full.Id + "&apos;, &apos;" + full.Name + "&apos;, &apos;tblExercises&apos;);'>" +
                        "<i style='color: red;' class='fas fa-trash-alt'></i>" +
                        "</a >";
                }
            }
        ]
    });

    $.fn.DataTable.ext.pager.numbers_length = 3;
    table.buttons().container()
        .appendTo('#' + tblExercises.id + '_wrapper .col-md-6:eq(0)');
}

// Showing toastr success alert
$("#btnSaveExercise").click(function () {
    var formData = $("#frmExercise").serialize();
    event.preventDefault();
    $('#btnSaveExercise').attr('disabled', 'disabled');
    var url = $("#frmExercise").attr("action");

    $.ajax({
        url: url,
        type: "POST",
        data: formData,
        dataType: "json",
        success: function (data) {
            if (data.Success) {
                if (data.Id) {
                    window.location.href = url.replace('/Create', '') + '/Edit/' + data.Id;
                }
                toastr.success(data.Message);
            }
            else {
                DisplayValidationErrors(data.Errors)
            }
        },
        error: function () {
            toastr.error('Não foi possível atualizar o cadastro.');
        },
        complete: function () {
            $('#btnSaveExercise').removeAttr('disabled');
        }
    })

    return false;
});

// Delete Exercise and showing toastr remove alert
function DeleteExercise(exerciseId, exerciseName, tableName) {
    var formAction = $("form").attr("action");
    var url = formAction.substr(0, formAction.indexOf('Exercise')) + 'Exercise/Delete';

    var token = $('input[name=__RequestVerificationToken]').val();
    var tokenadr = $('form[action="/Exercise"] input[name=__RequestVerificationToken]').val();
    var headers = {};
    var headersadr = {};
    headers['__RequestVerificationToken'] = token;
    headersadr['__RequestVerificationToken'] = tokenadr;

    if (confirm('Tem certeza que deseja o Exercício "' + exerciseName + '"?')) {
        $.ajax({
            type: "POST",
            dataType: "json",
            headers: headersadr,
            url: url,
            data: {
                __RequestVerificationToken: token,
                id: exerciseId
            },
            success: function (data) {
                if (data.Type == 'Success') {
                    toastr.success(data.Message);
                    RemoveDataTableRow(tableName, exerciseId);
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

// Code to upload, taking snapshot and remove image photo 
$(function () {
    $('#btnUpload').on('change', function () {
        reset_webcam();

        var selectedFile = event.target.files[0];
        var reader = new FileReader();

        reader.onload = function () {
            var formAction = $("form").attr("action");
            var baseUrl = formAction.substr(0, formAction.indexOf('Exercise'));
            var url = baseUrl + 'Exercise/CapturePhoto';

            var file = reader.result;
            var extension = $('#btnUpload').val().split('.').pop();

            if (extension != null && ValidateImage(extension)) {
                var id = $('#ExerciseId').val();
                var token = $('input[name=__RequestVerificationToken]').val();
                var tokenadr = $('form[action="/Exercise/Edit/' + id + '] input[name=__RequestVerificationToken]').val();
                var headers = {};
                var headersadr = {};
                headers['__RequestVerificationToken'] = token;
                headersadr['__RequestVerificationToken'] = tokenadr;

                $.ajax({
                    type: "POST",
                    dataType: "json",
                    headers: headersadr,
                    url: url,
                    data: {
                        __RequestVerificationToken: token,
                        id: id,
                        base64image: file,
                        fileExtension: extension
                    },
                    success: function (data) {
                        if (data.Type == 'Success') {
                            toastr.success(data.Message);
                            $("#btnRemovePhoto").show();
                            ReloadPhoto(data.ImageName, 'exercisePhoto', baseUrl + 'Images/Exercises/');
                        }
                        else if (data.Type == 'Error') {
                            toastr.error(data.Message);
                        }
                    },
                    error: function () {
                        toastr.error('Não foi possível remover a imagem.');
                    }
                });
            }
        };

        reader.readAsDataURL(selectedFile);
    });
    $('#btnSavePhoto').on('click', function () {
        var formAction = $("form").attr("action");
        var baseUrl = formAction.substr(0, formAction.indexOf('Exercise'));
        var url = baseUrl + 'Exercise/CapturePhoto';

        var file = $("#base64image").attr('src');
        var id = $("#ExerciseId").val();

        var token = $('input[name=__RequestVerificationToken]').val();
        var tokenadr = $('form[action="/Exercise/Edit/' + id + '] input[name=__RequestVerificationToken]').val();
        var headers = {};
        var headersadr = {};
        headers['__RequestVerificationToken'] = token;
        headersadr['__RequestVerificationToken'] = tokenadr;

        $.ajax({
            type: "POST",
            dataType: "json",
            headers: headersadr,
            url: url,
            data: {
                __RequestVerificationToken: token,
                id: id,
                base64image: file,
                fileExtension: 'jpg'
            },
            success: function (data) {
                if (data.Type == 'Success') {
                    toastr.success(data.Message);
                    $("#btnRemovePhoto").show();
                    ReloadPhoto(data.ImageName, 'exercisePhoto', baseUrl + 'Images/Exercises/');
                }
                else {
                    toastr.error(data.Message);
                }
            },
            error: function () {
                toastr.error('Não foi possível remover a imagem.');
            }
        });
    });
    $('#btnRemovePhoto').on('click', function () {
        var formAction = $("form").attr("action");
        var baseUrl = formAction.substr(0, formAction.indexOf('Exercise'));
        var url = baseUrl + 'Exercise/RemovePhoto';

        var file = $('#exercisePhoto').attr('src');

        if (file != null && file.toString().indexOf("default-profile.png") <= 0) {
            var id = $("#ExerciseId").val();
            var token = $('input[name=__RequestVerificationToken]').val();
            var tokenadr = $('form[action="/Exercise/Edit/' + id + '] input[name=__RequestVerificationToken]').val();
            var headers = {};
            var headersadr = {};
            headers['__RequestVerificationToken'] = token;
            headersadr['__RequestVerificationToken'] = tokenadr;

            if (confirm('Tem certeza que deseja remover a imagem?')) {
                $.ajax({
                    type: "POST",
                    dataType: "json",
                    headers: headersadr,
                    url: url,
                    data: {
                        __RequestVerificationToken: token,
                        id: id
                    },
                    success: function (data) {
                        if (data.Type == 'Success') {
                            toastr.success(data.Message);
                            $("#btnRemovePhoto").hide();
                            ReloadPhoto('default-profile.png', 'exercisePhoto', baseUrl + 'Images/Exercises/');
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
        else {
            toastr.error('O Usuário não possui foto para ser removida.');
        }

    });
});

$(document).ready(function ($) {
    $("#frmExercise").bootstrapValidator({});
});
