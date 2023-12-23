<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" exclude-result-prefixes="msxsl var userCSharp" version="1.0" xmlns:userCSharp="http://schemas.microsoft.com/BizTalk/2003/userCSharp">
  <xsl:output omit-xml-declaration="yes" method="xml" version="1.0" />
  <xsl:template match="/">
    <xsl:apply-templates select="/SolicitudCreditosFiscales" />
  </xsl:template>
  <xsl:template match="/SolicitudCreditosFiscales">
    <xsl:variable name="var:v1" select="userCSharp:StringConcat(&quot;0&quot;)" />
    <CreditosFiscales>
      <DatosGenerales>
        <Folio>
          <xsl:value-of select="$var:v1" />
        </Folio>
        <RFC>
          <xsl:value-of select="Mensaje/DatosGenerales/RFC/text()" />
        </RFC>
        <xsl:if test="string-length(Mensaje/DatosGenerales/Nombre/text()) > 0">
          <Nombre>
            <xsl:value-of select="Mensaje/DatosGenerales/Nombre/text()" />
          </Nombre>
        </xsl:if>
        <xsl:if test="string-length(Mensaje/DatosGenerales/ApellidoPaterno/text()) > 0">
          <ApellidoPaterno>
            <xsl:value-of select="Mensaje/DatosGenerales/ApellidoPaterno/text()" />
          </ApellidoPaterno>
        </xsl:if>
        <xsl:if test="string-length(Mensaje/DatosGenerales/ApellidoMaterno/text()) > 0">
          <ApellidoMaterno>
            <xsl:value-of select="Mensaje/DatosGenerales/ApellidoMaterno/text()" />
          </ApellidoMaterno>
        </xsl:if>
        <xsl:if test="string-length(Mensaje/DatosGenerales/RazonSocial/text())">
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
          <xsl:value-of select="$var:v1" />
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
              <xsl:value-of select="Mensaje/DatosGenerales/LineaCaptura/Importe/text()" />
            </Importe>
            <FechaVigencia>
              <xsl:value-of select="Mensaje/DatosGenerales/LineaCaptura/FechaVigencia/text()" />
            </FechaVigencia>
            <PagoObligadoInternet>
              <xsl:value-of select="Mensaje/DatosGenerales/LineaCaptura/PagoObligadoInternet/text()" />
            </PagoObligadoInternet>
          </DatosLineaCaptura>
        </LineasCaptura>
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
              <xsl:value-of select="NumeroResolucion/text()" />
            </NumeroAgrupador>
            <Fecha>
              <xsl:value-of select="Fecha/text()" />
            </Fecha>
            <AutoridadId>
              <xsl:value-of select="AutoridadId/text()" />
            </AutoridadId>
            <RFC>
              <xsl:value-of select="RFC/text()" />
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
                    <xsl:value-of select="Clave/text()" />
                  </Clave>
                  <xsl:if test="Ejercicio">
                    <Ejercicio>
                      <xsl:value-of select="Ejercicio/text()" />
                    </Ejercicio>
                  </xsl:if>
                  <PeriodicidadId>
                    <xsl:value-of select="PeriodicidadId/text()" />
                  </PeriodicidadId>
                  <PeriodoId>
                    <xsl:value-of select="PeriodoId/text()" />
                  </PeriodoId>
                  <MotivoId>
                    <xsl:value-of select="MotivoId/text()" />
                  </MotivoId>
                  <xsl:if test="FechaCausacion">
                    <FechaCausacion>
                      <xsl:value-of select="FechaCausacion/text()" />
                    </FechaCausacion>
                  </xsl:if>
                  <CreditoSir>
                    <xsl:value-of select="CreditoSir/text()" />
                  </CreditoSir>
                  <xsl:if test="CredicoARCA">
                    <CredicoARCA>
                      <xsl:value-of select="CredicoARCA/text()" />
                    </CredicoARCA>
                  </xsl:if>
                  <ImporteSinPagoInicial>
                    <xsl:value-of select="$var:ImporteTotalCargos - ImportePagar/text()"/>
                  </ImporteSinPagoInicial>
                  <ImporteHistorico>
                    <xsl:value-of select="ImporteHistorico/text()" />
                  </ImporteHistorico>
                  <ImporteParteActualizada>
                    <xsl:value-of select="ImporteParteActualizada/text()" />
                  </ImporteParteActualizada>
                  <ImportePagar>
                    <xsl:value-of select="ImportePagar/text()" />
                  </ImportePagar>
                  <xsl:if test="FechaNotificacion">
                    <FechaNotificacion>
                      <xsl:value-of select="FechaNotificacion/text()" />
                    </FechaNotificacion>
                  </xsl:if>
                  <Liquidar>
                    <xsl:value-of select="Liquidar/text()" />
                  </Liquidar>
                  <ImporteTotalCargos>
                    <xsl:value-of select="$var:ImporteTotalCargos"/>
                  </ImporteTotalCargos>
                  <ImporteTotalAbonos>
                    <xsl:choose>
                      <xsl:when test="../../FormaPago/text() = 920048">
                        <xsl:value-of select="$var:Descuentos"/>
                      </xsl:when>
                      <xsl:otherwise>
                        <xsl:value-of select="$var:Abonos"/>
                      </xsl:otherwise>
                    </xsl:choose>
                  </ImporteTotalAbonos>
                  <DiferenciaTotales>
                    <xsl:choose>
                      <xsl:when test="../../FormaPago/text() = 920048">
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
                  <xsl:for-each select="Descuentos">
                    <Descuentos>
                      <xsl:for-each select="Descuento">
                        <Descuento>
                          <IdDescuento>
                            <xsl:value-of select="IdDescuento/text()" />
                          </IdDescuento>
                          <ImporteDescuento>
                            <xsl:value-of select="ImporteDescuento/text()" />
                          </ImporteDescuento>
                          <xsl:value-of select="./text()" />
                        </Descuento>
                      </xsl:for-each>
                      <xsl:value-of select="./text()" />
                    </Descuentos>
                  </xsl:for-each>
                  <xsl:for-each select="Hijos">
                    <Hijos>
                      <xsl:for-each select="Hijo">
                        <xsl:variable name="var:ImporteTotalCargosHijo" select="ImporteHistorico/text() + ImporteParteActualizada/text()"/>
                        <xsl:variable name="var:AbonosHijo" select="sum(Descuentos/Descuento/ImporteDescuento/text()) + ImportePagar/text()"/>
                        <xsl:variable name="var:DescuentosHijo" select="sum(Descuentos/Descuento/ImporteDescuento/text())"/>
                        <xsl:variable name="var:DiferenciasHijo" select="$var:ImporteTotalCargosHijo - sum(Descuentos/Descuento/ImporteDescuento/text())"/>
                        <Hijo>
                          <Clave>
                            <xsl:value-of select="Clave/text()" />
                          </Clave>
                          <MotivoId>
                            <xsl:value-of select="MotivoId/text()" />
                          </MotivoId>
                          <xsl:if test="FechaCausacion">
                            <FechaCausacion>
                              <xsl:value-of select="FechaCausacion/text()" />
                            </FechaCausacion>
                          </xsl:if>
                          <CreditoSir>
                            <xsl:value-of select="CreditoSir/text()" />
                          </CreditoSir>
                          <ImporteSinPagoInicial>
                            <xsl:value-of select="$var:ImporteTotalCargosHijo - ImportePagar/text()"/>
                          </ImporteSinPagoInicial>
                          <ImporteHistorico>
                            <xsl:value-of select="ImporteHistorico/text()" />
                          </ImporteHistorico>
                          <ImporteParteActualizada>
                            <xsl:value-of select="ImporteParteActualizada/text()" />
                          </ImporteParteActualizada>
                          <ImportePagar>
                            <xsl:value-of select="ImportePagar/text()" />
                          </ImportePagar>
                          <xsl:if test="FechaNotificacion">
                            <FechaNotificacion>
                              <xsl:value-of select="FechaNotificacion/text()" />
                            </FechaNotificacion>
                          </xsl:if>
                          <xsl:if test="Ejercicio">
                            <Ejercicio>
                              <xsl:value-of select="Ejercicio/text()" />
                            </Ejercicio>
                          </xsl:if>
                          <PeriodicidadId>
                            <xsl:value-of select="PeriodicidadId/text()" />
                          </PeriodicidadId>
                          <PeriodoId>
                            <xsl:value-of select="PeriodoId/text()" />
                          </PeriodoId>
                          <Liquidar>
                            <xsl:value-of select="Liquidar/text()" />
                          </Liquidar>
                          <ImporteTotalCargos>
                            <xsl:value-of select="$var:ImporteTotalCargosHijo"/>
                          </ImporteTotalCargos>
                          <ImporteTotalAbonos>
                            <xsl:choose>
                              <xsl:when test="../../../../FormaPago/text() = 920048">
                                <xsl:value-of select="$var:DescuentosHijo"/>
                              </xsl:when>
                              <xsl:otherwise>
                                <xsl:value-of select="$var:AbonosHijo"/>
                              </xsl:otherwise>
                            </xsl:choose>
                          </ImporteTotalAbonos>
                          <DiferenciaTotales>
                            <xsl:choose>
                              <xsl:when test="../../../../FormaPago/text() = 920048">
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
                          <xsl:for-each select="Descuentos">
                            <Descuentos>
                              <xsl:for-each select="Descuento">
                                <Descuento>
                                  <IdDescuento>
                                    <xsl:value-of select="IdDescuento/text()" />
                                  </IdDescuento>
                                  <ImporteDescuento>
                                    <xsl:value-of select="ImporteDescuento/text()" />
                                  </ImporteDescuento>
                                  <xsl:value-of select="./text()" />
                                </Descuento>
                              </xsl:for-each>
                              <xsl:value-of select="./text()" />
                            </Descuentos>
                          </xsl:for-each>
                          <xsl:value-of select="./text()" />
                        </Hijo>
                      </xsl:for-each>
                      <xsl:value-of select="./text()" />
                    </Hijos>
                  </xsl:for-each>
                </Conceptos>
              </xsl:for-each>
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


public string MathAdd(string param0, string param1)
{
	System.Collections.ArrayList listValues = new System.Collections.ArrayList();
	listValues.Add(param0);
	listValues.Add(param1);
	double ret = 0;
	foreach (string obj in listValues)
	{
	double d = 0;
		if (IsNumeric(obj, ref d))
		{
			ret += d;
		}
		else
		{
			return "";
		}
	}
	return ret.ToString(System.Globalization.CultureInfo.InvariantCulture);
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