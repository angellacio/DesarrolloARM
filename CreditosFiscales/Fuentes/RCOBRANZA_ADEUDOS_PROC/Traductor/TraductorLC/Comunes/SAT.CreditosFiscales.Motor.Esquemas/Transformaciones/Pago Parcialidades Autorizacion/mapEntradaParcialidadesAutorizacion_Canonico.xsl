<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" exclude-result-prefixes="msxsl var userCSharp" version="1.0" xmlns:userCSharp="http://schemas.microsoft.com/BizTalk/2003/userCSharp">
  <xsl:output omit-xml-declaration="yes" method="xml" version="1.0" />
  <xsl:template match="/">
    <xsl:apply-templates select="/SolicitudCreditosFiscales" />
  </xsl:template>
  <xsl:template match="/SolicitudCreditosFiscales">
    <xsl:variable name="var:v1" select="userCSharp:StringConcat(&quot;0&quot;)"/>
    <CreditosFiscales>
      <DatosGenerales>
        <Folio>
          <xsl:value-of select="$var:v1"/>
        </Folio>
        <RFC>
          <xsl:value-of select="Mensaje/DatosGenerales/RFC/text()"/>
        </RFC>
        <Nombre>
          <xsl:value-of select="Mensaje/DatosGenerales/Nombre/text()"/>
        </Nombre>
        <ApellidoPaterno>
          <xsl:value-of select="Mensaje/DatosGenerales/ApellidoPaterno/text()"/>
        </ApellidoPaterno>
        <ApellidoMaterno>
          <xsl:value-of select="Mensaje/DatosGenerales/ApellidoMaterno/text()"/>
        </ApellidoMaterno>
        <xsl:if test="Mensaje/DatosGenerales/RazonSocial/text()">
          <RazonSocial>
            <xsl:value-of select="Mensaje/DatosGenerales/RazonSocial/text()"/>
          </RazonSocial>
        </xsl:if>
        <ALR>
          <xsl:value-of select="Mensaje/DatosGenerales/ALR/text()"/>
        </ALR>
        <IdDocumento>
          <xsl:value-of select="$var:v1"/>
        </IdDocumento>
        <TipoDocumento>
          <xsl:value-of select="Encabezado/TipoDocumento/text()"/>
        </TipoDocumento>
        <DescripcionDocumento>
          <xsl:value-of select="Encabezado/DescripcionTipoDocumento/text()"/>
        </DescripcionDocumento>
        <ImporteTotal>
          <xsl:value-of select="$var:v1"/>
        </ImporteTotal>
        <FechaEmision>
          <xsl:value-of select="$var:v1"/>
        </FechaEmision>
        <IdContribuyente>
          <xsl:value-of select="Mensaje/DatosGenerales/BOID/text()"/>
        </IdContribuyente>
        <IdAplicacion>
          <xsl:value-of select="Encabezado/Aplicacion/text()"/>
        </IdAplicacion>
        <LineasCaptura>
          <DatosLineaCaptura>
            <LineaCaptura>
              <xsl:value-of select="$var:v1"/>
            </LineaCaptura>
            <Importe>
              <xsl:value-of select="Mensaje/DatosGenerales/LineaCaptura/Importe/text()"/>
            </Importe>
            <FechaVigencia>
              <xsl:value-of select="Mensaje/DatosGenerales/LineaCaptura/FechaVigencia/text()"/>
            </FechaVigencia>
            <PagoObligadoInternet>
              <xsl:value-of select="Mensaje/DatosGenerales/LineaCaptura/PagoObligadoInternet/text()"/>
            </PagoObligadoInternet>
          </DatosLineaCaptura>
        </LineasCaptura>
        <xsl:if test="Mensaje/DatosGenerales/DeudorPuro/text()">
          <DeudorPuro>
            <xsl:value-of select="Mensaje/DatosGenerales/DeudorPuro/text()" />
          </DeudorPuro>
        </xsl:if>
        <NumeroParcialidad>
          <xsl:value-of select="Mensaje/DatosGenerales/NumeroParcialidad/text()" />
        </NumeroParcialidad>
        <xsl:if test="Encabezado/TipoOperacion/text()">
          <TipoOperacion>
            <xsl:value-of select="Encabezado/TipoOperacion/text()"/>
          </TipoOperacion>
        </xsl:if>
        <xsl:if test="Mensaje/DatosGenerales/Observaciones/text()">
          <Observaciones>
            <xsl:value-of select="Mensaje/DatosGenerales/Observaciones/text()"/>
          </Observaciones>
        </xsl:if>
      </DatosGenerales>
      <Agrupadores>
        <xsl:for-each select="Mensaje/ResolucionesDeterminante/ResolucionDeterminante">
          <Agrupador>
            <NumeroAgrupador>
              <xsl:value-of select="NumeroResolucion/text()"/>
            </NumeroAgrupador>
            <Fecha>
              <xsl:value-of select="Fecha/text()"/>
            </Fecha>
            <AutoridadId>
              <xsl:value-of select="AutoridadId/text()"/>
            </AutoridadId>
            <ALR>
              <xsl:value-of select="ALR/text()"/>
            </ALR>
            <RFC>
              <xsl:value-of select="RFC/text()"/>
            </RFC>
            <SaldoInformativo>
              <xsl:value-of select="SaldoInformativo/text()"/>
            </SaldoInformativo>
            <FormaPago>
              <xsl:value-of select="FormaPago/text()"/>
            </FormaPago>
            <Marcas>
              <xsl:for-each select="Marcas">
                <xsl:for-each select="Marca">
                  <Marca>
                    <xsl:if test="@TipoMarca">
                      <xsl:attribute name="TipoMarca">
                        <xsl:value-of select="@TipoMarca" />
                      </xsl:attribute>
                    </xsl:if>
                    <xsl:value-of select="./text()" />
                  </Marca>
                </xsl:for-each>
              </xsl:for-each>
            </Marcas>
            <ConceptosOriginal>
              <xsl:for-each select="Conceptos/Concepto">
                <xsl:variable name="var:ImporteTotalCargos" select="ImporteHistorico/text() + ImporteParteActualizada/text()"/>
                <xsl:variable name="var:Abonos" select="sum(Descuentos/Descuento/ImporteDescuento/text()) + ImportePagar/text()"/>
                <xsl:variable name="var:Descuentos" select="sum(Descuentos/Descuento/ImporteDescuento/text())"/>
                <xsl:variable name="var:Diferencias" select="$var:ImporteTotalCargos - sum(Descuentos/Descuento/ImporteDescuento/text())"/>
                <Conceptos>
                  <Clave>
                    <xsl:value-of select="Clave/text()"/>
                  </Clave>
                  <xsl:choose>
                    <xsl:when test="Ejercicio/text() = 0 or Ejercicio/text()=''">
                    </xsl:when>
                    <xsl:otherwise>
                      <xsl:if test="Ejercicio/text()">
                        <Ejercicio>
                          <xsl:value-of select="Ejercicio/text()" />
                        </Ejercicio>
                      </xsl:if>
                    </xsl:otherwise>
                  </xsl:choose>
                  <PeriodicidadId>
                    <xsl:value-of select="PeriodicidadId/text()"/>
                  </PeriodicidadId>
                  <PeriodoId>
                    <xsl:value-of select="PeriodoId/text()"/>
                  </PeriodoId>
                  <MotivoId>
                    <xsl:value-of select="MotivoId/text()"/>
                  </MotivoId>
                  <xsl:if test="FechaCausacion/text()">
                    <FechaCausacion>
                      <xsl:value-of select="FechaCausacion/text()"/>
                    </FechaCausacion>
                  </xsl:if>
                  <CreditoSir>
                    <xsl:value-of select="CreditoSir/text()"/>
                  </CreditoSir>
                  <xsl:if test="CredicoARCA">
                    <CredicoARCA>
                      <xsl:value-of select="CredicoARCA/text()"/>
                    </CredicoARCA>
                  </xsl:if>
                  <ImporteHistorico>
                    <xsl:value-of select="ImporteHistorico/text()"/>
                  </ImporteHistorico>
                  <ImporteParteActualizada>
                    <xsl:value-of select="ImporteParteActualizada/text()"/>
                  </ImporteParteActualizada>
                  <ImportePagar>
                    <xsl:choose>
                      <xsl:when test="../../FormaPago/text() = '920048-4423'">
                        <xsl:value-of select="ImportePagar/text()"/>
                      </xsl:when>
                      <xsl:otherwise>
                        <xsl:value-of select="ImportePagar/text()"/>
                      </xsl:otherwise>
                    </xsl:choose>
                  </ImportePagar>
                  <xsl:if test="FechaNotificacion">
                    <FechaNotificacion>
                      <xsl:value-of select="FechaNotificacion/text()"/>
                    </FechaNotificacion>
                  </xsl:if>
                  <Liquidar>
                    <xsl:value-of select="Liquidar/text()"/>
                  </Liquidar>
                  <ImporteTotalCargos>
                    <xsl:value-of select="$var:ImporteTotalCargos"/>
                  </ImporteTotalCargos>
                  <ImporteTotalAbonos>
                    <xsl:choose>
                      <xsl:when test="../../FormaPago/text() = '920048-4423'">
                        <xsl:value-of select="$var:Descuentos"/>
                      </xsl:when>
                      <xsl:otherwise>
                        <xsl:value-of select="$var:Abonos"/>
                      </xsl:otherwise>
                    </xsl:choose>
                  </ImporteTotalAbonos>
                  <DiferenciaTotales>
                    <xsl:choose>
                      <xsl:when test="../../FormaPago/text() = '920048-4423'">
                        <xsl:value-of select="$var:ImporteTotalCargos - sum(Descuentos/Descuento/ImporteDescuento/text())"/>
                      </xsl:when>
                      <xsl:otherwise>
                        <xsl:value-of select="$var:ImporteTotalCargos - $var:Abonos"/>
                      </xsl:otherwise>
                    </xsl:choose>
                  </DiferenciaTotales>
                  <FormaPago>
                    <xsl:value-of select="../../FormaPago/text()" />
                  </FormaPago>
                  <Marcas>
                    <xsl:for-each select="Marcas">
                      <xsl:for-each select="Marca">
                        <Marca>
                          <xsl:if test="@TipoMarca">
                            <xsl:attribute name="TipoMarca">
                              <xsl:value-of select="@TipoMarca" />
                            </xsl:attribute>
                          </xsl:if>
                          <xsl:value-of select="./text()" />
                        </Marca>
                      </xsl:for-each>
                    </xsl:for-each>
                  </Marcas>
                  <Descuentos>
                    <xsl:for-each select="Descuentos/Descuento">
                      <Descuento>
                        <IdDescuento>
                          <xsl:value-of select="IdDescuento/text()"/>
                        </IdDescuento>
                        <ImporteDescuento>
                          <xsl:value-of select="ImporteDescuento/text()"/>
                        </ImporteDescuento>
                        <xsl:value-of select="./text()"/>
                      </Descuento>
                    </xsl:for-each>
                  </Descuentos>
                  <Hijos>
                    <xsl:for-each select="Hijos">
                      <xsl:for-each select="Hijo">
                        <xsl:variable name="var:ImporteTotalCargosHijo" select="ImporteHistorico/text() + ImporteParteActualizada/text()"/>
                        <xsl:variable name="var:AbonosHijo" select="sum(Descuentos/Descuento/ImporteDescuento/text()) + ImportePagar/text()"/>
                        <xsl:variable name="var:DescuentosHijo" select="sum(Descuentos/Descuento/ImporteDescuento/text())"/>
                        <xsl:variable name="var:DiferenciasHijo" select="$var:ImporteTotalCargosHijo - sum(Descuentos/Descuento/ImporteDescuento/text())"/>
                        <Hijo>
                          <Clave>
                            <xsl:value-of select="Clave/text()"/>
                          </Clave>
                          <MotivoId>
                            <xsl:value-of select="MotivoId/text()"/>
                          </MotivoId>
                          <FechaCausacion>
                            <xsl:value-of select="FechaCausacion/text()"/>
                          </FechaCausacion>
                          <CreditoSir>
                            <xsl:value-of select="CreditoSir/text()"/>
                          </CreditoSir>
                          <ImporteHistorico>
                            <xsl:value-of select="ImporteHistorico/text()"/>
                          </ImporteHistorico>
                          <ImporteParteActualizada>
                            <xsl:value-of select="ImporteParteActualizada/text()"/>
                          </ImporteParteActualizada>
                          <ImportePagar>
                            <xsl:choose>
                              <xsl:when test="../../../../FormaPago/text() = '920048-4423'">
                                <xsl:value-of select="ImportePagar/text()"/>
                              </xsl:when>
                              <xsl:otherwise>
                                <xsl:value-of select="ImportePagar/text()"/>
                              </xsl:otherwise>
                            </xsl:choose>
                          </ImportePagar>
                          <xsl:if test="FechaNotificacion">
                            <FechaNotificacion>
                              <xsl:value-of select="FechaNotificacion/text()"/>
                            </FechaNotificacion>
                          </xsl:if>
                          <xsl:choose>
                            <xsl:when test="Ejercicio/text() = 0 or Ejercicio/text()=''">
                            </xsl:when>
                            <xsl:otherwise>
                              <xsl:if test="Ejercicio/text()">
                                <Ejercicio>
                                  <xsl:value-of select="Ejercicio/text()" />
                                </Ejercicio>
                              </xsl:if>
                            </xsl:otherwise>
                          </xsl:choose>
                          <PeriodicidadId>
                            <xsl:value-of select="PeriodicidadId/text()"/>
                          </PeriodicidadId>
                          <PeriodoId>
                            <xsl:value-of select="PeriodoId/text()"/>
                          </PeriodoId>
                          <Liquidar>
                            <xsl:value-of select="Liquidar/text()"/>
                          </Liquidar>
                          <ImporteTotalCargos>
                            <xsl:value-of select="$var:ImporteTotalCargosHijo"/>
                          </ImporteTotalCargos>
                          <ImporteTotalAbonos>
                            <xsl:choose>
                              <xsl:when test="../../../../FormaPago/text() = '920048-4423'">
                                <xsl:value-of select="$var:DescuentosHijo"/>
                              </xsl:when>
                              <xsl:otherwise>
                                <xsl:value-of select="$var:AbonosHijo"/>
                              </xsl:otherwise>
                            </xsl:choose>
                          </ImporteTotalAbonos>
                          <DiferenciaTotales>
                            <xsl:choose>
                              <xsl:when test="../../../../FormaPago/text() = '920048-4423'">
                                <xsl:value-of select="$var:ImporteTotalCargosHijo - sum(Descuentos/Descuento/ImporteDescuento/text())"/>
                              </xsl:when>
                              <xsl:otherwise>
                                <xsl:value-of select="$var:ImporteTotalCargosHijo - $var:AbonosHijo"/>
                              </xsl:otherwise>
                            </xsl:choose>
                          </DiferenciaTotales>
                          <FormaPago>
                            <xsl:value-of select="../../../../FormaPago/text()" />
                          </FormaPago>
                          <Marcas>
                            <xsl:for-each select="Marcas">
                              <xsl:for-each select="Marca">
                                <Marca>
                                  <xsl:if test="@TipoMarca">
                                    <xsl:attribute name="TipoMarca">
                                      <xsl:value-of select="@TipoMarca" />
                                    </xsl:attribute>
                                  </xsl:if>
                                  <xsl:value-of select="./text()" />
                                </Marca>
                              </xsl:for-each>
                            </xsl:for-each>
                          </Marcas>
                          <Descuentos>
                            <xsl:for-each select="Descuentos/Descuento">
                              <Descuento>
                                <IdDescuento>
                                  <xsl:value-of select="IdDescuento/text()"/>
                                </IdDescuento>
                                <ImporteDescuento>
                                  <xsl:value-of select="ImporteDescuento/text()"/>
                                </ImporteDescuento>
                                <xsl:value-of select="./text()"/>
                              </Descuento>
                            </xsl:for-each>
                          </Descuentos>
                          <xsl:value-of select="./text()"/>
                        </Hijo>
                      </xsl:for-each>
                    </xsl:for-each>
                  </Hijos>
                  <xsl:value-of select="./text()"/>
                </Conceptos>
              </xsl:for-each>
              <xsl:value-of select="Conceptos/text()"/>
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


public bool LogicalEq(string val1, string val2)
{
	bool ret = false;
	double d1 = 0;
	double d2 = 0;
	if (IsNumeric(val1, ref d1) && IsNumeric(val2, ref d2))
	{
		ret = d1 == d2;
	}
	else
	{
		ret = String.Compare(val1, val2, StringComparison.Ordinal) == 0;
	}
	return ret;
}


public bool IsNumeric(string val)
{
	if (val == null)
	{
		return false;
	}
	double d = 0;
	return Double.TryParse(val, System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out d);
}

public bool IsNumeric(string val, ref double d)
{
	if (val == null)
	{
		return false;
	}
	return Double.TryParse(val, System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out d);
}







]]>
  </msxsl:script>
</xsl:stylesheet>