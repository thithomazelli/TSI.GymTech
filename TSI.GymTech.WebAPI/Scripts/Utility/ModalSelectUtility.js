
// Select TrainingSheet and close popup modal
function SelectTrainingSheetToCopy(trainingSheetId) {
    var personId = $("#PersonId").val();
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
            $('#modalTraining').modal('toggle');
        }
    });
}

// Select Person and close popup modal
function SelectPerson(personId) {
    var url = $("#frmSelectPerson").attr("action") + '/' + personId;
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
