// Delete Address and showing toastr remove alert
function DeleteAddress(addressId, tableName, formName) {
    var token = $('input[name=__RequestVerificationToken]').val();
    var tokenadr = $('form[action="/' + formName + '"] input[name=__RequestVerificationToken]').val();
    var headers = {};
    var headersadr = {};
    headers['__RequestVerificationToken'] = token;
    headersadr['__RequestVerificationToken'] = tokenadr;

    if (confirm('Tem certeza que deseja remover o endereço selecionado?')) {
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

// Search PostalCode
$("#searchPostalCode").click(function() {

    // New variable "postalCode" with only numbers.
    var postalCode = $("#PostalCode").val().replace(/\D/g, '');

    // Checks if postalCode field has value
    if (postalCode != "") {

        // Regular expression to validate PostalCode.
        var validatePostalCode = /^[0-9]{8}$/;

        // Validate de PostalCode format
        if (validatePostalCode.test(postalCode)) {

            //// Autofil the address fields with "..." when the webservice is loading.
            //$("#Street").val("...");
            //$("#District").val("...");
            //$("#City").val("...");
            //$("#State").val("...");

            // Get the values from webservice viacep.com.br/
            $.getJSON("https://viacep.com.br/ws/" + postalCode +"/json/?callback=?", function(dados) {

                if (!("erro" in dados)) {
                    // Update the address fields with the value found
                    $("#Street").val(dados.logradouro);
                    $("#District").val(dados.bairro);
                    $("#City").val(dados.localidade);
                    $("#State").val(dados.uf);

                    // Toastr success message
                    toastr.success("O CEP informado foi encontrado.");
                } 
                else {
                    // Toastr error message - The PostalCode was not found.
                    toastr.error("O CEP informado não foi encontrado.");
                }
            });
        } 
        else {            
            // Toastr error message - PostalCode is invalid.
            toastr.error("O formato de CEP inválido.");
        }
    } 
});