// Load Datatable to Index page with ajax and controller 
// Create structure to DataTable show entries, search and export buttons
function LoadSheetQuestionDataTable(orderingStatus) {
    var formAction = $("form").attr("action");
    var urlBase = formAction.substr(0, formAction.indexOf('SheetQuestion'));

    var table = $(tblSheetQuestions).DataTable({
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
            url: urlBase + "SheetQuestion/GetSheetQuestions",
            type: "GET",
            dataType: "json"
        },
        columns: [
            {
                data: "Question", autowidth: true, render: function (data, type, full, meta) {
                    return '<a href=' + urlBase + 'SheetQuestion/Edit/' + full.Id + '>' + data + '</a>';
                }
            },
            { data: "QuestionType", autowidth: true },
            { data: "AnswerType", autowidth: true },
            { data: "Order", autowidth: true },
            {
                data: null, render: function (data, type, full, meta) {
                    return "<a href=" + urlBase + "SheetQuestion/Edit/" + full.Id + ">" +
                        "<i class='fas fa-edit'></i>" +
                        "</a>";
                }
            },
            {
                data: null, render: function (data, type, full, meta) {
                    return "<a href='#' onClick='DeleteSheetQuestion(&apos;" + full.Id + "&apos;, &apos;" + full.Question + "&apos;, &apos;tblSheetQuestions&apos;);'>" +
                        "<i style='color: red;' class='fas fa-trash-alt'></i>" +
                        "</a >";
                }
            }
        ]
    });

    $.fn.DataTable.ext.pager.numbers_length = 3;
    table.buttons().container()
        .appendTo('#' + tblSheetQuestions.id + '_wrapper .col-md-6:eq(0)');
}

// Showing toastr success alert
$("#btnSaveSheetQuestion").click(function () {
    var formData = $("#frmSheetQuestion").serialize();
    var url = $("#frmSheetQuestion").attr("action");
    $('#btnSaveSheetQuestion').attr('disabled', 'disabled');

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
                DisplayValidationSuccess();
                toastr.success(data.Message);
            }
            else {
                DisplayValidationErrors(data.Errors);
            }
        },
        error: function () {
            toastr.error('Não foi possível atualizar o cadastro.');
        },
        complete: function () {
            $('#btnSaveSheetQuestion').removeAttr('disabled');
        }
    })
});

// Delete User and showing toastr remove alert
function DeleteSheetQuestion(sheetQuestionId, question, tableName) {
    var formAction = $("form").attr("action");
    var url = formAction.substr(0, formAction.indexOf('SheetQuestion')) + 'SheetQuestion/Delete';

    var token = $('input[name=__RequestVerificationToken]').val();
    var tokenadr = $('form[action="/SheetQuestion"] input[name=__RequestVerificationToken]').val();
    var headers = {};
    var headersadr = {};
    headers['__RequestVerificationToken'] = token;
    headersadr['__RequestVerificationToken'] = tokenadr;

    if (confirm('Tem certeza que deseja remover a Questão "' + question + '"?')) {
        $.ajax({
            type: "POST",
            dataType: "json",
            headers: headersadr,
            url: url,
            data: {
                __RequestVerificationToken: token,
                id: sheetQuestionId
            },
            success: function (data) {
                if (data.Type == 'Success') {
                    toastr.success(data.Message);
                    RemoveDataTableRow(tableName, sheetQuestionId);
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
