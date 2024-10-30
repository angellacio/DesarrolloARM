function unInner(lugar, loqueva) {
    var receptor = document.getElementById(lugar);
    var unhtml = loqueva;
    receptor.innerHTML = unhtml;
}

function ModalError(mensaje, textoAceptar ,scriptOk)
{
    $("#msgError").text(mensaje);
    $("#btnAceptarError").attr("value", textoAceptar);
    var defaultAction = "cerrarPopup();"
    if (scriptOk)
        defaultAction = defaultAction + scriptOk;
    $("#btnAceptarError").attr("onclick", defaultAction);
    $("#cerrarPopup").attr("onclick", defaultAction);
    $("#btnAceptarError").focus();
    abrir('modalError', 400, 134);
}
function ModalAlerta(mensaje, textoAceptar, scriptOk) {
    $("#msgAlerta").text(mensaje);
    $("#btnAceptarAlerta").attr("value", textoAceptar);
    var defaultAction = "cerrarPopup();"
    if (scriptOk)
        defaultAction = defaultAction + scriptOk;
    $("#btnAceptarAlerta").attr("onclick", defaultAction);
    $("#cerrarPopup").attr("onclick", defaultAction);
    $("#btnAceptarAlerta").focus();
    abrir('modalAlerta', 400, 134);
}
function ModalInfo(mensaje, textoAceptar, scriptOk) {
    $("#msgInfo").text(mensaje);
    $("#btnAceptarInfo").attr("value", textoAceptar);
    var defaultAction = "cerrarPopup();"
    if (scriptOk)
        defaultAction = defaultAction + scriptOk;
    $("#btnAceptarInfo").attr("onclick", defaultAction);
    $("#cerrarPopup").attr("onclick", defaultAction);
    $("#btnAceptarInfo").focus();
    abrir('modalInfo', 400, 134);
}

function ModalConfirmar(mensaje, textoAceptar, textoCancelar, scriptOk) {
    $("#msgConfirmar").text(mensaje);
    $("#btnAceptar").attr("value", textoAceptar);
    $("#btnCancelar").attr("value", textoCancelar);
    var defaultAction = "cerrarPopup();"
    if (scriptOk)
        defaultAction = defaultAction + scriptOk;
    $("#btnAceptar").attr("onclick", defaultAction);
    $("#cerrarPopup").attr("onclick", "cerrarPopup();");
    $("#btnAceptarInfo").focus();
    
    abrir('modalConfirmar', 400, 134);
}

function abrir(contenedor, unAncho, unAlto) {
    if (document.getElementById(contenedor).value == undefined) { var mandarContPop = document.getElementById(contenedor).innerHTML; }
    else { var mandarContPop = document.getElementById(contenedor).value; }
    unInner('cajaPopup', mandarContPop.replace('txta', 'textarea').replace('txta', 'textarea'));

    document.getElementById("cajaPopup").style.width = unAncho + "px";
    document.getElementById("cajaPopup").style.height = unAlto + "px";
    document.getElementById("interiorPopup").style.width = unAncho + "px";
    document.getElementById("interiorPopup").style.height = unAlto + "px";
    //var cajaPopup = document.getElementById("interiorPopup");
    document.getElementById("pantallaNegra").style.display = "block";
    document.getElementById("popup").style.display = "block";
    document.getElementById("cerrarPopup").style.display = "block";
    document.getElementById("cajaPopup").style.display = "block";

    var margenLeft = -unAncho / 2;
    var margenTop = -unAlto / 2;
    document.getElementById('interiorPopup').style.marginTop = margenTop + "px";
    document.getElementById('cerrarPopup').style.marginTop = (margenTop - 10) + "px";
    document.getElementById('interiorPopup').style.marginLeft = margenLeft + "px";
    document.getElementById('cerrarPopup').style.marginLeft = (-1 * margenLeft) + 30 + "px";

    //
    //setTimeout("MojoMagnify.init()", 1500);
    setTimeout("addReflections()", 1200);
    //MojoMagnify.init();
    //addReflections();
}

function abrirRemoto(contenedor2) {
    var divRemoto = document.getElementById(contenedor2);
    abrir(divRemoto);
}

function centrarModal(centralAncho, centralAlto) {
    document.getElementById('emergente').style.marginLeft = "-" + Math.round(centralAncho / 2) + "px";
    document.getElementById('emergente').style.marginTop = "-" + Math.round(centralAlto / 2) + "px";
    document.getElementById('emergente').style.visibility = "visible";
}

function cerrarPopup() {
    document.getElementById("pantallaNegra").style.display = "none";
    document.getElementById("popup").style.display = "none";
    document.getElementById("cerrarPopup").style.display = "none";
    document.getElementById("cajaPopup").style.display = "none";
    unInner("cajaPopup", "");
}

function reloj() {  

    var src = $("#loadingImg").attr("src");    
    $("#loadingImg").attr("src",src);
    $("#pantallaNegraReloj").show();
    $("#loading").show()
 

}
function ocultarReloj() {
    $("#loadingBar").hide();
    $("#loading").hide();
    $("#pantallaNegraReloj").hide();
}

function muestraDialogoError() {
    $(".mensajeError").dialog(
                {
                    modal: true,
                    buttons:
                        {
                            Ok: function () { $(this).dialog("close"); }
                        }
                });
}

function muestraDialogoCambioOrigen() {
    
    $(".mensajeCambioOrigen").dialog(
               {
                   modal: true,
                   buttons:
                       {
                           Ok: function () { $(this).dialog("close"); }
                       }
               });
}

