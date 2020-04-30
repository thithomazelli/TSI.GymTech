// Create new Payment Product
$(function () {
    $(".create-payment-product").click(function () {
        var formAction = $("form").attr("action");
        var baseUrl = formAction.substr(0, formAction.indexOf('Payment'));
        var url = baseUrl + 'PaymentProduct/Create' + '?paymentId=' + $(this).attr("data-id");

        $("#modal").load(url, function () {
            $("#modal").modal();
        });

        return false;
    });
})

// Update payment product prices according to the quantity and discount
$(".update-payment-product-prices").change(function () {
    var formData = $("#frmPaymentProduct").serialize();
    var formAction = $("form").attr("action");
    var baseUrl = formAction.substr(0, formAction.indexOf('Payment'));
    var url = baseUrl + 'PaymentProduct/UpdateProductValues';
    event.preventDefault();
                              
    $.ajax({
        type: "GET",
        url: url,
        data: formData,
        dataType: "json",
        success: function (data) {
            console.log(data.UnitPrice);
            console.log(data.TotalPrice);
            $("#UnitPrice").val(data.UnitPrice.toString().replace('.', ','));
            $("#HiddenUnitPrice").val(data.UnitPrice.toString().replace('.', ','));
            $("#TotalPrice").val(data.TotalPrice.toString().replace('.', ','));
            $("#Quota").val(data.Quota);
            $("#HiddenQuota").val(data.Quota);
        },
        error: function () {
            toastr.error('Não foi possível atualizar os valores do produto do pagamento.');
        },
    })

    return false;
});

// Edit new Payment Product 
function EditPaymentProduct(element) {
    var formAction = $("form").attr("action");
    var baseUrl = formAction.substr(0, formAction.indexOf('Payment'));
    var url = baseUrl + 'PaymentProduct/Edit/' + $(element).attr("data-id");

    $("#modal").load(url, function () {
        $("#modal").modal();
    });
    return false;
}

// Delete Payment Product and showing toastr remove alert
function DeletePaymentProduct(id, tableName, formName) {
    var formAction = $("form").attr("action");
    var url = formAction.substr(0, formAction.indexOf(formName)) + 'PaymentProduct/Delete';

    var token = $('input[name=__RequestVerificationToken]').val();
    var tokenadr = $('form[action="/' + formName + '"] input[name=__RequestVerificationToken]').val();
    var headers = {};
    var headersadr = {};
    headers['__RequestVerificationToken'] = token;
    headersadr['__RequestVerificationToken'] = tokenadr;

    if (confirm('Tem certeza que deseja remover o produto selecionado?')) {
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
                    $("#PaymentTotalPrice").val(data.TotalPrice);
                    $("#totalPriceFormated")[0].innerText = data.TotalPriceFormated;
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