// Delete Address and showing toastr remove alert
function DeleteAddress(addressId, tableName) {
    var token = $('input[name=__RequestVerificationToken]').val();
    var tokenadr = $('form[action="/User"] input[name=__RequestVerificationToken]').val();
    var headers = {};
    var headersadr = {};
    headers['__RequestVerificationToken'] = token;
    headersadr['__RequestVerificationToken'] = tokenadr;

    if (confirm('Tem certeza que deseja remover o endereco selecionado?')) {
        $.ajax({
            type: "POST",
            dataType: "json",
            headers: headersadr,
            url: '/gymtech/Address/Delete',
            data: {
                __RequestVerificationToken: token,
                id: addressId
            },
            success: function (data) {
                if (data.Type == 'Success') {
                    toastr.success(data.Message);
                    RemoveDataTableRow(tableName, addressId);
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
