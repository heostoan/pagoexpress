// ============================================================
// Helper global para DataTables - Reutilizable
// ============================================================
window.AppDataTable = (function () {

    const defaultOptions = {
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.13.8/i18n/es-ES.json"
        },
        responsive: true,
        autoWidth: false,
        pageLength: 10,
        lengthMenu: [[5, 10, 25, 50, -1], [5, 10, 25, 50, "Todos"]],
        columnDefs: [
            { orderable: false, targets: -1 }
        ],
        dom:
            "<'row mb-3'<'col-sm-12 col-md-6'B><'col-sm-12 col-md-6'f>>" +
            "<'row mb-3'<'col-sm-12 col-md-6'l>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row mt-3'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7'p>>",
        buttons: [
            {
                extend: 'copy', className: 'btn btn-sm btn-secondary',
                text: '<i class="bi bi-clipboard"></i> Copiar'
            },
            {
                extend: 'excel', className: 'btn btn-sm btn-success',
                text: '<i class="bi bi-file-earmark-excel"></i> Excel'
            },
            {
                extend: 'pdf', className: 'btn btn-sm btn-danger',
                text: '<i class="bi bi-file-earmark-pdf"></i> PDF'
            },
            {
                extend: 'print', className: 'btn btn-sm btn-primary',
                text: '<i class="bi bi-printer"></i> Imprimir'
            }
        ]
    };

    function init(tableId, options = {}) {
        const selector = "#" + tableId;
        if ($.fn.DataTable.isDataTable(selector)) {
            $(selector).DataTable().destroy();
        }
        DataTable.Buttons.defaults.dom.button.className = 'btn';

        const finalOptions = $.extend(true, {}, defaultOptions, options);
        return $(selector).DataTable(finalOptions);
    }

    // ⬅️ refresh = destroy + init (NO ajax.reload)
    function refresh(tableId, options = {}) {
        return init(tableId, options);
    }

    function destroy(tableId) {
        const selector = "#" + tableId;
        if ($.fn.DataTable.isDataTable(selector)) {
            $(selector).DataTable().destroy();
        }
    }

    return { init, refresh, destroy };
})();