// Load Datatable to Index page with ajax and controller 
// Create structure to DataTable show entries, search and export buttons
function LoadAccessLogDataTable(orderingStatus, columnToOrder) {
    var formAction = $("form").attr("action");
    var urlBase = formAction.substr(0, formAction.indexOf('AccessLog'));
    var filtering = location.search.split('filter=')[1];
    var urlController = urlBase;

    if (filtering) {
        urlController += "AccessLog/GetAccessLogs?filter=" + filtering;
    }
    else {
        urlController += "AccessLog/GetAccessLogs?filter=" + "90";
    }

    $.fn.dataTable.moment('DD/MM/YYYY HH:mm:ss');
    $.fn.DataTable.ext.pager.numbers_length = 3;

    var table = $(tblAccessLogs).DataTable({
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
        order: [[columnToOrder, "desc"]],
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
                data: "PersonName", autowidth: true, render: function (data, type, full, meta) {
                    return '<a href=' + urlBase + full.PersonType + '/Edit/' + full.PersonId + '>' + data + '</a>';
                }
            },
            { data: "AccessType", autowidth: true },
            { data: "MessageDisplayed", autowidth: true },
            { data: "CreateDate", autowidth: true },
            {
                data: null, render: function (data, type, full, meta) {
                    return "<a href='#' onClick='DeleteAccessLog(&apos;" + full.Id + "&apos;, &apos;tblAccessLogs&apos;, &apos;AccessLog&apos;);'>" +
                        "<i style='color: red;' class='fas fa-trash-alt'></i>" +
                        "</a >";
                }
            }
        ]
    });

    table.buttons().container()
        .appendTo('#' + tblAccessLogs.id + '_wrapper .col-md-6:eq(0)');
}

// Create structure to simple DataTable 
function LoadAccessLogListWithPaging(orderingStatus, columnToOrder, element, formName) {
    var formAction = $("form").attr("action");
    var urlBase = formAction.substr(0, formAction.indexOf(formName));

    $.fn.dataTable.moment('DD/MM/YYYY HH:mm:ss');
    $.fn.DataTable.ext.pager.numbers_length = 3;

    $.fn.DataTable.ext.pager.numbers_length = 3;
    var table = $(element).DataTable({
        language: {
            url: urlBase + 'Scripts/Utility/i18n/Portuguese-Brasil.json'
        },
        searching: false,   // Search Box will Be Disabled
        lengthChange: true, // Will Disabled Record number per page
        ordering: orderingStatus == null || orderingStatus == undefined ? true : orderingStatus,
        pagingType: 'simple_numbers',
        order: [[columnToOrder, "desc"]],
        info: true,          // Will show "1 to n of n entries" Text at bottom
        paging: true,        // Will enable paging
        //pagingType: 'simple_numbers',   // Will configure paging type
        responsive: true
    });
    table.buttons().container()
        .appendTo('#' + element.id + '_wrapper .col-md-6:eq(0)');
}
             
// AccessLog List Filters
function LoadAccessLogTableFilters(filter, isReload) {
    // Getting the filter applied to the Index page 
    if (!filter) {
        filter = location.search.split('filter=')[1];
    }

    // Reload data table when filter was changed
    if (isReload) {
        var formAction = $("form").attr("action");
        var urlBase = formAction.substr(0, formAction.indexOf('AccessLog'));
        $(tblAccessLogs).DataTable().ajax.url(urlBase + 'AccessLog/GetAccessLogs?filter=' + filter).load();

        $('.btn-primary.filter-disabled').each(function () {
            $(this).removeClass('btn-primary filter-disabled');
            $(this).addClass('btn-secondary');
        });
    }

    // Apply new style to the selected filter button 
    switch (filter) {
        case '7':
            $('#seven').removeClass('btn-secondary');
            $('#seven').addClass('btn-primary filter-disabled');
            break;

        case '15':
            $('#fifteen').removeClass('btn-secondary');
            $('#fifteen').addClass('btn-primary filter-disabled');
            break;

        case '30':
            $("#thirty").removeClass('btn-secondary');
            $("#thirty").addClass('btn-primary filter-disabled');
            break;

        case '60':
            $('#sixty').removeClass('btn-secondary');
            $('#sixty').addClass('btn-primary filter-disabled');
            break;

        default:
            $('#ninety').removeClass('btn-secondary');
            $('#ninety').addClass('btn-primary filter-disabled');
            break;
    }

}

// Delete AccessLog and showing toastr remove alert
function DeleteAccessLog(accessLogId, tableName, formName) {
    var formAction = $("form").attr("action");
    var url = formAction.substr(0, formAction.indexOf(formName)) + 'AccessLog/Delete';

    var token = $('input[name=__RequestVerificationToken]').val();
    var tokenadr = $('form[action="/' + formName + '"] input[name=__RequestVerificationToken]').val();
    var headers = {};
    var headersadr = {};
    headers['__RequestVerificationToken'] = token;
    headersadr['__RequestVerificationToken'] = tokenadr;

    if (confirm('Tem certeza que deseja remover o log de acesso selecionado?')) {
        $.ajax({
            type: "POST",
            dataType: "json",
            headers: headersadr,
            url: url,
            data: {
                __RequestVerificationToken: token,
                id: accessLogId
            },
            success: function (data) {
                if (data.Type == 'Success') {
                    toastr.success(data.Message);
                    RemoveDataTableRow(tableName, accessLogId);
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

$.fn.dataTable.moment = function (format, locale) {
    var types = $.fn.dataTable.ext.type;

    // Add type detection
    types.detect.unshift(function (d) {
        return moment(d, format, locale, true).isValid() ?
            'moment-' + format :
            null;
    });

    // Add sorting method - use an integer for the sorting
    types.order['moment-' + format + '-pre'] = function (d) {
        return moment(d, format, locale, true).unix();
    };
};