// Load Datatable to Index page with ajax and controller 
// Create structure to DataTable show entries, search and export buttons
function LoadPortalUserDataTable(orderingStatus) {
    var formAction = $("form").attr("action");
    var urlBase = formAction.substr(0, formAction.indexOf('PortalUser'));

    var table = $(tblTrainingSheet).DataTable({
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
            url: urlBase + "PortalUser/GetTrainingSheets",
            type: "GET",
            dataType: "json"
        },
        columns: [
            { data: "Name", autowidth: true },
            { data: "Cycle", autowidth: true },
            { data: "StudentId", autowidth: true },
            { data: "StudentName", autowidth: true },
            { data: "Status", autowidth: true },
            { data: "Type", autowidth: true },
            {
                data: null, render: function (data, type, full, meta) {
                    return "<a href='' onclick='PrintTrainingSheet(this, &apos;TrainingSheet&apos;); return false;' data-id=" + + full.Id + "> " +
                        "<i class='fas fa-print'></i>" +
                        "</a>";
                }
            }
        ]
    });

    $.fn.DataTable.ext.pager.numbers_length = 3;
    table.buttons().container()
        .appendTo('#' + tblTrainingSheet.id + '_wrapper .col-md-6:eq(0)');
}