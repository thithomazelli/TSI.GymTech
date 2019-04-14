// Showing toastr success alert
$("#btnSaveUser").click(function () {
    toastr.success("Usuário salvo com sucesso.");
});

// Delete User and showing toastr remove alert
function DeleteUser(personId, personName, tableName) {
    var token = $('input[name=__RequestVerificationToken]').val();
    var tokenadr = $('form[action="/User"] input[name=__RequestVerificationToken]').val();
    var headers = {};
    var headersadr = {};
    headers['__RequestVerificationToken'] = token;
    headersadr['__RequestVerificationToken'] = tokenadr;

    if (confirm('Tem certeza que deseja o Usuário ' + personName + '?')) {
        $.ajax({
            type: "POST",
            dataType: "json",
            headers: headersadr,
            url: '/gymtech/User/Delete',
            data: {
                __RequestVerificationToken: token,
                id: personId
            },
            success: function (data) {
                if (data.Type == 'Success') {
                    toastr.success(data.Message);
                    RemoveDataTableRow(tableName, personId);
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
            var file = reader.result;
            var extension = $('#btnUpload').val().split('.').pop();
            $('#base64image').attr('src', reader.result);
            
            if (extension != null && ValidateImage(extension)) {
                var id = $('#PersonId').val();
                var token = $('input[name=__RequestVerificationToken]').val();
                var tokenadr = $('form[action="/User/Edit/' + id + '] input[name=__RequestVerificationToken]').val();
                var headers = {};
                var headersadr = {};
                headers['__RequestVerificationToken'] = token;
                headersadr['__RequestVerificationToken'] = tokenadr;

                $.ajax({
                    type: "POST",
                    dataType: "json",
                    headers: headersadr,
                    url: '/gymtech/User/CapturePhoto',
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
                            ReloadPhoto(data.ImageName, 'personPhoto', '/gymtech/Images/Persons/');
                        }                              
                        else if (data.Type == 'Error') {
                            toastr.error(data.Message);
                        }
                    },
                    error: function () {
                        toastr.error('Não foi possível remover a imagem.');
                    }
                });
            }
        };

        reader.readAsDataURL(selectedFile);
    });
    $('#btnSavePhoto').on('click', function () {
        var file = $("#base64image").attr('src');
        var id = $("#PersonId").val();

        var token = $('input[name=__RequestVerificationToken]').val();
        var tokenadr = $('form[action="/User/Edit/' + id + '] input[name=__RequestVerificationToken]').val();
        var headers = {};
        var headersadr = {};
        headers['__RequestVerificationToken'] = token;
        headersadr['__RequestVerificationToken'] = tokenadr;

        $.ajax({
            type: "POST",
            dataType: "json",
            headers: headersadr,
            url: '/gymtech/User/CapturePhoto',
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
                    ReloadPhoto(data.ImageName, 'personPhoto', '/gymtech/Images/Persons/');
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

        var file = $('#personPhoto').attr('src');

        if (file != null && file.toString().indexOf("default-user-profile.svg") <= 0) {
            var id = $("#PersonId").val();
            var token = $('input[name=__RequestVerificationToken]').val();
            var tokenadr = $('form[action="/User/Edit/' + id + '] input[name=__RequestVerificationToken]').val();
            var headers = {};
            var headersadr = {};
            headers['__RequestVerificationToken'] = token;
            headersadr['__RequestVerificationToken'] = tokenadr;

            if (confirm('Tem certeza que deseja remover a imagem?')) {
                $.ajax({
                    type: "POST",
                    dataType: "json",
                    headers: headersadr,
                    url: '/gymtech/User/RemovePhoto',
                    data: {
                        __RequestVerificationToken: token,
                        id: id
                    },
                    success: function (data) {
                        if (data.Type == 'Success') {
                            toastr.success(data.Message);
                            $("#btnRemovePhoto").hide();
                            ReloadPhoto('default-user-profile.svg', 'personPhoto', '/gymtech/Images/Persons/');
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
            toastr.error('O Usuário não possui foto para ser removida.');
        }

    });
});