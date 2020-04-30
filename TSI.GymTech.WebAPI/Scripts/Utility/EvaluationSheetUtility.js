// Load Datatable to Index page with ajax and controller 
// Create structure to DataTable show entries, search and export buttons
function LoadEvaluationSheetsDataTable(orderingStatus) {
    var formAction = $("form").attr("action");
    var urlBase = formAction.substr(0, formAction.indexOf('EvaluationSheet'));

    var table = $(tblEvaluationSheets).DataTable({
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
            url: urlBase + "EvaluationSheet/GetEvaluationSheets",
            type: "GET",
            dataType: "json"
        },
        columns: [
            {
                data: "Description", autowidth: true, render: function (data, type, full, meta) {
                    return '<a href=' + urlBase + 'EvaluationSheet/Edit/' + full.Id + '>' + data + '</a>';
                }
            },
            {
                data: "StudentName", title: "Aluno", autowidth: true, render: function (data, type, full, meta) {
                    return '<a href=' + urlBase + 'Student/Edit/' + full.StudentId + '>' + data + '</a>';
                }
            },
            { data: "Revaluation", autowidth: true },
            { data: "Comments", autowidth: true },
            {
                data: null, render: function (data, type, full, meta) {
                    return "<a href=" + urlBase + "EvaluationSheet/Edit/" + full.Id + ">" +
                        "<i class='fas fa-edit'></i>" +
                        "</a>";
                }
            },
            {
                data: null, render: function (data, type, full, meta) {
                    return "<a href='#' onClick='DeleteEvaluationSheet(&apos;" + full.Id + "&apos;, &apos;" + full.StudentName + "&apos;, &apos;tblEvaluationSheets&apos;);'>" +
                        "<i style='color: red;' class='fas fa-trash-alt'></i>" +
                        "</a >";
                }
            }
        ]
    });

    $.fn.DataTable.ext.pager.numbers_length = 3;
    table.buttons().container()
        .appendTo('#' + tblEvaluationSheets.id + '_wrapper .col-md-6:eq(0)');
}

// Open the modal popup to select a Student
$(function () {
    $(".select-student-evaluationsheet").click(function (ev) {
        if ($("#modalIsLoad").val() == 'False') {
            var formAction = $("form").attr("action");
            var url = formAction.substr(0, formAction.indexOf('EvaluationSheet')) + 'Student/Select';

            $("#modalIsLoad").val('True');
            $("#modal").load(url);
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

$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        if (o[this.name] !== undefined) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
};

// Showing toastr success alert
$("#btnSaveEvaluationSheet").click(function () {
    //var formData = $("#frmEvaluationSheet").serialize();-
    var url = $("#frmEvaluationSheet").attr("action");

    event.preventDefault();
    $('#btnSaveEvaluationSheet').attr('disabled', 'disabled');

    var sheetEvaluationAnswer = new Array();
    var sheetAnamnesisAnswer = new Array();

    $("input[data-val='SheetEvaluationAnswer']").each(function () {
        if ($(this).attr("type") == "text" || ($(this).attr("type") == "radio" && $(this).prop("checked"))) {
            sheetEvaluationAnswer.push(
                {
                    key: parseInt($(this).attr("id")),
                    value: $(this).val()
                });
        }
    });

    $("input[data-val='SheetAnamnesisAnswer']").each(function () {
        if ($(this).attr("type") == "text" || ($(this).attr("type") == "radio" && $(this).prop("checked"))) {
            sheetAnamnesisAnswer.push(
                {
                    key: parseInt($(this).attr("id")),
                    value: $(this).val()
                });
        }
    });
                     
    var formModel = $('#frmEvaluationSheet').serializeObject();
    formModel['SheetEvaluationAnswer'] = sheetEvaluationAnswer;
    formModel['SheetAnamnesisAnswer'] = sheetAnamnesisAnswer;
                                            
    $.ajax({
        type: "POST",
        url: url,
        data: formModel,
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
                if ($("#StudentId").val() == "") {
                    toastr.error(data.Message);
                }
            }
        },
        error: function () {
            toastr.error('Não foi possível atualizar o cadastro.');
        },
        complete: function () {
            $('#btnSaveEvaluationSheet').removeAttr('disabled');
        }
    })

    return false;
});

// Changing the SheetAnswer
function UpdateSheetAnwser(element) {
    var formAction = $("form").attr("action");
    var url = formAction.substr(0, formAction.indexOf('EvaluationSheet')) + 'EvaluationSheet/UpdateSheetAnswer';

    var token = $('input[name=__RequestVerificationToken]').val();
    var tokenadr = $('form[action="/User"] input[name=__RequestVerificationToken]').val();
    var headers = {};
    var headersadr = {};
    headers['__RequestVerificationToken'] = token;
    headersadr['__RequestVerificationToken'] = tokenadr;

    var sheetAnswerId = parseInt($(element).attr("id"));
    var newAnswer = $(element).val();
    var answerType = $(element).attr('data-val');

    $.ajax({
        type: "POST",
        dataType: "json",
        headers: headersadr,
        url: url,
        data: {
            __RequestVerificationToken: token,
            sheetAnswerId: sheetAnswerId,
            newAnswer: newAnswer,
            answerType: answerType
        },
        success: function (data) {
            if (data.success == true) {
                toastr.success(data.Message);
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

// Delete EvaluationSheet and showing toastr remove alert
function DeleteEvaluationSheet(evaluationSheetId, evaluationSheetName, tableName) {
    var formAction = $("form").attr("action");
    var url = formAction.substr(0, formAction.indexOf('Student')) + 'EvaluationSheet/Delete/' + evaluationSheetId;

    var token = $('input[name=__RequestVerificationToken]').val();
    var tokenadr = $('form[action="/User"] input[name=__RequestVerificationToken]').val();
    var headers = {};
    var headersadr = {};
    headers['__RequestVerificationToken'] = token;
    headersadr['__RequestVerificationToken'] = tokenadr;

    if (confirm('Tem certeza que deseja remover a Avaliação "' + evaluationSheetName + '"?')) {
        $.ajax({
            type: "POST",
            dataType: "json",
            headers: headersadr,
            url: url,
            data: {
                __RequestVerificationToken: token,
                id: evaluationSheetId
            },
            success: function (data) {
                if (data.Type == 'Success') {
                    toastr.success(data.Message);
                    RemoveDataTableRow(tableName, evaluationSheetId);
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

//// Select Person and close popup modal
//function SelectTrainingSheetToCopy(trainingSheetId) {
//    var personId = $("#PersonId").val();
//    var url = $("#frmTrainingSheet").attr("action");
//    var token = $('input[name=__RequestVerificationToken]').val();
//    var tokenadr = $('form[action="' + url + '"] input[name=__RequestVerificationToken]').val();
//    var headers = {};
//    var headersadr = {};
//    headers['__RequestVerificationToken'] = token;
//    headersadr['__RequestVerificationToken'] = tokenadr;

//    $.ajax({
//        type: "POST",
//        dataType: "json",
//        headers: headersadr,
//        url: url,
//        data: {
//            __RequestVerificationToken: token,
//            trainingSheetId: trainingSheetId,
//            personId: personId
//        },
//        success: function (data) {
//            if (data.Success) {
//                toastr.success(data.Message);
//                AddDataTableRow('tblTrainingSheet', data.Model)
//            }
//            else {
//                toastr.error(data.Message);
//            }
//        },
//        error: function (data) {
//            toastr.error(data.Message);
//        },
//        complete: function () {
//            $('#modalTraining').modal('toggle');
//        }
//    });
//}

//// Print Training Sheet
//function PrintTrainingSheet(element, formName, formNameAction) {
//    if (!formNameAction) {
//        formNameAction = formName;
//    }
//    var formAction = $("form").attr("action");
//    var url = formAction.substr(0, formAction.indexOf(formNameAction)) + formName + '/Print/' + $(element).attr("data-id");

//    $("#modal-print").load(url, function () {
//        $("#modal-print").modal();
//    });
//    return false;
//}