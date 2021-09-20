var DatatableResponsive = function () {

    // Basic Datatable examples
    var _componentDatatableResponsive = function () {
        if (!$().DataTable) {
            console.warn('Warning - datatables.min.js is not loaded.');
            return;
        }

        // Setting datatable defaults
        $.extend($.fn.dataTable.defaults, {

            autoWidth: false,
            responsive: true,
            lengthMenu: [25, 50, 75, 100],
            displayLength: 25,
            columnDefs: [{
                orderable: false,
                width: 100,
                targets: [11]
            }],
            dom: '<"datatable-header"fl><"datatable-scroll"t><"datatable-footer"ip>',
            language: {
                search: '<span>Buscar:</span> _INPUT_',
                searchPlaceholder: 'Ingresa un filtro...',
                lengthMenu: '<span>Mostrar:</span> _MENU_',
                info: 'Mostrando del _START_ al _END_ de _TOTAL_ registros',
                emptyTable: 'No hay informacion disponible en la tabla',
                zeroRecords: 'No se encontraron coincidencias',
                paginate: { 'first': 'First', 'last': 'Last', 'next': $('html').attr('dir') == 'rtl' ? '&larr;' : '&rarr;', 'previous': $('html').attr('dir') == 'rtl' ? '&rarr;' : '&larr;' }
            }
        });
        // Basic responsive configuration
        $('.datatable-responsive').DataTable();
    };


    // Select2 for length menu styling
    var _componentSelect2 = function () {
        if (!$().select2) {
            console.warn('Warning - select2.min.js is not loaded.');
            return;
        }

        // Initialize
        $('.dataTables_length select').select2({
            minimumResultsForSearch: Infinity,
            dropdownAutoWidth: true,
            width: 'auto'
        });
    };


    //
    // Return objects assigned to module
    //

    return {
        init: function () {
            _componentDatatableResponsive();
            _componentSelect2();
        }
    }
}();


// Initialize module
// ------------------------------

document.addEventListener('DOMContentLoaded', function () {
    DatatableResponsive.init();
});
