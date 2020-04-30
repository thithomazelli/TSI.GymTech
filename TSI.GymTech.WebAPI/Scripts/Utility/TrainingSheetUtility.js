// Enable or disable div with Student lookup and Revaluation date
$(document).ready(function () {
    EnableOrDisablePersonalOptions();
    $("#frmTrainingSheet").bootstrapValidator({
    });
});

$("#Model").change(function () {
    EnableOrDisablePersonalOptions();
});

function EnableOrDisablePersonalOptions() {
    var personalOptionsStatus = false;

    if ($("#Model").val() == 0) {
        personalOptionsStatus = true;
        $("#Student_Name").val(null);
        $("#Revaluation").val(null);
    } 

    $(".select-student-trainingsheet").prop("disabled", personalOptionsStatus);
    $("#Revaluation").prop("disabled", personalOptionsStatus);
}

// Load Datatable to Index page with ajax and controller 
// Create structure to DataTable show entries, search and export buttons
function LoadTrainingSheetsDataTable(orderingStatus) {
    var formAction = $("form").attr("action");
    var urlBase = formAction.substr(0, formAction.indexOf('TrainingSheet'));

    var table = $(tblTrainingSheet).DataTable({
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
            url: urlBase + "TrainingSheet/GetTrainingSheets",
            type: "GET",
            dataType: "json"
        },
        columns: [
            {
                data: "Name", autowidth: true, render: function (data, type, full, meta) {
                    return '<a href=' + urlBase + 'TrainingSheet/Edit/' + full.Id + '>' + data + '</a>';
                }
            },
            { data: "Cycle", autowidth: true },
            {
                data: "StudentName", title: "Aluno", autowidth: true, render: function (data, type, full, meta) {
                    return '<a href=' + urlBase + 'Student/Edit/' + full.StudentId + '>' + data + '</a>';
                }
            },
            { data: "Model", autowidth: true },
            { data: "Status", autowidth: true },
            { data: "Type", autowidth: true },
            {
                data: null, render: function (data, type, full, meta) {
                    return "<a href='' onclick='PrintTrainingSheet(this, &apos;TrainingSheet&apos;); return false;' data-id=" + + full.Id + "> " +
                        "<i class='fas fa-print'></i>" +
                        "</a>";
                }
            },
            {
                data: null, render: function (data, type, full, meta) {
                    return "<a href=" + urlBase + "TrainingSheet/Edit/" + full.Id + ">" +
                        "<i class='fas fa-edit'></i>" +
                        "</a>";
                }
            },
            {
                data: null, render: function (data, type, full, meta) {
                    return "<a href='#' onClick='DeleteTrainingSheet(&apos;" + full.Id + "&apos;, &apos;" + full.Name + "&apos;, &apos;tblTrainingSheet&apos;);'>" +
                        "<i style='color: red;' class='fas fa-trash-alt'></i>" +
                        "</a >";
                }
            }
        ]
    });

    $.fn.DataTable.ext.pager.numbers_length = 3;
    table.buttons().container()
        .appendTo('#' + tblTrainingSheet.id + '_wrapper .col-md-6:eq(0)');
}

// Load Datatable to Index page with ajax and controller 
// Create structure to DataTable show entries, search and export buttons
function LoadDataTableToSelectTrainingSheet(element, orderingStatus) {
    var formAction = $("#frmStudent").attr("action");
    var urlBase = formAction.substr(0, formAction.indexOf('Student'));

    var table = $(element).DataTable({
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
        lengthMenu: [5, 10, 15, "Todos"],
        info: false,
        rowId: "Id",
        responsive: true,
        ajax: {
            url: urlBase + "TrainingSheet/GetTrainingSheets",
            type: "GET",
            dataType: "json"
        },
        columns: [
            { data: "Name", autowidth: true },
            { data: "Cycle", autowidth: true },
            { data: "StudentName", autowidth: true },
            {
                data: null, render: function (data, type, full, meta) {
                    return "<a href='#' onClick='SelectTrainingSheetToCopy(&apos;" + full.Id + "&apos;); return false;'>" +
                        "<i class='fas fa-check-circle'></i>" +
                        "</a >";
                }
            }
        ]
    });

    $.fn.DataTable.ext.pager.numbers_length = 3;
    table.buttons().container()
        .appendTo('#' + tblTrainingSheet.id + '_wrapper .col-md-6:eq(0)');
}

// Open the modal popup to select a Student
$(function () {
    $(".select-student-trainingsheet").click(function (ev) {
        if ($("#modalIsLoad").val() == 'False') {
            var formAction = $("form").attr("action");
            var url = formAction.substr(0, formAction.indexOf('TrainingSheet')) + 'Student/Select';
            
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
    var formAction = $("form").attr("action");
    var url = formAction.substr(0, formAction.indexOf('Student')) + 'TrainingSheet/Delete/' + trainingSheetId;

    var token = $('input[name=__RequestVerificationToken]').val();
    var tokenadr = $('form[action="/User"] input[name=__RequestVerificationToken]').val();
    var headers = {};
    var headersadr = {};
    headers['__RequestVerificationToken'] = token;
    headersadr['__RequestVerificationToken'] = tokenadr;

    if (confirm('Tem certeza que deseja remover o Treino "' + trainingSheetName + '"?')) {
        $.ajax({
            type: "POST",
            dataType: "json",
            headers: headersadr,
            url: url,
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

// Print Training Sheet
function PrintTrainingSheet(element, formName, formNameAction) {
    if (!formNameAction) {
        formNameAction = formName;
    }
    var formAction = $("form").attr("action");
    var url = formAction.substr(0, formAction.indexOf(formNameAction)) + formName + '/Print/' + $(element).attr("data-id");

    $("#modal-print").load(url, function () {
        $("#modal-print").modal();
    });
    return false;
}