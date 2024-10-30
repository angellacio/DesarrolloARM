<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" exclude-result-prefixes="msxsl var s0 userCSharp" version="1.0"  xmlns:s0="http://SAT.CreditosFiscales.Traductor" xmlns:userCSharp="http://schemas.microsoft.com/BizTalk/2003/userCSharp">
  <xsl:output omit-xml-declaration="yes" method="xml" version="1.0"/>
  <xsl:template match="/">
    <xsl:apply-templates select="/MovimientoRectificacion"/>
  </xsl:template>
  <xsl:template match="/MovimientoRectificacion">
    <xsl:variable name="var:CantidadPagarCorrecto" select="Mensaje/ResolucionesDeterminante/ResolucionDeterminante/Conceptos/Rectificaciones/Correcto/ImportePagar/text() + sum(Mensaje/ResolucionesDeterminante/ResolucionDeterminante/Conceptos/Rectificaciones/Correcto/Hijos/Hijo/ImportePagar)"/>
    <xsl:variable name="var:CantidadPagarErroneo" select="Mensaje/ResolucionesDeterminante/ResolucionDeterminante/Conceptos/Rectificaciones/Erroneo/ImportePagar/text() + sum(Mensaje/ResolucionesDeterminante/ResolucionDeterminante/Conceptos/Rectificaciones/Erroneo/Hijos/Hijo/ImportePagar)"/>
    <xsl:variable name="var:FormaPago" select="Mensaje/ResolucionesDeterminante/ResolucionDeterminante/FormaPago/text()"/>
    <xsl:choose>
      <xsl:when test="$var:CantidadPagarCorrecto = $var:CantidadPagarErroneo">
        <Exito>OK</Exito>
      </xsl:when>
      <xsl:otherwise>
        <xsl:variable name="var:ErrorImportes" select="userCSharp:StringConcat('El importe de cantidad a pagar debe ser igual en ambos conceptos', '' )" />
        <xsl:message terminate="yes">
          <xsl:value-of select="$var:ErrorImportes"/>
        </xsl:message>
      </xsl:otherwise>
    </xsl:choose>        
  </xsl:template>
  <msxsl:script language="C#" implements-prefix="userCSharp">
    <![CDATA[
public string StringConcat(string param0, string param1)
{
   return param0 + param1;
}



]]>
  </msxsl:script>
</xsl:stylesheet>