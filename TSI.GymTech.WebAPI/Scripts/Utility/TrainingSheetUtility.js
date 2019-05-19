// Enable or disable div with Student lookup and Revaluation date
$(document).ready(function () {
    EnableOrDisablePersonalContent();
});

$("#Model").change(function () {
    EnableOrDisablePersonalContent();
});

function EnableOrDisablePersonalContent() {
    if ($("#Model").val() == 0) {
        $("#personalContent").hide();
    }
    else {
        $("#personalContent").show();
    }
}

// Open the modal popup to select a Student
$(function () {
    $(".select-student-trainingsheet").click(function (ev) {
        if ($("#modalIsLoad").val() == 'False') {
            $("#modalIsLoad").val('True');
            $("#modal").load("/Student/Select");
        }
        $("#modal").modal({
            cache: false,
            backdrop: 'static',
            keyboard: false
        }, "show");
        ev.preventDefault();
        return false;
    });

})

// Select Person and close popup modal
function SelectPerson(personId) {
    var url = '/Student/Select/' + personId
    var token = $('input[name=__RequestVerificationToken]').val();
    var tokenadr = $('form[action="' + url + '"] input[name=__RequestVerificationToken]').val();
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
            personId: personId
        },
        success: function (data) {
            if (data.Success) {
                $('#StudentId').val(data.Id);
                $('#Student_Name').val(data.PersonName);
                toastr.success(data.Message);
            }
            else {
                toastr.error(data.Message);
            }
        },
        error: function (data) {
            toastr.error(data.Message);
        },
        complete: function () {
            $('#modal').modal('toggle');
        }
    });
}

// Showing toastr success alert
$("#btnSaveTrainingSheet").click(function () {
    var formData = $("#frmTrainingSheet").serialize();
    var url = $("#frmTrainingSheet").attr("action");

    event.preventDefault();
    $('#btnSaveTrainingSheet').attr('disabled', 'disabled');
    
    $.ajax({
        type: "POST",
        url: url,
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
                if ($("#Model").val() == 1 && $("#StudentId").val() == "") {
                    toastr.error(data.Message);
                }
            }
        },
        error: function () {
            toastr.error('Não foi possível atualizar o cadastro.');
        },
        complete: function () {
            $('#btnSaveTrainingSheet').removeAttr('disabled');
        }
    })

    return false;
});

// Delete User and showing toastr remove alert
function DeleteTrainingSheet(trainingSheetId, trainingSheetName, tableName) {
    var url = $("#frmStudent").attr("action");
    var token = $('input[name=__RequestVerificationToken]').val();
    var tokenadr = $('form[action="/User"] input[name=__RequestVerificationToken]').val();
    var headers = {};
    var headersadr = {};
    headers['__RequestVerificationToken'] = token;
    headersadr['__RequestVerificationToken'] = tokenadr;

    if (confirm('Tem certeza que deseja remover o Treino ' + trainingSheetName + '?')) {
        $.ajax({
            type: "POST",
            dataType: "json",
            headers: headersadr,
            url: '/TrainingSheet/Delete',
            data: {
                __RequestVerificationToken: token,
                id: trainingSheetId
            },
            success: function (data) {
                if (data.Type == 'Success') {
                    toastr.success(data.Message);
                    RemoveDataTableRow(tableName, trainingSheetId);
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

// Select Person and close popup modal
function SelectTrainingSheetToCopy(trainingSheetId, personId) {
    var url = $("#frmTrainingSheet").attr("action");
    var token = $('input[name=__RequestVerificationToken]').val();
    var tokenadr = $('form[action="' + url + '"] input[name=__RequestVerificationToken]').val();
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
            trainingSheetId: trainingSheetId,
            personId: personId
        },
        success: function (data) {
            if (data.Success) {
                toastr.success(data.Message);
                AddDataTableRow('tblTrainingSheet', data.Model)
            }
            else {
                toastr.error(data.Message);
            }
        },
        error: function (data) {
            toastr.error(data.Message);
        },
        complete: function () {
            $('#modal').modal('toggle');
        }
    });
}
