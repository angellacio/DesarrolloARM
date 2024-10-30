<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" exclude-result-prefixes="msxsl var s0 userCSharp" version="1.0" xmlns:s0="http://SAT.CreditosFiscales.Traductor" xmlns:userCSharp="http://schemas.microsoft.com/BizTalk/2003/userCSharp">
  <xsl:output omit-xml-declaration="yes" method="xml" version="1.0"/>
  <xsl:template match="/">
    <xsl:apply-templates select="SolicitudGeneracionLC"/>
  </xsl:template>
  <xsl:template match="SolicitudGeneracionLC">  
    <xsl:for-each select="Conceptos/Concepto">
      <xsl:variable name="var:ClavePeriodo" select="DatosIcep/ClavePeriodo/text()"/>      
      <xsl:choose>
        <xsl:when test="$var:ClavePeriodo = '099'">
          <xsl:choose>
            <xsl:when test="DatosIcep/FechaCausacion/text()">
              <Exito>OK</Exito>
            </xsl:when>
            <xsl:otherwise>
              <xsl:variable name="var:v1" select="userCSharp:StringConcat('Sí concepto de pago tiene periodo 099 (Sin Periodo) debe tener fecha de causación, en otro caso debe tener Ejercicio.', '' )" />
              <xsl:message terminate="yes">
                <xsl:value-of select="$var:v1"/>
              </xsl:message>
            </xsl:otherwise>
          </xsl:choose>
        </xsl:when>
        <xsl:otherwise>
          <Exito>OK</Exito>
        </xsl:otherwise>
      </xsl:choose>
    </xsl:for-each>   
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