window.chosenInit = (select) => {
    try {
        if (!select) return;
        // comprobar si jQuery y Chosen están cargados
        if (window.jQuery && typeof window.jQuery(select).chosen === 'function') {
            window.jQuery(select).chosen({width: '100%'});
        } else {
            // si no está chosen, añadir buscador nativo simple
            const el = select;
            el.setAttribute('data-search', 'true');
        }
    } catch (e) {
        console.error('chosenInit error', e);
    }
};
