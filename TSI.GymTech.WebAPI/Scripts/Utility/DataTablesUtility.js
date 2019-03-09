﻿//
function LoadDataTableButtonsAndFilter(element) {
    var table = $(element).DataTable({
        language: {
            search: '<div class="input-group col-md-12">' +
                        ' _INPUT_ ' +
                        '<span class= "input-group-append">' + 
                        '<button class="btn btn-primary" type="button">' +
                            '<i class="fa fa-search"></i>' + 
                        '</button>' + 
                        '</span>' + 
                    '</div> ',
            searchPlaceholder: "Search for..."
        },
        lengthChange: false,
        buttons: [
            {
                extend: 'copyHtml5',
                text: '<i class="fas fa-copy fa-sm text-white-35"></i>' +
                      '<span> Copy</span>',
                titleAttr: 'Copy'
            },
            {
                extend: 'print',
                text: '<i class="fas fa-print fa-sm text-white-35"></i>' +
                      '<span> Print</span>',
                titleAttr: 'Print'
            },
            {
                extend: 'excelHtml5',
                text: '<i class="fas fa-file-excel fa-sm text-white-35"></i>' +
                      '<span> Excel</span>',
                titleAttr: 'Excel'
            },
            {
                extend: 'pdfHtml5',
                text: '<i class="fas fa-file-pdf fa-sm text-white-35"></i>' +
                      '<span> PDF</span>',
                titleAttr: 'PDF'
            },
            {
                extend: 'colvis',
                text: '<i class="fas fa-columns fa-sm text-white-35"></i>' +
                      '<span> Column visibility</span>',
                titleAttr: 'PDF'
            }
        ]
    });

    console.log(element.id);
    console.log();
    table.buttons().container()
        .appendTo('#' + element.id + '_wrapper .col-md-6:eq(0)');
}