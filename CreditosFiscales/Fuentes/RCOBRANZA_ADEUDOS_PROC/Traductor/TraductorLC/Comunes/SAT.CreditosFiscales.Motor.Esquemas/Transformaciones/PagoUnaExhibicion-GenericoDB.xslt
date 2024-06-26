<?xml version="1.0" encoding="utf-16"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:output omit-xml-declaration="yes" method="xml" version="1.0" />
  <xsl:template match="/">
    <xsl:apply-templates select="/SolicitudCreditosFiscales/Mensaje" />
  </xsl:template>
  <xsl:template match="/SolicitudCreditosFiscales/Mensaje">
    <SolicitudGenerarLC>
      <DatosGenerales>
        <RFC>
          <xsl:value-of select="DatosGenerales/RFC/text()" />
        </RFC>
        <ALR>
          <xsl:value-of select="DatosGenerales/ALR/text()" />
        </ALR>
        <ImporteLC>
          <xsl:value-of select="DatosGenerales/LineaCaptura/Importe/text()" />
        </ImporteLC>
        <FechaVigenciaLC>
          <xsl:value-of select="DatosGenerales/LineaCaptura/FechaVigencia/text()" />
        </FechaVigenciaLC>
      </DatosGenerales>
      <xsl:if test="ResolucionesDeterminante/ResolucionDeterminante">
        <xsl:for-each select="ResolucionesDeterminante/ResolucionDeterminante">
          <Agrupador Valor="">
            <xsl:if test="Conceptos/Concepto">
              <xsl:for-each select="Conceptos/Concepto">
                <Clave>
                  <xsl:value-of select="Clave/text()" />
                </Clave>
                <Ejercicio>
                  <xsl:value-of select="Ejercicio/text()" />
                </Ejercicio>
                <Periodicidad>
                  <xsl:value-of select="PeriodicidadId/text()" />
                </Periodicidad>
                <PeriodoId>
                  <xsl:value-of select="PeriodoId/text()" />
                </PeriodoId>
                <FechaCausacion>
                  <xsl:value-of select="FechaCausacion/text()" />
                </FechaCausacion>
                <Transacciones>
                  <!--Inicia Transacciones padre-->
                  <Transaccion>
                    <Clave></Clave>
                    <Valor></Valor>
                  </Transaccion>
                  <!--Finaliza Trasacciones padre-->
                  
                  <!--Inicia Transacciones Hijo-->
                  <!--Finaliza Transacciones Hijo-->
                  
                </Transacciones>
              </xsl:for-each>
            </xsl:if>           
          </Agrupador>
        </xsl:for-each>
      </xsl:if>
    </SolicitudGenerarLC>
  </xsl:template>  
</xsl:stylesheet>