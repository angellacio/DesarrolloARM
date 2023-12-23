<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" exclude-result-prefixes="msxsl var userCSharp" version="1.0" xmlns:userCSharp="http://schemas.microsoft.com/BizTalk/2003/userCSharp">
  <xsl:output omit-xml-declaration="yes" method="xml" version="1.0" />
  <xsl:template match="/">
    <xsl:apply-templates select="/CreditosFiscales" />
  </xsl:template>
  <xsl:template match="/CreditosFiscales">
    <xsl:variable name="var:v1" select="userCSharp:StringConcat(&quot;0&quot;)" />
    <xsl:variable name="var:v2" select="userCSharp:StringConcat(&quot;5&quot;)" />
    <xsl:variable name="var:v3" select="count(/CreditosFiscales/Conceptos/Concepto)" />
    <SolicitudGeneracionLC>
      <DatosGenerales>
        <IdDocumento>
          <xsl:value-of select="$var:v1" />
        </IdDocumento>
        <Solicitud>
          <xsl:value-of select="DatosGenerales/Folio/text()" />
        </Solicitud>
        <IdContribuyente>
          <xsl:value-of select="DatosGenerales/IdContribuyente/text()" />
        </IdContribuyente>
        <OrigenInformacion>
          <xsl:value-of select="$var:v2" />
        </OrigenInformacion>
        <NumeroConceptos>
          <xsl:value-of select="$var:v3" />
        </NumeroConceptos>
        <ImporteTotal>
          <xsl:value-of select="DatosGenerales/ImporteTotal/text()" />
        </ImporteTotal>
        <FechaEmision>
          <xsl:value-of select="DatosGenerales/FechaEmision/text()" />
        </FechaEmision>
        <RFC>
          <xsl:choose>
            <xsl:when test="DatosGenerales/DeudorPuro/text() = 1">
              <xsl:value-of select="DatosGenerales/RFCGenerico/text()" />
            </xsl:when>
            <xsl:otherwise>
              <xsl:value-of select="DatosGenerales/RFC/text()" />
            </xsl:otherwise>
          </xsl:choose>
        </RFC>
        <xsl:if test="DatosGenerales/Nombre/text()">
          <Nombre>
            <xsl:value-of select="DatosGenerales/Nombre/text()" />
          </Nombre>
        </xsl:if>
        <xsl:if test="DatosGenerales/ApellidoPaterno/text()">
          <ApellidoPaterno>
            <xsl:value-of select="DatosGenerales/ApellidoPaterno/text()" />
          </ApellidoPaterno>
        </xsl:if>
        <xsl:if test="DatosGenerales/ApellidoMaterno/text()">
          <ApellidoMaterno>
            <xsl:value-of select="DatosGenerales/ApellidoMaterno/text()" />
          </ApellidoMaterno>
        </xsl:if>
        <xsl:if test="DatosGenerales/RazonSocial/text()">
          <RazonSocial>
            <xsl:value-of select="DatosGenerales/RazonSocial/text()" />
          </RazonSocial>
        </xsl:if>
        <ClaveAlr>
          <xsl:value-of select="DatosGenerales/ALR/text()" />
        </ClaveAlr>
        <TipoDocumento>
          <xsl:value-of select="DatosGenerales/TipoDocumento/text()" />
        </TipoDocumento>
        <DescripcionTipoDocumento>
          <xsl:value-of select="DatosGenerales/DescripcionDocumento/text()" />
        </DescripcionTipoDocumento>        
      </DatosGenerales>
      <Conceptos>
        <xsl:for-each select="Conceptos/Concepto">
          <Concepto>
            <NumeroSecuencia>
              <xsl:value-of select="NoSecuencia/text()" />
            </NumeroSecuencia>
            <Clave>
              <xsl:value-of select="Clave/text()" />
            </Clave>
            <Descripcion>
              <xsl:value-of select="Descripcion/text()" />
            </Descripcion>
            <CantidadPagar>
              <xsl:value-of select="ImportePagar/text()"/>
            </CantidadPagar>
            <DatosIcep>
              <ClavePeriodicidad>
                <xsl:value-of select="TipoPeriodicidad/text()" />
              </ClavePeriodicidad>
              <DescripcionPeriodicidad>
                <xsl:value-of select="Periodicidad/text()" />
              </DescripcionPeriodicidad>
              <ClavePeriodo>
                <xsl:value-of select="ClavePeriodo/text()" />
              </ClavePeriodo>
              <DescripcionPeriodo>
                <xsl:value-of select="Periodo/text()" />
              </DescripcionPeriodo>
              <FechaCausacion>
                <xsl:value-of select="FechaCausacion/text()" />
              </FechaCausacion>
            </DatosIcep>
            <Transacciones>
              <xsl:for-each select="Transacciones/Transaccion">
                <Transaccion>
                  <Clave>
                    <xsl:value-of select="Clave/text()" />
                  </Clave>
                  <Descripcion>
                    <xsl:value-of select="Descripcion/text()" />
                  </Descripcion>
                  <Valor>
                    <xsl:value-of select="Importe/text()" />
                  </Valor>
                  <Tipo>
                    <xsl:value-of select="Tipo/text()" />
                  </Tipo>
                  <xsl:value-of select="./text()" />
                </Transaccion>
              </xsl:for-each>
              <xsl:value-of select="Transacciones/text()" />
            </Transacciones>
            <xsl:value-of select="./text()" />
          </Concepto>
        </xsl:for-each>
      </Conceptos>
    </SolicitudGeneracionLC>
  </xsl:template>
  <msxsl:script language="C#" implements-prefix="userCSharp">
    <![CDATA[
public string StringConcat(string param0)
{
   return param0;
}



]]>
  </msxsl:script>
</xsl:stylesheet>