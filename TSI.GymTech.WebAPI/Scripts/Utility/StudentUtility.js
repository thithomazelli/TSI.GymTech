// Load Datatable to Index page with ajax and controller
// Create structure to DataTable show entries, search and export buttons
function LoadStudentsDataTable(orderingStatus) {
    var formAction = $("form").attr("action");
    var urlBase = formAction.substr(0, formAction.indexOf('Student'));
    var filtering = location.search.split('filter=')[1];
    var urlController = urlBase;

    if (filtering) {
        urlController += "Student/GetStudents?filter=" + filtering;
    }
    else {
        urlController += "Student/GetStudents";
    }

    var table = $(tblStudents).DataTable({
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
            url: urlController,
            type: "GET",
            dataType: "json"
        },
        columns: [
            {
                data: "Name", autowidth: true, render: function (data, type, full, meta) {
                    return '<a href=' + urlBase + 'Student/Edit/' + full.Id + '>' + data + '</a>';
                }
            },
            { data: "Id", autowidth: true },
            { data: "SocialSecurityCard", autowidth: true },
            { data: "Status", autowidth: true },
            { data: "Email", autowidth: true },
            {
                data: null, render: function (data, type, full, meta) {
                    return "<a href=" + urlBase + "Student/Edit/" + full.Id + ">" +
                        "<i class='fas fa-edit'></i>" +
                        "</a>";
                }
            },
            {
                data: null, render: function (data, type, full, meta) {
                    return "<a href='#' onClick='DeleteStudent(&apos;" + full.Id + "&apos;, &apos;" + full.Name + "&apos;, &apos;tblStudents&apos;);'>" +
                        "<i style='color: red;' class='fas fa-trash-alt'></i>" +
                        "</a >";
                }
            }
        ]
    });

    $.fn.DataTable.ext.pager.numbers_length = 3;
    table.buttons().container()
        .appendTo('#' + tblStudents.id + '_wrapper .col-md-6:eq(0)');
}

// Load Datatable to the Select page with ajax and controller 
function LoadDataTableToSelectStudent(element, orderingStatus) {
    var formAction = $("#frmSelectPerson").attr("action");
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
            url: urlBase + "Student/GetStudents",
            type: "GET",
            dataType: "json"
        },
        columns: [
            { data: "Id", autowidth: true },
            { data: "Name", autowidth: true },
            {
                data: null, render: function (data, type, full, meta) {
                    return "<a href='#' onClick='SelectPerson(&apos;" + full.Id + "&apos;, &apos;" + full.Name + "&apos;, &apos;tblStudents&apos;); return false;'>" +
                        "<i class='fas fa-check-circle'></i>" +
                        "</a >";
                }
            }
        ]
    });

    $.fn.DataTable.ext.pager.numbers_length = 3;
    table.buttons().container()
        .appendTo('#' + element.id + '_wrapper .col-md-6:eq(0)');
}

// Student List Filters
function LoadStudentTableFilters(filter, isReload) {
    // Getting the filter applied to the Index page 
    if (!filter) {
        filter = location.search.split('filter=')[1];
    }

    // Reload data table when filter was changed
    if (isReload) {
        var formAction = $("form").attr("action");
        var urlBase = formAction.substr(0, formAction.indexOf('Student'));
        $(tblStudents).DataTable().ajax.url(urlBase + 'Student/GetStudents?filter=' + filter).load();

        $('.btn-primary.filter-disabled').each(function () {
            $(this).removeClass('btn-primary filter-disabled');
            $(this).addClass('btn-secondary');
        });
    }

    // Apply new style to the selected filter button 
    switch (filter) {
        case "Frequent":
            $("#frequent").removeClass('btn-secondary');
            $("#frequent").addClass('btn-primary filter-disabled');
            break;

        case "NotFrequent":
            $("#notFrequent").removeClass('btn-secondary');
            $("#notFrequent").addClass('btn-primary filter-disabled');
            break;

        case "Inactive":
            $("#inactive").removeClass('btn-secondary');
            $("#inactive").addClass('btn-primary filter-disabled');
            break;

        case "Birthday":
            $("#birthday").removeClass('btn-secondary');
            $("#birthday").addClass('btn-primary filter-disabled');
            break;

        default:
            $("#all").removeClass('btn-secondary');
            $("#all").addClass('btn-primary filter-disabled');
            break;
    }
}

// Showing toastr success alert
$("#btnSaveStudent").click(function () {
    var formData = $("#frmStudent").serialize();
    var url = $("#frmStudent").attr("action");
    $('#btnSaveStudent').attr('disabled', 'disabled');
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
            $('#btnSaveStudent').removeAttr('disabled');
        }
    })
});

// Delete User and showing toastr remove alert
function DeleteStudent(personId, personName, tableName) {
    var formAction = $("form").attr("action");
    var url = formAction.substr(0, formAction.indexOf('Student')) + 'Student/Delete';

    var token = $('input[name=__RequestVerificationToken]').val();
    var tokenadr = $('form[action="/Student"] input[name=__RequestVerificationToken]').val();
    var headers = {};
    var headersadr = {};
    headers['__RequestVerificationToken'] = token;
    headersadr['__RequestVerificationToken'] = tokenadr;

    if (confirm('Tem certeza que deseja remover o Aluno "' + personName + '"?')) {
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
            var baseUrl = formAction.substr(0, formAction.indexOf('Student'));
            var url = baseUrl + 'Student/CapturePhoto';

            var file = reader.result;
            var extension = $('#btnUpload').val().split('.').pop();
            $('#base64image').attr('src', reader.result);

            if (extension != null && ValidateImage(extension)) {
                var id = $('#PersonId').val();
                var token = $('input[name=__RequestVerificationToken]').val();
                var tokenadr = $('form[action="/Student/Edit/' + id + '] input[name=__RequestVerificationToken]').val();
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
        var baseUrl = formAction.substr(0, formAction.indexOf('Student'));
        var url = baseUrl + 'Student/CapturePhoto';

        var file = $("#base64image").attr('src');
        var id = $("#PersonId").val();

        var token = $('input[name=__RequestVerificationToken]').val();
        var tokenadr = $('form[action="/Student/Edit/' + id + '] input[name=__RequestVerificationToken]').val();
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
        var baseUrl = formAction.substr(0, formAction.indexOf('Student'));
        var url = baseUrl + 'Student/RemovePhoto';

        var file = $('#personPhoto').attr('src');

        if (file != null && file.toString().indexOf("default-user-profile.svg") <= 0) {
            var id = $("#PersonId").val();
            var token = $('input[name=__RequestVerificationToken]').val();
            var tokenadr = $('form[action="/Student/Edit/' + id + '] input[name=__RequestVerificationToken]').val();
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

// Create new Copy of Training Sheet
$(function () {
    $("#copy-trainingsheet").click(function (ev) {
        if ($("#modalTrainingIsLoad").val() == 'False') {
            var formAction = $("form").attr("action");
            var url = formAction.substr(0, formAction.indexOf('Student')) + 'TrainingSheet/Select/' + $(this).attr("data-id");

            $("#modalTraining").load(url);
            $("#modalTrainingIsLoad").val('True');
        }
        $("#modalTraining").modal({
            cache: false,
            backdrop: 'static',
            keyboard: false
        }, "show");
        ev.preventDefault();
        return false;
    });
});