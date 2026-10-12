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
            { extend: 'copy', className: 'btn btn-sm btn-secondary', text: '<i class="bi bi-clipboard"></i> Copiar' },
            { extend: 'excel', className: 'btn btn-sm btn-success', text: '<i class="bi bi-file-earmark-excel"></i> Excel' },
            { extend: 'pdf', className: 'btn btn-sm btn-danger', text: '<i class="bi bi-file-earmark-pdf"></i> PDF' },
            { extend: 'print', className: 'btn btn-sm btn-primary', text: '<i class="bi bi-printer"></i> Imprimir' }
        ]
    };

    // Inicializar. El ID debe ser único por refresh, así nunca chocamos con instancias previas.
    function init(tableId, options = {}) {
        const selector = "#" + tableId;
        const $table = $(selector);

        if (!$table.length || !$table[0]) {
            // La tabla no existe (fue reemplazada) — no es error
            return null;
        }

        // Si por alguna razón ya está inicializada, intentar destruir SIN tocar el DOM
        try {
            if ($.fn.DataTable.isDataTable(selector)) {
                $table.DataTable().destroy(false);
            }
        } catch (e) {
            // Ignorar: significa que el DOM ya fue reemplazado por Blazor
            console.warn('[DataTables] destroy ignorado:', e.message);
        }

        DataTable.Buttons.defaults.dom.button.className = 'btn';
        const finalOptions = $.extend(true, {}, defaultOptions, options);

        try {
            return $table.DataTable(finalOptions);
        } catch (e) {
            console.error('[DataTables] Error init:', e);
            return null;
        }
    }

    function refresh(tableId, options = {}) {
        return init(tableId, options);
    }

    // Destrucción silenciosa: nunca lanza error
    function destroy(tableId) {
        try {
            const selector = "#" + tableId;
            const $table = $(selector);
            if (!$table.length) return;
            if ($.fn.DataTable.isDataTable(selector)) {
                $table.DataTable().destroy(false);
            }
        } catch (e) {
            console.warn('[DataTables] destroy:', e.message);
        }
    }

    const safeDestroy = destroy;

    return { init, refresh, destroy, safeDestroy };
})();