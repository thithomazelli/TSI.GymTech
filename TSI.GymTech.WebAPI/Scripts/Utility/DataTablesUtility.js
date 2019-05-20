// Create structure to simple DataTable
function LoadSimpleDataTable(element) {
    $.fn.DataTable.ext.pager.numbers_length = 3;
    var table = $(element).DataTable({
        language: {
            url: '/gymtech/Scripts/Utility/i18n/Portuguese-Brasil.json'
        },
        searching: false,   // Search Box will Be Disabled
        lengthChange: false, // Will Disabled Record number per page
        ordering: true,    // Ordering (Sorting on Each Column)will Be Disabled
        info: true,          // Will show "1 to n of n entries" Text at bottom
        paging: false,        // Will enable paging
        //pagingType: 'simple_numbers',   // Will configure paging type
        responsive: true
    });
    table.buttons().container()
        .appendTo('#' + element.id + '_wrapper .col-md-6:eq(0)');
}

// Create structure to simple DataTable 
function LoadDataTableWithPaging(element) {
    $.fn.DataTable.ext.pager.numbers_length = 3;
    var table = $(element).DataTable({
        language: {
            url: '/gymtech/Scripts/Utility/i18n/Portuguese-Brasil.json'
        },
        searching: false,   // Search Box will Be Disabled
        lengthChange: true, // Will Disabled Record number per page
        ordering: true,    // Ordering (Sorting on Each Column)will Be Disabled
        info: true,          // Will show "1 to n of n entries" Text at bottom
        paging: true,        // Will enable paging
        //pagingType: 'simple_numbers',   // Will configure paging type
        responsive: true
    });
    table.buttons().container()
        .appendTo('#' + element.id + '_wrapper .col-md-6:eq(0)');
}

// Create structure to DataTable show entries, search and export buttons
function LoadDataTableButtonsAndFilter(element) {
    $.fn.DataTable.ext.pager.numbers_length = 3;
    var table = $(element).DataTable({
        language: {
            url: '/gymtech/Scripts/Utility/i18n/Portuguese-Brasil.json',
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
        pagingType: 'simple_numbers',
        lengthChange: true,
        rowId: "id",
        responsive: true //,
        //buttons: [
        //    {
        //        extend: 'copyHtml5',
        //        text: '<i class="fas fa-copy fa-sm text-white-35"></i>' +
        //              '<span> Copy</span>',
        //        titleAttr: 'Copy'
        //    },
        //    {
        //        extend: 'print',
        //        text: '<i class="fas fa-print fa-sm text-white-35"></i>' +
        //              '<span> Print</span>',
        //        titleAttr: 'Print'
        //    },
        //    {
        //        extend: 'excelHtml5',
        //        text: '<i class="fas fa-file-excel fa-sm text-white-35"></i>' +
        //              '<span> Excel</span>',
        //        titleAttr: 'Excel'
        //    },
        //    {
        //        extend: 'pdfHtml5',
        //        text: '<i class="fas fa-file-pdf fa-sm text-white-35"></i>' +
        //              '<span> PDF</span>',
        //        titleAttr: 'PDF'
        //    },
        //    {
        //        extend: 'colvis',
        //        text: '<i class="fas fa-columns fa-sm text-white-35"></i>' +
        //              '<span> Column visibility</span>',
        //        titleAttr: 'PDF'
        //    }
        //]
    });
    table.buttons().container()
        .appendTo('#' + element.id + '_wrapper .col-md-6:eq(0)');
}

// Create structure to DataTable show entries, search and export buttons
function LoadDataTableToPopUp(element) {
    $.fn.DataTable.ext.pager.numbers_length = 3;
    var table = $(element).DataTable({
        language: {
            url: '/gymtech/Scripts/Utility/i18n/Portuguese-Brasil.json',
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
        pagingType: 'simple_numbers',
        lengthChange: true,
        lengthMenu: [5, 10, 15, "Todos"],
        info: false,
        responsive: true
    });
    table.buttons().container()
        .appendTo('#' + element.id + '_wrapper .col-md-6:eq(0)');
}

// Create structure to DataTable show entries, search and export buttons
function LoadDataTableButtonsAndFilterAndOrdering(element, columnToOrder) {
    $.fn.DataTable.ext.pager.numbers_length = 3;
    var table = $(element).DataTable({
        language: {
            url: '/gymtech/Scripts/Utility/i18n/Portuguese-Brasil.json',
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
        order: [[columnToOrder, "desc"]],
        pagingType: 'simple_numbers',
        lengthChange: true,
        responsive: true //,
        //buttons: [
        //    {
        //        extend: 'copyHtml5',
        //        text: '<i class="fas fa-copy fa-sm text-white-35"></i>' +
        //              '<span> Copy</span>',
        //        titleAttr: 'Copy'
        //    },
        //    {
        //        extend: 'print',
        //        text: '<i class="fas fa-print fa-sm text-white-35"></i>' +
        //              '<span> Print</span>',
        //        titleAttr: 'Print'
        //    },
        //    {
        //        extend: 'excelHtml5',
        //        text: '<i class="fas fa-file-excel fa-sm text-white-35"></i>' +
        //              '<span> Excel</span>',
        //        titleAttr: 'Excel'
        //    },
        //    {
        //        extend: 'pdfHtml5',
        //        text: '<i class="fas fa-file-pdf fa-sm text-white-35"></i>' +
        //              '<span> PDF</span>',
        //        titleAttr: 'PDF'
        //    },
        //    {
        //        extend: 'colvis',
        //        text: '<i class="fas fa-columns fa-sm text-white-35"></i>' +
        //              '<span> Column visibility</span>',
        //        titleAttr: 'PDF'
        //    }
        //]
    });
    table.buttons().container()
        .appendTo('#' + element.id + '_wrapper .col-md-6:eq(0)');
}

// Add new row to DataTable
function AddDataTableRow(tableName, model) {
    var dataTable = $('#' + tableName).DataTable();
    dataTable.row.add([
        '<a href="/gymtech/TrainingSheet/Edit/' + parseInt(model.TrainingSheetId) + '">' +
            model.Name +
        '</a>',
        model.Cycle,
        model.Model,
        model.Status,
        model.Type,
        '<a href="/TrainingSheet/Edit/' + parseInt(model.TrainingSheetId) + '">' +
            '<i class= "fas fa-edit" ></i >' +
        '</a>',
        '<a href="#" onclick="DeleteTrainingSheet(' + model.TrainingSheetId + ', &apos;' + model.Name + '&apos;, &apos;tblTrainingSheet&apos;);">' +
            '<i style = "color: red;" class="fas fa-trash-alt" ></i>' +
        '</a>'
    ])
        .draw(false)
        .node().id = parseInt(model.TrainingSheetId);
}

// Remove specific row from DataTable
function RemoveDataTableRow(tableName, rowId) {
    $("#" + tableName).DataTable().row("#" + rowId).remove().draw();
}