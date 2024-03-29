<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" exclude-result-prefixes="msxsl var userCSharp" version="1.0" xmlns:userCSharp="http://schemas.microsoft.com/BizTalk/2003/userCSharp">
  <xsl:output omit-xml-declaration="yes" method="xml" version="1.0" />
  <xsl:template match="/">
    <xsl:apply-templates select="/MovimientoRectificacion" />
  </xsl:template>
  <xsl:template match="/MovimientoRectificacion">
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
          </DatosLineaCaptura>
        </LineasCaptura>
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
        <SolicitudARectificar>
          <xsl:value-of select="Mensaje/ResolucionesDeterminante/ResolucionDeterminante/Conceptos/SolicitudARectificar/text()" />
        </SolicitudARectificar>
      </DatosGenerales>
      <Agrupadores>
        <xsl:for-each select="Mensaje/ResolucionesDeterminante/ResolucionDeterminante">
          <xsl:variable name="var:FormaDePago" select="FormaPago/text()"/>
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
            <FormaPago>
              <xsl:value-of select="$var:FormaDePago" />
            </FormaPago>
            <ConceptosOriginal>              
              <xsl:if test="count(Conceptos/MovimientosContables) > 0">
                <Conceptos>
                  <AutoridadId>
                    <xsl:value-of select="AutoridadId/text()" />
                  </AutoridadId>
                  <Clave>
                    <xsl:value-of select="Conceptos/MovimientosContables/Correcto/Clave/text()" />
                  </Clave>
                  <PeriodicidadId>
                    <xsl:value-of select="Conceptos/MovimientosContables/Correcto/PeriodicidadId/text()" />
                  </PeriodicidadId>
                  <PeriodoId>
                    <xsl:value-of select="Conceptos/MovimientosContables/Correcto/PeriodoId/text()" />
                  </PeriodoId>
                  <MotivoId>
                    <xsl:value-of select="Conceptos/MovimientosContables/Correcto/MotivoId/text()" />
                  </MotivoId>
                  <FechaCausacion>
                    <xsl:value-of select="Conceptos/MovimientosContables/Correcto/FechaCausacion/text()" />
                  </FechaCausacion>
                  <CreditoSir>
                    <xsl:value-of select="Conceptos/MovimientosContables/Correcto/CreditoSir/text()" />
                  </CreditoSir>
                  <xsl:if test="Conceptos/MovimientosContables/Correcto/CreditoARCA">
                    <CredicoARCA>
                      <xsl:value-of select="Conceptos/MovimientosContables/Correcto/CreditoARCA/text()" />
                    </CredicoARCA>
                  </xsl:if>
                  <ImporteHistorico>
                    <xsl:value-of select="Conceptos/MovimientosContables/Correcto/ImporteOperacion/text()" />
                  </ImporteHistorico>
                  <FormaPago>
                    <xsl:value-of select="$var:FormaDePago" />
                  </FormaPago>
                  <EsCorrecto>true</EsCorrecto>
                  <TipoMovimiento>C</TipoMovimiento>
                </Conceptos>
                <Conceptos>
                  <AutoridadId>
                    <xsl:value-of select="AutoridadId/text()" />
                  </AutoridadId>
                  <Clave>
                    <xsl:value-of select="Conceptos/MovimientosContables/Erroneo/Clave/text()" />
                  </Clave>
                  <PeriodicidadId>
                    <xsl:value-of select="Conceptos/MovimientosContables/Erroneo/PeriodicidadId/text()" />
                  </PeriodicidadId>
                  <PeriodoId>
                    <xsl:value-of select="Conceptos/MovimientosContables/Erroneo/PeriodoId/text()" />
                  </PeriodoId>
                  <MotivoId>
                    <xsl:value-of select="Conceptos/MovimientosContables/Erroneo/MotivoId/text()" />
                  </MotivoId>
                  <FechaCausacion>
                    <xsl:value-of select="Conceptos/MovimientosContables/Erroneo/FechaCausacion/text()" />
                  </FechaCausacion>
                  <CreditoSir>
                    <xsl:value-of select="Conceptos/MovimientosContables/Erroneo/CreditoSir/text()" />
                  </CreditoSir>
                  <xsl:if test="Conceptos/MovimientosContables/Erroneo/CreditoARCA">
                    <CredicoARCA>
                      <xsl:value-of select="Conceptos/MovimientosContables/Erroneo/CreditoARCA/text()" />
                    </CredicoARCA>
                  </xsl:if>
                  <ImporteHistorico>
                    <xsl:value-of select="Conceptos/MovimientosContables/Erroneo/ImporteOperacion/text()" />
                  </ImporteHistorico>
                  <FormaPago>
                    <xsl:value-of select="$var:FormaDePago" />
                  </FormaPago>
                  <EsCorrecto>false</EsCorrecto>
                  <TipoMovimiento>C</TipoMovimiento>
                </Conceptos>
              </xsl:if>
              <xsl:if test="count(Conceptos/Rectificaciones) > 0">
                <xsl:variable name="var:ImporteTotalCargosCorrecto" select="Conceptos/Rectificaciones/Correcto/ImporteHistorico/text() + Conceptos/Rectificaciones/Correcto/ImporteParteActualizada/text()"/>
                <xsl:variable name="var:AbonosCorrecto" select="sum(Conceptos/Rectificaciones/Correcto/Descuentos/Descuento/ImporteDescuento/text()) + Conceptos/Rectificaciones/Correcto/ImportePagar/text()"/>
                <xsl:variable name="var:DescuentosCorrecto" select="sum(Conceptos/Rectificaciones/Correcto/Descuentos/Descuento/ImporteDescuento/text())"/>
                <xsl:variable name="var:DiferenciasCorrecto" select="$var:ImporteTotalCargosCorrecto - sum(Conceptos/Rectificaciones/Correcto/Descuentos/Descuento/ImporteDescuento/text())"/>
                <Conceptos>
                  <AutoridadId>
                    <xsl:value-of select="AutoridadId/text()" />
                  </AutoridadId>
                  <Clave>
                    <xsl:value-of select="Conceptos/Rectificaciones/Correcto/Clave/text()" />
                  </Clave>
                  <xsl:if test="Conceptos/Rectificaciones/Correcto/Ejercicio">
                    <Ejercicio>
                      <xsl:value-of select="Conceptos/Rectificaciones/Correcto/Ejercicio/text()" />
                    </Ejercicio>
                  </xsl:if>
                  <PeriodicidadId>
                    <xsl:value-of select="Conceptos/Rectificaciones/Correcto/PeriodicidadId/text()" />
                  </PeriodicidadId>
                  <PeriodoId>
                    <xsl:value-of select="Conceptos/Rectificaciones/Correcto/PeriodoId/text()" />
                  </PeriodoId>
                  <MotivoId>
                    <xsl:value-of select="Conceptos/Rectificaciones/Correcto/MotivoId/text()" />
                  </MotivoId>
                  <xsl:if test="Conceptos/Rectificaciones/Correcto/FechaCausacion">
                    <FechaCausacion>
                      <xsl:value-of select="Conceptos/Rectificaciones/Correcto/FechaCausacion/text()" />
                    </FechaCausacion>
                  </xsl:if>
                  <CreditoSir>
                    <xsl:value-of select="Conceptos/Rectificaciones/Correcto/CreditoSir/text()" />
                  </CreditoSir>
                  <xsl:if test="Conceptos/Rectificaciones/Correcto/CredicoARCA">
                    <CredicoARCA>
                      <xsl:value-of select="Conceptos/Rectificaciones/Correcto/CredicoARCA/text()" />
                    </CredicoARCA>
                  </xsl:if>
                  <ImporteHistorico>
                    <xsl:value-of select="Conceptos/Rectificaciones/Correcto/ImporteHistorico/text()" />
                  </ImporteHistorico>
                  <ImporteParteActualizada>
                    <xsl:value-of select="Conceptos/Rectificaciones/Correcto/ImporteParteActualizada/text()" />
                  </ImporteParteActualizada>
                  <xsl:if test="Conceptos/Rectificaciones/Correcto/PagosAnteriores">
                    <PagosAnteriores>
                      <xsl:value-of select="Conceptos/Rectificaciones/Correcto/PagosAnteriores/text()" />
                    </PagosAnteriores>
                  </xsl:if>
                  <ImportePagar>
                    <xsl:choose>
                      <xsl:when test="$var:FormaDePago = '920048-4423'">
                        <xsl:value-of select="Conceptos/Rectificaciones/Correcto/ImportePagar/text()" />
                      </xsl:when>
                      <xsl:otherwise>
                        <xsl:value-of select="Conceptos/Rectificaciones/Correcto/ImportePagar/text()" />
                      </xsl:otherwise>
                    </xsl:choose>                    
                  </ImportePagar>
                  <xsl:if test="Conceptos/Rectificaciones/Correcto/FechaNotificacion">
                    <FechaNotificacion>
                      <xsl:value-of select="Conceptos/Rectificaciones/Correcto/FechaNotificacion/text()" />
                    </FechaNotificacion>
                  </xsl:if>
                  <Liquidar>
                    <xsl:value-of select="Conceptos/Rectificaciones/Correcto/Liquidar/text()" />
                  </Liquidar>
                  <ImporteTotalCargos>
                    <xsl:value-of select="$var:ImporteTotalCargosCorrecto"/>
                  </ImporteTotalCargos>
                  <ImporteTotalAbonos>
                    <xsl:choose>
                      <xsl:when test="$var:FormaDePago = '920048-4423'">
                        <xsl:value-of select="$var:DescuentosCorrecto"/>
                      </xsl:when>
                      <xsl:otherwise>
                        <xsl:value-of select="$var:AbonosCorrecto"/>
                      </xsl:otherwise>
                    </xsl:choose>
                  </ImporteTotalAbonos>
                  <DiferenciaTotales>
                    <xsl:choose>
                      <xsl:when test="$var:FormaDePago = '920048-4423'">
                        <xsl:value-of select="$var:ImporteTotalCargosCorrecto - sum(Descuentos/Descuento/ImporteDescuento/text())"/>
                      </xsl:when>
                      <xsl:otherwise>
                        <xsl:value-of select="$var:ImporteTotalCargosCorrecto - $var:AbonosCorrecto"/>
                      </xsl:otherwise>
                    </xsl:choose>
                  </DiferenciaTotales>
                  <FormaPago>
                    <xsl:value-of select="$var:FormaDePago" />
                  </FormaPago>
                  <xsl:for-each select="Conceptos/Rectificaciones/Correcto/Descuentos">
                    <Descuentos>
                      <xsl:for-each select="Descuento">
                        <Descuento>
                          <IdDescuento>
                            <xsl:value-of select="IdDescuento/text()" />
                          </IdDescuento>
                          <ImporteDescuento>
                            <xsl:value-of select="ImporteDescuento/text()" />
                          </ImporteDescuento>
                        </Descuento>
                      </xsl:for-each>
                    </Descuentos>
                  </xsl:for-each>
                  <xsl:for-each select="Conceptos/Rectificaciones/Correcto/Hijos">
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
                              <xsl:when test="$var:FormaDePago = '920048-4423'">
                                <xsl:value-of select="$var:DescuentosHijo"/>
                              </xsl:when>
                              <xsl:otherwise>
                                <xsl:value-of select="$var:AbonosHijo"/>
                              </xsl:otherwise>
                            </xsl:choose>
                          </ImporteTotalAbonos>
                          <DiferenciaTotales>
                            <xsl:choose>
                              <xsl:when test="$var:FormaDePago = '920048-4423'">
                                <xsl:value-of select="$var:ImporteTotalCargosHijo - sum(Descuentos/Descuento/ImporteDescuento/text())"/>
                              </xsl:when>
                              <xsl:otherwise>
                                <xsl:value-of select="$var:ImporteTotalCargosHijo - $var:AbonosHijo"/>
                              </xsl:otherwise>
                            </xsl:choose>
                          </DiferenciaTotales>
                          <FormaPago>
                            <xsl:value-of select="$var:FormaDePago" />
                          </FormaPago>
                          <xsl:for-each select="Marcas">
                            <Marcas>
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
                              <xsl:value-of select="./text()" />
                            </Marcas>
                          </xsl:for-each>
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
                        </Hijo>
                      </xsl:for-each>
                    </Hijos>
                  </xsl:for-each>
                  <EsCorrecto>true</EsCorrecto>
                  <TipoMovimiento>R</TipoMovimiento>
                </Conceptos>
                <Conceptos>
                  <xsl:variable name="var:ImporteTotalCargosErroneo" select="Conceptos/Rectificaciones/Erroneo/ImporteHistorico/text() + Conceptos/Rectificaciones/Erroneo/ImporteParteActualizada/text()"/>
                  <xsl:variable name="var:AbonosErroneo" select="sum(Conceptos/Rectificaciones/Erroneo/Descuentos/Descuento/ImporteDescuento/text()) + Conceptos/Rectificaciones/Erroneo/ImportePagar/text()"/>
                  <xsl:variable name="var:DescuentosErroneo" select="sum(Conceptos/Rectificaciones/Erroneo/Descuentos/Descuento/ImporteDescuento/text())"/>
                  <xsl:variable name="var:DiferenciasErroneo" select="$var:ImporteTotalCargosCorrecto - sum(Conceptos/Rectificaciones/Erroneo/Descuentos/Descuento/ImporteDescuento/text())"/>
                  <AutoridadId>
                    <xsl:value-of select="AutoridadId/text()" />
                  </AutoridadId>
                  <Clave>
                    <xsl:value-of select="Conceptos/Rectificaciones/Erroneo/Clave/text()" />
                  </Clave>
                  <xsl:if test="Conceptos/Rectificaciones/Erroneo/Ejercicio">
                    <Ejercicio>
                      <xsl:value-of select="Conceptos/Rectificaciones/Erroneo/Ejercicio/text()" />
                    </Ejercicio>
                  </xsl:if>
                  <PeriodicidadId>
                    <xsl:value-of select="Conceptos/Rectificaciones/Erroneo/PeriodicidadId/text()" />
                  </PeriodicidadId>
                  <PeriodoId>
                    <xsl:value-of select="Conceptos/Rectificaciones/Erroneo/PeriodoId/text()" />
                  </PeriodoId>
                  <MotivoId>
                    <xsl:value-of select="Conceptos/Rectificaciones/Erroneo/MotivoId/text()" />
                  </MotivoId>
                  <xsl:if test="Conceptos/Rectificaciones/Erroneo/FechaCausacion">
                    <FechaCausacion>
                      <xsl:value-of select="Conceptos/Rectificaciones/Erroneo/FechaCausacion/text()" />
                    </FechaCausacion>
                  </xsl:if>
                  <CreditoSir>
                    <xsl:value-of select="Conceptos/Rectificaciones/Erroneo/CreditoSir/text()" />
                  </CreditoSir>
                  <xsl:if test="Conceptos/Rectificaciones/Erroneo/CredicoARCA">
                    <CredicoARCA>
                      <xsl:value-of select="Conceptos/Rectificaciones/Erroneo/CredicoARCA/text()" />
                    </CredicoARCA>
                  </xsl:if>
                  <ImporteHistorico>
                    <xsl:value-of select="Conceptos/Rectificaciones/Erroneo/ImporteHistorico/text()" />
                  </ImporteHistorico>
                  <ImporteParteActualizada>
                    <xsl:value-of select="Conceptos/Rectificaciones/Erroneo/ImporteParteActualizada/text()" />
                  </ImporteParteActualizada>
                  <xsl:if test="Conceptos/Rectificaciones/Erroneo/PagosAnteriores">
                    <PagosAnteriores>
                      <xsl:value-of select="Conceptos/Rectificaciones/Erroneo/PagosAnteriores/text()" />
                    </PagosAnteriores>
                  </xsl:if>
                  <ImportePagar>
                    <xsl:value-of select="Conceptos/Rectificaciones/Erroneo/ImportePagar/text()" />
                  </ImportePagar>
                  <xsl:if test="Conceptos/Rectificaciones/Erroneo/FechaNotificacion">
                    <FechaNotificacion>
                      <xsl:value-of select="Conceptos/Rectificaciones/Erroneo/FechaNotificacion/text()" />
                    </FechaNotificacion>
                  </xsl:if>
                  <Liquidar>
                    <xsl:value-of select="Conceptos/Rectificaciones/Erroneo/Liquidar/text()" />
                  </Liquidar>
                  <ImporteTotalCargos>
                    <xsl:value-of select="$var:ImporteTotalCargosErroneo"/>
                  </ImporteTotalCargos>
                  <ImporteTotalAbonos>
                    <xsl:choose>
                      <xsl:when test="$var:FormaDePago = '920048-4423'">
                        <xsl:value-of select="$var:DescuentosErroneo"/>
                      </xsl:when>
                      <xsl:otherwise>
                        <xsl:value-of select="$var:AbonosErroneo"/>
                      </xsl:otherwise>
                    </xsl:choose>
                  </ImporteTotalAbonos>
                  <DiferenciaTotales>
                    <xsl:choose>
                      <xsl:when test="$var:FormaDePago = '920048-4423'">
                        <xsl:value-of select="$var:ImporteTotalCargosErroneo - sum(Descuentos/Descuento/ImporteDescuento/text())"/>
                      </xsl:when>
                      <xsl:otherwise>
                        <xsl:value-of select="$var:ImporteTotalCargosErroneo - $var:AbonosErroneo"/>
                      </xsl:otherwise>
                    </xsl:choose>
                  </DiferenciaTotales>
                  <FormaPago>
                    <xsl:value-of select="$var:FormaDePago" />
                  </FormaPago>
                  <xsl:for-each select="Conceptos/Rectificaciones/Erroneo/Descuentos">
                    <Descuentos>
                      <xsl:for-each select="Descuento">
                        <Descuento>
                          <IdDescuento>
                            <xsl:value-of select="IdDescuento/text()" />
                          </IdDescuento>
                          <ImporteDescuento>
                            <xsl:value-of select="ImporteDescuento/text()" />
                          </ImporteDescuento>
                        </Descuento>
                      </xsl:for-each>
                    </Descuentos>
                  </xsl:for-each>
                  <xsl:for-each select="Conceptos/Rectificaciones/Erroneo/Hijos">
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
                              <xsl:when test="$var:FormaDePago = '920048-4423'">
                                <xsl:value-of select="$var:DescuentosHijo"/>
                              </xsl:when>
                              <xsl:otherwise>
                                <xsl:value-of select="$var:AbonosHijo"/>
                              </xsl:otherwise>
                            </xsl:choose>
                          </ImporteTotalAbonos>
                          <DiferenciaTotales>
                            <xsl:choose>
                              <xsl:when test="$var:FormaDePago = '920048-4423'">
                                <xsl:value-of select="$var:ImporteTotalCargosHijo - sum(Descuentos/Descuento/ImporteDescuento/text())"/>
                              </xsl:when>
                              <xsl:otherwise>
                                <xsl:value-of select="$var:ImporteTotalCargosHijo - $var:AbonosHijo"/>
                              </xsl:otherwise>
                            </xsl:choose>
                          </DiferenciaTotales>
                          <FormaPago>
                            <xsl:value-of select="$var:FormaDePago" />
                          </FormaPago>
                          <xsl:for-each select="Marcas">
                            <Marcas>
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
                              <xsl:value-of select="./text()" />
                            </Marcas>
                          </xsl:for-each>
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
                        </Hijo>
                      </xsl:for-each>
                    </Hijos>
                  </xsl:for-each>
                  <EsCorrecto>false</EsCorrecto>
                  <TipoMovimiento>R</TipoMovimiento>
                </Conceptos>
              </xsl:if>
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