// Update payment total price according to the discount
$(".update-payment-prices").change(function () {
    var formData = $("#frmPayment").serialize();
    var formAction = $("form").attr("action");
    var baseUrl = formAction.substr(0, formAction.indexOf('Payment'));
    var url = baseUrl + 'Payment/UpdatePrices';
    event.preventDefault();

    $.ajax({
        type: "GET",
        url: url,
        data: formData,
        dataType: "json",
        success: function (data) {
            $("#PaymentTotalPrice").val(data.TotalPrice.toString().replace('.', ','));
            $("#totalPriceFormated")[0].innerText = data.TotalPriceFormated;
        },
        error: function () {
            toastr.error('Não foi possível atualizar os valores do produto do pagamento.');
        },
    })

    return false;
});


// Load Datatable to Index page with ajax and controller
// Create structure to DataTable show entries, search and export buttons
function LoadPaymentDataTable(orderingStatus) {
    var formAction = $("form").attr("action");
    var urlBase = formAction.substr(0, formAction.indexOf('Payment'));

    var table = $(tblPayments).DataTable({
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
            url: urlBase + "Payment/GetPayments",
            type: "GET",
            dataType: "json"
        },
        columns: [
            {
                data: "Description", autowidth: true, render: function (data, type, full, meta) {
                    return '<a href=' + urlBase + 'Payment/Edit/' + full.Id + '>' + data + '</a>';
                }
            },
            {
                data: "StudentName", title: "Aluno", autowidth: true, render: function (data, type, full, meta) {
                    return '<a href=' + urlBase + 'Student/Edit/' + full.StudentId + '>' + data + '</a>';
                }
            },
            { data: "Status", autowidth: true },
            { data: "DatePaymentEstimated", autowidth: true },
            { data: "Discount", autowidth: true },
            { data: "TotalPrice", autowidth: true },
            {
                data: null, render: function (data, type, full, meta) {
                    return "<a href=" + urlBase + "Payment/Edit/" + full.Id + ">" +
                        "<i class='fas fa-edit'></i>" +
                        "</a>";
                }
            },
            {
                data: null, render: function (data, type, full, meta) {
                    return "<a href='#' onClick='DeletePayment(&apos;" + full.Id + "&apos;, &apos;" + full.Description + "&apos;, &apos;tblPayments&apos;);'>" +
                        "<i style='color: red;' class='fas fa-trash-alt'></i>" +
                        "</a >";
                }
            }
        ]
    });

    $.fn.dataTable.moment('DD/MM/YYYY HH:mm:ss');
    $.fn.DataTable.ext.pager.numbers_length = 3;

    table.buttons().container()
        .appendTo('#' + tblPayments.id + '_wrapper .col-md-6:eq(0)');
}

// Payment List Filters
function LoadPaymentTableFilters(filter, isReload) {
    // Getting the filter applied to the Index page 
    if (!filter) {
        filter = location.search.split('filter=')[1];
    }

    // Reload data table when filter was changed
    if (isReload) {
        var formAction = $("form").attr("action");
        var urlBase = formAction.substr(0, formAction.indexOf('Payment'));
        $(tblPayments).DataTable().ajax.url(urlBase + 'Payment/GetPayments?filter=' + filter).load();

        $('.btn-primary.filter-disabled').each(function () {
            $(this).removeClass('btn-primary filter-disabled');
            $(this).addClass('btn-secondary');
        });
    }

    // Apply new style to the selected filter button 
    switch (filter) {
        case "PaymentDelayed":
            $("#paymentDelayed").removeClass('btn-secondary');
            $("#paymentDelayed").addClass('btn-primary filter-disabled');
            break;

        case "PaymentDue":
            $("#paymentDue").removeClass('btn-secondary');
            $("#paymentDue").addClass('btn-primary filter-disabled');
            break;

        case "PaymentPaid":
            $("#paymentPaid").removeClass('btn-secondary');
            $("#paymentPaid").addClass('btn-primary filter-disabled');
            break;

        default:
            $("#all").removeClass('btn-secondary');
            $("#all").addClass('btn-primary filter-disabled');
            break;
    }
}
 
// Open the modal popup to select a Student
$(function () {
    $(".select-student-payment").click(function (ev) {
        if ($("#modalIsLoad").val() == 'False') {
            var formAction = $("form").attr("action");
            var url = formAction.substr(0, formAction.indexOf('Payment')) + 'Student/Select';

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
$("#btnSavePayment").click(function () {
    var formData = $("#frmPayment").serialize();
    var url = $("#frmPayment").attr("action");

    event.preventDefault();
    $('#btnSavePayment').attr('disabled', 'disabled');

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
                if ($("#StudentId").val() == "" || $("#StudentId").val() == 0) {
                    toastr.error(data.Message);
                }
            }
        },
        error: function () {
            toastr.error('Não foi possível atualizar o cadastro.');
        },
        complete: function () {
            $('#btnSavePayment').removeAttr('disabled');
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

$(document).ready(function ($) {
    $("#frmPayment").bootstrapValidator({ });
});
