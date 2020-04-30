// Load Datatable to Index page with ajax and controller 
// Create structure to DataTable show entries, search and export buttons
function LoadUsersDataTable(orderingStatus) {
    var formAction = $("form").attr("action");
    var urlBase = formAction.substr(0, formAction.indexOf('User'));

    var table = $(tblUsers).DataTable({
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
            url: urlBase + "User/GetUsers",
            type: "GET",
            dataType: "json"
        },
        columns: [
            {
                data: "Name", autowidth: true, render: function (data, type, full, meta) {
                    return '<a href=' + urlBase + 'User/Edit/' + full.Id + '>' + data + '</a>';
                }
            },
            { data: "Id", autowidth: true },
            { data: "ProfileType", autowidth: true },
            { data: "SocialSecurityCard", autowidth: true },
            { data: "Status", autowidth: true },
            { data: "Email", autowidth: true },
            {
                data: null, render: function (data, type, full, meta) {
                    return "<a href=" + urlBase + "User/Edit/" + full.Id + ">" +
                        "<i class='fas fa-edit'></i>" +
                        "</a>";
                }
            },
            {
                data: null, render: function (data, type, full, meta) {
                    return "<a href='#' onClick='DeleteUser(&apos;" + full.Id + "&apos;, &apos;" + full.Name + "&apos;, &apos;tblUsers&apos;);'>" +
                        "<i style='color: red;' class='fas fa-trash-alt'></i>" +
                        "</a >";
                }
            }
        ]
    });

    $.fn.DataTable.ext.pager.numbers_length = 3;
    table.buttons().container()
        .appendTo('#' + tblUsers.id + '_wrapper .col-md-6:eq(0)');
}

// Showing toastr success alert
$("#btnSaveUser").click(function () {
    var formData = $("#frmUser").serialize();

    event.preventDefault();
    $('#btnSaveUser').attr('disabled', 'disabled');
    var url = $("#frmUser").attr("action");

    var cpf = $('#SocialSecurityCard').val().replace(/[^0-9]/g, '').toString();
    SocialSecurityCard(cpf);

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
                toastr.success(data.Message);
            }
            else {
                DisplayValidationErrors(data.Errors)
            }
        },
        error: function () {
            toastr.error('Não foi possível atualizar o cadastro.');
        },
        complete: function () {
            $('#btnSaveUser').removeAttr('disabled');
        }
    })

    return false;
});

// Delete User and showing toastr remove alert
function DeleteUser(personId, personName, tableName) {
    var formAction = $("form").attr("action");
    var url = formAction.substr(0, formAction.indexOf('User')) + 'User/Delete';

    var token = $('input[name=__RequestVerificationToken]').val();
    var tokenadr = $('form[action="/User"] input[name=__RequestVerificationToken]').val();
    var headers = {};
    var headersadr = {};
    headers['__RequestVerificationToken'] = token;
    headersadr['__RequestVerificationToken'] = tokenadr;

    if (confirm('Tem certeza que deseja o Usuário "' + personName + '"?')) {
        $.ajax({
            type: "POST",
            dataType: "json",
            headers: headersadr,
            url: url,
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
            var formAction = $("form").attr("action");
            var baseUrl = formAction.substr(0, formAction.indexOf('User'));
            var url = baseUrl + 'User/CapturePhoto';

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
                            ReloadPhoto(data.ImageName, 'personPhoto', baseUrl + 'Images/Persons/');
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
        var formAction = $("form").attr("action");
        var baseUrl = formAction.substr(0, formAction.indexOf('User'));
        var url = baseUrl + 'User/CapturePhoto';

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
                    ReloadPhoto(data.ImageName, 'personPhoto', baseUrl + 'Images/Persons/');
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
        var baseUrl = formAction.substr(0, formAction.indexOf('User'));
        var url = baseUrl + 'User/RemovePhoto';

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
                    url: url,
                    data: {
                        __RequestVerificationToken: token,
                        id: id
                    },
                    success: function (data) {
                        if (data.Type == 'Success') {
                            toastr.success(data.Message);
                            $("#btnRemovePhoto").hide();
                            ReloadPhoto('default-user-profile.svg', 'personPhoto', baseUrl + 'Images/Persons/');
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