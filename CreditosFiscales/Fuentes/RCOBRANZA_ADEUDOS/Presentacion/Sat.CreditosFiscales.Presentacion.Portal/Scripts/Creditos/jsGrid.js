function CambiaPagina(formulario, accion, control, paginaContenedor ,pagina) {
    $("#" + paginaContenedor).attr('value', pagina)
    if (document.forms[formulario]) {
        $("#" + formulario).attr('action', accion);
        $("#" + formulario).attr('data-ajax-update', "#" + control);
        $("#" + formulario).submit();
    }
    else
        alert("No existe el formulario " + formulario);

}
