<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" exclude-result-prefixes="msxsl var userCSharp" version="1.0" xmlns:userCSharp="http://schemas.microsoft.com/BizTalk/2003/userCSharp">
  <xsl:output omit-xml-declaration="yes" method="xml" version="1.0" />
  <xsl:template match="/">
    <xsl:apply-templates select="/CreditosFiscales" />
  </xsl:template>
  <xsl:template match="/CreditosFiscales">
    <xsl:variable name="var:v1" select="userCSharp:StringConcat(&quot;0&quot;)" />
    <xsl:variable name="var:v2" select="userCSharp:StringConcat(&quot;5&quot;)" />
    <xsl:variable name="var:v3" select="count(/CreditosFiscales/Conceptos/Concepto)" />
    <xsl:variable name="var:FormaPago" select="//FormaPago[1]"/>
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
        <xsl:if test="string-length(DatosGenerales/Nombre/text())>0">
          <Nombre>
            <xsl:value-of select="DatosGenerales/Nombre/text()" />
          </Nombre>
        </xsl:if>
        <xsl:if test="string-length(DatosGenerales/ApellidoPaterno/text())>0">
          <ApellidoPaterno>
            <xsl:value-of select="DatosGenerales/ApellidoPaterno/text()" />
          </ApellidoPaterno>
        </xsl:if>
        <xsl:if test="string-length(DatosGenerales/ApellidoMaterno/text())>0">
          <ApellidoMaterno>
            <xsl:value-of select="DatosGenerales/ApellidoMaterno/text()" />
          </ApellidoMaterno>
        </xsl:if>
        <xsl:if test="string-length(DatosGenerales/RazonSocial/text())>0">
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
        <NumeroParcialidad>
          <xsl:value-of select="DatosGenerales/NumeroParcialidad/text()" />
        </NumeroParcialidad>
        <LineasCaptura>
          <xsl:for-each select="DatosGenerales/LineasCaptura">
            <xsl:for-each select="DatosLineaCaptura">
              <xsl:variable name="var:v4" select="userCSharp:StringConcat(&quot;&quot;)" />
              <DatosLineaCaptura>
                <LineaCaptura>
                  <xsl:value-of select="$var:v4" />
                </LineaCaptura>
                <Importe>
                  <xsl:value-of select="Importe/text()" />
                </Importe>
                <FechaVigencia>
                  <xsl:value-of select="FechaVigencia/text()" />
                </FechaVigencia>
                <PagoObligadoInternet>
                  <xsl:value-of select="PagoObligadoInternet/text()" />
                </PagoObligadoInternet>
              </DatosLineaCaptura>
            </xsl:for-each>
          </xsl:for-each>
        </LineasCaptura>
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
              <xsl:choose>
                <xsl:when test="$var:FormaPago = '920048-4423'">
                  <xsl:value-of select="ImportePagar/text()"/>
                </xsl:when>
                <xsl:otherwise>0</xsl:otherwise>
              </xsl:choose>
            </CantidadPagar>
            <DatosIcep>
              <xsl:choose>
                <xsl:when test="Ejercicio/text() = 0">
                </xsl:when>
                <xsl:otherwise>
                  <xsl:if test="Ejercicio/text()">
                    <Ejercicio>
                      <xsl:value-of select="Ejercicio/text()" />
                    </Ejercicio>
                  </xsl:if>
                </xsl:otherwise>
              </xsl:choose>
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
              <xsl:if test="FechaCausacion/text()">
                <FechaCausacion>
                  <xsl:value-of select="FechaCausacion/text()" />
                </FechaCausacion>
              </xsl:if>
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
            </Transacciones>
          </Concepto>
        </xsl:for-each>
      </Conceptos>
    </SolicitudGeneracionLC>
  </xsl:template>
  <msxsl:script language="C#" implements-prefix="userCSharp"><![CDATA[
public string StringConcat(string param0)
{
   return param0;
}



]]></msxsl:script>
</xsl:stylesheet>