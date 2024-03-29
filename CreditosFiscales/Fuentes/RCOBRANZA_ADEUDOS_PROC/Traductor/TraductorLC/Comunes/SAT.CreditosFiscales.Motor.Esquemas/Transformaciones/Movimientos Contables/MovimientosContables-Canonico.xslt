<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" exclude-result-prefixes="msxsl var userCSharp" version="1.0" xmlns:userCSharp="http://schemas.microsoft.com/BizTalk/2003/userCSharp">
  <xsl:output omit-xml-declaration="yes" method="xml" version="1.0" />
  <xsl:template match="/">
    <xsl:apply-templates select="/MovimientoContable" />
  </xsl:template>
  <xsl:template match="/MovimientoContable">
    <xsl:variable name="var:v1" select="userCSharp:StringConcat(&quot;0&quot;)" />
    <CreditosFiscales>
      <DatosGenerales>
        <Folio>
          <xsl:value-of select="$var:v1" />
        </Folio>
        <RFC>
          <xsl:value-of select="Mensaje/DatosGenerales/RFC/text()" />
        </RFC>
        <xsl:if test="Mensaje/DatosGenerales/Nombre">
          <Nombre>
            <xsl:value-of select="Mensaje/DatosGenerales/Nombre/text()" />
          </Nombre>
        </xsl:if>
        <xsl:if test="Mensaje/DatosGenerales/ApellidoPaterno">
          <ApellidoPaterno>
            <xsl:value-of select="Mensaje/DatosGenerales/ApellidoPaterno/text()" />
          </ApellidoPaterno>
        </xsl:if>
        <xsl:if test="Mensaje/DatosGenerales/ApellidoMaterno">
          <ApellidoMaterno>
            <xsl:value-of select="Mensaje/DatosGenerales/ApellidoMaterno/text()" />
          </ApellidoMaterno>
        </xsl:if>
        <xsl:if test="Mensaje/DatosGenerales/RazonSocial">
          <RazonSocial>
            <xsl:value-of select="Mensaje/DatosGenerales/RazonSocial/text()" />
          </RazonSocial>
        </xsl:if>
        <ALR>
          <xsl:value-of select="Mensaje/DatosGenerales/ALR/text()" />
        </ALR>
        <IdDocumento>
          <xsl:value-of select="$var:v1" />
        </IdDocumento>
        <TipoDocumento>
          <xsl:value-of select="Encabezado/TipoDocumento/text()" />
        </TipoDocumento>
        <DescripcionDocumento>
          <xsl:value-of select="Encabezado/DescripcionTipoDocumento/text()" />
        </DescripcionDocumento>
        <ImporteTotal>
          <xsl:value-of select="Mensaje/DatosGenerales/ImporteTotal/text()" />
        </ImporteTotal>
        <FechaEmision>
          <xsl:value-of select="$var:v1" />
        </FechaEmision>
        <IdContribuyente>
          <xsl:value-of select="Mensaje/DatosGenerales/BOID/text()" />
        </IdContribuyente>
        <IdAplicacion>
          <xsl:value-of select="Encabezado/Aplicacion/text()" />
        </IdAplicacion>
        <LineasCaptura>
          <DatosLineaCaptura>
            <LineaCaptura>
              <xsl:value-of select="$var:v1" />
            </LineaCaptura>
            <Importe>
              <xsl:value-of select="$var:v1" />
            </Importe>
          </DatosLineaCaptura>
        </LineasCaptura>
        <xsl:if test="Mensaje/DatosGenerales/NumeroParcialidad">
          <NumeroParcialidad>
            <xsl:value-of select="Mensaje/DatosGenerales/NumeroParcialidad/text()" />
          </NumeroParcialidad>
        </xsl:if>
        <xsl:if test="Encabezado/TipoOperacion">
          <TipoOperacion>
            <xsl:value-of select="Encabezado/TipoOperacion/text()" />
          </TipoOperacion>
        </xsl:if>
        <DeudorPuro>
          <xsl:value-of select="Mensaje/DatosGenerales/DeudorPuro/text()" />
        </DeudorPuro>
        <xsl:if test="Mensaje/DatosGenerales/Observaciones">
          <Observaciones>
            <xsl:value-of select="Mensaje/DatosGenerales/Observaciones/text()" />
          </Observaciones>
        </xsl:if>
      </DatosGenerales>
      <Agrupadores>
        <xsl:for-each select="Mensaje/ResolucionesDeterminante/ResolucionDeterminante">
          <Agrupador>
            <NumeroAgrupador>
              <xsl:value-of select="NumeroResolucion/text()" />
            </NumeroAgrupador>
            <Fecha>
              <xsl:value-of select="Fecha/text()" />
            </Fecha>
            <AutoridadId>
              <xsl:value-of select="AutoridadId/text()" />
            </AutoridadId>
            <ALR>
              <xsl:value-of select="ALR/text()" />
            </ALR>
            <RFC>
              <xsl:value-of select="RFC/text()" />
            </RFC>
            <SaldoInformativo>
              <xsl:value-of select="SaldoInformativo/text()" />
            </SaldoInformativo>
            <FormaPago>
              <xsl:value-of select="FormaPago/text()" />
            </FormaPago>
            <ConceptosOriginal>
              <Conceptos>
                <AutoridadId>
                  <xsl:value-of select="AutoridadId/text()" />
                </AutoridadId>
                <Clave>
                  <xsl:value-of select="Conceptos/Concepto/Clave/text()" />
                </Clave>
                <PeriodicidadId>
                  <xsl:value-of select="Conceptos/Concepto/PeriodicidadId/text()" />
                </PeriodicidadId>
                <PeriodoId>
                  <xsl:value-of select="Conceptos/Concepto/PeriodoId/text()" />
                </PeriodoId>
                <MotivoId>
                  <xsl:value-of select="Conceptos/Concepto/MotivoId/text()" />
                </MotivoId>
                <FechaCausacion>
                  <xsl:value-of select="Conceptos/Concepto/FechaCausacion/text()" />
                </FechaCausacion>
                <CreditoSir>
                  <xsl:value-of select="Conceptos/Concepto/CreditoSir/text()" />
                </CreditoSir>
                <xsl:if test="Conceptos/Concepto/CredicoARCA">
                  <CredicoARCA>
                    <xsl:value-of select="Conceptos/Concepto/CredicoARCA/text()" />
                  </CredicoARCA>
                </xsl:if>
                <ImporteHistorico>
                  <xsl:value-of select="Conceptos/Concepto/ImporteOperacion/text()" />
                </ImporteHistorico>
                <ImportePagar>
                  <xsl:value-of select="Conceptos/Concepto/ImporteOperacion/text()" />
                </ImportePagar>
                <FormaPago>
                  <xsl:value-of select="FormaPago/text()" />
                </FormaPago>
              </Conceptos>
            </ConceptosOriginal>
          </Agrupador>
        </xsl:for-each>
      </Agrupadores>
    </CreditosFiscales>
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