// Showing toastr success alert
$("#btnSaveProduct").click(function () {
    toastr.success("Produto salvo com sucesso.");
});

// Delete Product and showing toastr remove alert
function DeleteProduct(productId, productName, tableName) {
    var formAction = $("form").attr("action");
    var url = formAction.substr(0, formAction.indexOf('Product')) + 'Product/Delete';

    var token = $('input[name=__RequestVerificationToken]').val();
    var tokenadr = $('form[action="/Product"] input[name=__RequestVerificationToken]').val();
    var headers = {};
    var headersadr = {};
    headers['__RequestVerificationToken'] = token;
    headersadr['__RequestVerificationToken'] = tokenadr;

    if (confirm('Tem certeza que deseja o Produto ' + productName + '?')) {
        $.ajax({
            type: "POST",
            dataType: "json",
            headers: headersadr,
            url: url,
            data: {
                __RequestVerificationToken: token,
                id: productId
            },
            success: function (data) {
                if (data.Type == 'Success') {
                    toastr.success(data.Message);
                    RemoveDataTableRow(tableName, productId);
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
            var baseUrl = formAction.substr(0, formAction.indexOf('Product'));
            var url = baseUrl + 'Product/CapturePhoto';

            var file = reader.result;
            var extension = $('#btnUpload').val().split('.').pop();

            if (extension != null && ValidateImage(extension)) {
                var id = $('#ProductId').val();
                var token = $('input[name=__RequestVerificationToken]').val();
                var tokenadr = $('form[action="/Product/Edit/' + id + '] input[name=__RequestVerificationToken]').val();
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
                            ReloadPhoto(data.ImageName, 'productPhoto', baseUrl + 'Images/Products/');
                        }
                        else if (data.Type == 'Error') {
                            toastr.error(data.Message);
                        }
                    },
                    error: function (data) {
                        toastr.error(data.Message);
                    }
                });
            }
        };

        reader.readAsDataURL(selectedFile);
    });
    $('#btnSavePhoto').on('click', function () {
        var formAction = $("form").attr("action");
        var baseUrl = formAction.substr(0, formAction.indexOf('Product'));
        var url = baseUrl + 'Product/CapturePhoto';

        var file = $("#base64image").attr('src');
        var id = $("#ProductId").val();

        var token = $('input[name=__RequestVerificationToken]').val();
        var tokenadr = $('form[action="/Product/Edit/' + id + '] input[name=__RequestVerificationToken]').val();
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
                    ReloadPhoto(data.ImageName, 'productPhoto', baseUrl + 'Images/Products/');
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
        var baseUrl = formAction.substr(0, formAction.indexOf('Product'));
        var url = baseUrl + 'Product/RemovePhoto';

        var file = $('#productPhoto').attr('src');

        if (file != null && file.toString().indexOf("default-profile.png") <= 0) {
            var id = $("#ProductId").val();
            var token = $('input[name=__RequestVerificationToken]').val();
            var tokenadr = $('form[action="/Product/Edit/' + id + '] input[name=__RequestVerificationToken]').val();
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
                            ReloadPhoto('default-profile.png', 'productPhoto', baseUrl + 'Images/Products/');
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
            toastr.error('O Produto não possui foto para ser removida.');
        }

    });
});