// Load Datatable to Index page with ajax and controller 
// Create structure to DataTable show entries, search and export buttons
function LoadAccessControlDataTable(orderingStatus) {
    var formAction = $("form").attr("action");
    var urlBase = formAction.substr(0, formAction.indexOf('AccessControl'));

    var table = $(tblAccessControls).DataTable({
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
            url: urlBase + "AccessControl/GetAccessControls",
            type: "GET",
            dataType: "json"
        },
        columns: [
            {
                data: "Name", autowidth: true, render: function (data, type, full, meta) {
                    return '<a href=' + urlBase + 'AccessControl/Edit/' + full.Id + '>' + data + '</a>';
                }
            },
            { data: "IpAddress", autowidth: true },
            {
                data: "IsStandard", autowidth: true, render: function (data, type, full, meta) {
                    var result = "<input "; 

                    if (data == true) {
                        result += "checked='checked'";
                    }

                    return result += " class='check-box' disabled='disabled' type='checkbox'>";
                }
            },
            {
                data: null, render: function (data, type, full, meta) {
                    return "<a href=" + urlBase + "AccessControl/Edit/" + full.Id + ">" +
                        "<i class='fas fa-edit'></i>" +
                        "</a>";
                }
            },
            {
                data: null, render: function (data, type, full, meta) {
                    return "<a href='#' onClick='DeleteAccessControl(&apos;" + full.Id + "&apos;, &apos;" + full.Name + "&apos;, &apos;tblAccessControls&apos;);'>" +
                        "<i style='color: red;' class='fas fa-trash-alt'></i>" +
                        "</a >";
                }
            }
        ]
    });

    $.fn.DataTable.ext.pager.numbers_length = 3;
    table.buttons().container()
        .appendTo('#' + tblAccessControls.id + '_wrapper .col-md-6:eq(0)');
}

// Showing toastr success alert
$("#btnSaveAccessControl").click(function () {
    var formData = $("#frmAccessControl").serialize();
    event.preventDefault();
    $('#btnSaveAccessControl').attr('disabled', 'disabled');
    var url = $("#frmAccessControl").attr("action");

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
            $('#btnSaveAccessControl').removeAttr('disabled');
        }
    })

    return false;
});

// Delete AccessControl and showing toastr remove alert
function DeleteAccessControl(accessControlId, accessControlName, tableName) {
    var formAction = $("form").attr("action");
    var url = formAction.substr(0, formAction.indexOf('AccessControl')) + 'AccessControl/Delete';

    var token = $('input[name=__RequestVerificationToken]').val();
    var tokenadr = $('form[action="/AccessControl"] input[name=__RequestVerificationToken]').val();
    var headers = {};
    var headersadr = {};
    headers['__RequestVerificationToken'] = token;
    headersadr['__RequestVerificationToken'] = tokenadr;

    if (confirm('Tem certeza que deseja o remover Equipamento "' + accessControlName + '"?')) {
        $.ajax({
            type: "POST",
            dataType: "json",
            headers: headersadr,
            url: url,
            data: {
                __RequestVerificationToken: token,
                id: accessControlId
            },
            success: function (data) {
                if (data.Type == 'Success') {
                    toastr.success(data.Message);
                    RemoveDataTableRow(tableName, accessControlId);
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