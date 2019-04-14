// Create structure to DataTable show entries, search and export buttons
function LoadDataTableButtonsAndFilter(element) {
    $.fn.DataTable.ext.pager.numbers_length = 3;
    var table = $(element).DataTable({
        language: {
            url: 'Scripts/Utility/i18n/Portuguese-Brasil.json',
            search: '<div class="input-group col-md-12">' +
                        ' _INPUT_ ' +
                        '<span class= "input-group-append">' + 
                            '<button class="btn btn-primary" type="button">' +
                                '<i class="fa fa-search"></i>' + 
                            '</button>' + 
                        '</span>' + 
                    '</div> ',
            searchPlaceholder: 'Pesquisar por...'
        },
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

// Remove specific row from DataTable
function RemoveDataTableRow(tableName, rowId) {
    $("#" + tableName).DataTable().row("#" + rowId).remove().draw();
}