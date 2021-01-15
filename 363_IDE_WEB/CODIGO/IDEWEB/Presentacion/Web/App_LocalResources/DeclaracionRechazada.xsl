<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:template match="/">
    <xsl:choose>
      <xsl:when test="mensajeAcuse/MedioPresentacion = 3">
        <center>
          <div class="row">
            <div class="col-sm-1"></div>
            <div class="col-sm-10">
              <p>
                <xsl:value-of disable-output-escaping="yes" select="mensajeAcuse/Mensaje"/>
              </p>
            </div>
          </div>
        </center>
        <!--center>
					<table width="680"><tr ><td  align="center"><th><font SIZE="4" COLOR="black" FACE="Arial">El Estado de su Declaración es el siguiente:</font></th></td></tr></table>
					<br/>
					<table width="680"><tr ><td align="left"><font SIZE="3" COLOR="black" FACE="Arial">Contribuyente:</font><th align="left"><font SIZE="3" COLOR="black" FACE="Arial"><xsl:value-of select="mensajeAcuse/RfcAutenticacion"/></font></th></td></tr>
                             <tr ><td align="left"><font SIZE="3" COLOR="black" FACE="Arial" ></font><th align="left"><p	align="justify"><font SIZE="3" COLOR="black" FACE="Arial" bold="false"><xsl:value-of disable-output-escaping="yes" select="mensajeAcuse/Mensaje"/></font></p></th></td></tr></table>
				</center-->
      </xsl:when>
      <xsl:when test="mensajeAcuse/MedioPresentacion = 2 or mensajeAcuse/MedioPresentacion = 4">
        <div class="row">
          <div class="col-sm-1"></div>
          <div class="col-sm-10">
            <center>
              <p>
                El servicio de Administración Tributaria detectó que el Archivo de Datos presentado NO CUMPLE con las especificaciones para la integración o entrega de los datos de la Declaración Informativa de Depósitos en Efectivo, deberá generarlo nuevamente o enviarlo por el medio correcto.
              </p>
            </center>
          </div>
        </div>
        <div class="row">
          <div class="col-sm-6">
            <div class="pull-right">
              <div class="form-group">
                <label>Fecha de Presentación:</label>
              </div>
            </div>
          </div>
          <div class="col-sm-6">
            <xsl:value-of select="mensajeAcuse/FechaRecepcion"/>
          </div>
        </div>
        <div class="row">
          <div class="col-sm-6">
            <div class="pull-right">
              <div class="form-group">
                <label>Nombre del archivo:</label>
              </div>
            </div>
          </div>
          <div class="col-sm-6">
            <xsl:value-of select="mensajeAcuse/NombreArchivo"/>
          </div>
        </div>
        <div class="row">
          <div class="col-sm-6">
            <div class="pull-right">
              <div class="form-group">
                <label>Motivo de Rechazo:</label>
              </div>
            </div>
          </div>
        </div>
        <div class="row">
          <div class="col-sm-2">
          </div>
          <div class="col-sm-8">
            <xsl:value-of disable-output-escaping="yes" select="mensajeAcuse/Mensaje"/>
          </div>
        </div>
        <div class="row">
          <div class="col-sm-1"></div>
          <div class="col-sm-10">
            <center>
              En el SAT es un Placer Servirle
            </center>
          </div>
        </div>
        <!--center>
          <table><tr><td  align="center"><th><font SIZE="4" COLOR="black" FACE="Arial">Acuse de Error en Validación</font></th></td></tr></table>
          <br></br>
          <table width="400"><tr><td><font SIZE="2" COLOR="black" FACE="Verdana"><P align="justify">El servicio de Administración Tributaria detectó que el Archivo de Datos presentado NO CUMPLE con las especificaciones para la integración o entrega de los datos de la Declaración Informativa de Depósitos en Efectivo, deberá generarlo nuevamente o enviarlo por el medio correcto.</P></font></td></tr></table >
          <br></br>
          <table width="400"><tr ><td align="left"><font SIZE="2" COLOR="black" FACE="Arial" >Usuario Autenticado: </font><font SIZE="2" COLOR="black" FACE="Arial"><xsl:value-of select="mensajeAcuse/RfcAutenticacion"/></font></td></tr>
            <tr ><td style="height: 20px;"></td></tr>
            <tr><td align="left"><font SIZE="2" COLOR="black" FACE="Arial">Fecha de Presentación:</font><font SIZE="2" COLOR="black" FACE="Arial" bold="false"><xsl:value-of select="mensajeAcuse/FechaRecepcion"/></font></td></tr>
            <tr><td style="height: 20px;"></td></tr>
            <tr><td align="left"><font SIZE="2" COLOR="black" FACE="Arial" >Nombre del archivo: </font><font SIZE="2" COLOR="black" FACE="Arial" bold="false"><xsl:value-of select="mensajeAcuse/NombreArchivo"/></font></td></tr>
            <tr><td style="height: 20px;"></td></tr>
            <tr><td align="left"><font SIZE="2" COLOR="black" FACE="Arial" >Motivo de Rechazo: </font><font SIZE="2" COLOR="black" FACE="Arial" bold="false"><xsl:value-of disable-output-escaping="yes" select="mensajeAcuse/Mensaje"/></font></td></tr>
          </table>
          <br></br>
          <table ><tr ><td  align="center"><th><font SIZE="2" COLOR="black" FACE="Arial">En el SAT es un Placer Servirle</font></th></td></tr></table>
        </center-->
      </xsl:when>
    </xsl:choose>
  </xsl:template>
</xsl:stylesheet>