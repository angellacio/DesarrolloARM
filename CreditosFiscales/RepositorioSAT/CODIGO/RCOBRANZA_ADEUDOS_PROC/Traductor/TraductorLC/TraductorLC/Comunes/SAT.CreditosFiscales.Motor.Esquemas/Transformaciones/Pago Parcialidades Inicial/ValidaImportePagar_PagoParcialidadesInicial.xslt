<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" exclude-result-prefixes="msxsl var s0 userCSharp" version="1.0" xmlns:ns0="http://MotorTraductor.RespuestaRegla" xmlns:s0="http://SAT.CreditosFiscales.Traductor" xmlns:userCSharp="http://schemas.microsoft.com/BizTalk/2003/userCSharp">
  <xsl:output omit-xml-declaration="yes" method="xml" version="1.0"/>
  <xsl:template match="/">
    <xsl:apply-templates select="SolicitudGeneracionLC"/>
  </xsl:template>
  <xsl:template match="SolicitudGeneracionLC">  
    <xsl:for-each select="Conceptos/Concepto">
      <xsl:variable name="var:Abono" select="sum(Transacciones/Transaccion[Tipo='Abono']/Valor)"/>
      <xsl:variable name="var:Cargos" select="sum(Transacciones/Transaccion[Tipo='Cargo']/Valor)"/>
      <xsl:variable name="var:TotalPagar" select="$var:Cargos - $var:Abono"/>
      <xsl:choose>
        <xsl:when test="$var:TotalPagar != CantidadPagar/text()">
          <xsl:variable name="var:v1" select="userCSharp:StringConcat('El total de Cargos menos Abonos es diferente de la Cantidad a Pagar en Concepto ', Clave/text() )" />
          <xsl:message terminate="yes">
            <xsl:value-of select="$var:v1"/> 
          </xsl:message>
        </xsl:when>
        <xsl:otherwise>
          <Exito>OK</Exito>
        </xsl:otherwise>
      </xsl:choose>
    </xsl:for-each>
    <xsl:variable name="var:SumaCantidadesPagar" select="sum(Conceptos/Concepto/CantidadPagar)"/>
    <xsl:choose>
      <xsl:when test="$var:SumaCantidadesPagar != DatosGenerales/LineasCaptura/DatosLineaCaptura/Importe/text()">        
        <xsl:message terminate="yes">El total de Cantidad a Pagar por Concepto es diferente a Importe Total</xsl:message>
      </xsl:when>
      <xsl:otherwise>
        <Exito>OK</Exito>
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