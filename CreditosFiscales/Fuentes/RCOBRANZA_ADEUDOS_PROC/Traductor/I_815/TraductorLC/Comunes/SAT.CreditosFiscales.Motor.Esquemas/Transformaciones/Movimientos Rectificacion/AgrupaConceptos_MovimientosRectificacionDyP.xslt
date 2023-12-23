<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" exclude-result-prefixes="msxsl var s0 userCSharp" version="1.0" xmlns:ns0="http://MotorTraductor.RespuestaRegla" xmlns:s0="http://SAT.CreditosFiscales.Traductor" xmlns:userCSharp="http://schemas.microsoft.com/BizTalk/2003/userCSharp">
  <xsl:output omit-xml-declaration="yes" method="xml" version="1.0"/>
  <xsl:template match="/">
    <xsl:apply-templates select="SolicitudGeneracionLC"/>
  </xsl:template>
  <xsl:template match="SolicitudGeneracionLC">
    <xsl:for-each select="Conceptos/Concepto">
      <xsl:value-of select="userCSharp:AgrupaData(NumeroSecuencia/text(), Clave/text(), DatosIcep/FechaCausacion/text(), DatosIcep/ClavePeriodo/text(), DatosIcep/ClavePeriodicidad/text(), CantidadPagar/text())"/>
    </xsl:for-each>
    <SolicitudGeneracionLC>
      <DatosGenerales>
        <IdDocumento>
          <xsl:value-of select="DatosGenerales/IdDocumento/text()"/>
        </IdDocumento>
        <Solicitud>
          <xsl:value-of select="DatosGenerales/Solicitud/text()"/>
        </Solicitud>
        <IdContribuyente>
          <xsl:value-of select="DatosGenerales/IdContribuyente/text()"/>
        </IdContribuyente>
        <OrigenInformacion>
          <xsl:value-of select="DatosGenerales/OrigenInformacion/text()"/>
        </OrigenInformacion>
        <NumeroConceptos>
          <xsl:value-of select="userCSharp:ObtieneNoTotalConceptos()"/>
        </NumeroConceptos>
        <ImporteTotal>
          <xsl:value-of select="DatosGenerales/ImporteTotal/text()"/>
        </ImporteTotal>
        <FechaEmision>
          <xsl:value-of select="DatosGenerales/FechaEmision/text()"/>
        </FechaEmision>
        <FechaRecepcion>
          <xsl:value-of select="DatosGenerales/FechaRecepcion/text()"/>
        </FechaRecepcion>
        <RFC>
          <xsl:value-of select="DatosGenerales/RFC/text()"/>
        </RFC>
        <xsl:if test="DatosGenerales/Nombre">
          <Nombre>
            <xsl:value-of select="DatosGenerales/Nombre/text()" />
          </Nombre>
        </xsl:if>
        <xsl:if test="DatosGenerales/ApellidoPaterno">
          <ApellidoPaterno>
            <xsl:value-of select="DatosGenerales/ApellidoPaterno/text()" />
          </ApellidoPaterno>
        </xsl:if>
        <xsl:if test="DatosGenerales/ApellidoMaterno">
          <ApellidoMaterno>
            <xsl:value-of select="DatosGenerales/ApellidoMaterno/text()" />
          </ApellidoMaterno>
        </xsl:if>
        <xsl:if test="DatosGenerales/RazonSocial/text()">
          <RazonSocial>
            <xsl:value-of select="DatosGenerales/RazonSocial/text()"/>
          </RazonSocial>
        </xsl:if>
        <ClaveAlr>
          <xsl:value-of select="DatosGenerales/ClaveAlr/text()"/>
        </ClaveAlr>
        <TipoDocumento>
          <xsl:value-of select="DatosGenerales/TipoDocumento/text()"/>
        </TipoDocumento>
        <DescripcionTipoDocumento>
          <xsl:value-of select="DatosGenerales/DescripcionTipoDocumento/text()"/>
        </DescripcionTipoDocumento>
        <xsl:if test="DatosGenerales/NumeroParcialidad">
          <NumeroParcialidad>
            <xsl:value-of select="DatosGenerales/NumeroParcialidad/text()"/>
          </NumeroParcialidad>
        </xsl:if>
        <LineasCaptura>
          <xsl:for-each select="DatosGenerales/LineasCaptura">
            <xsl:for-each select="DatosLineaCaptura">
              <DatosLineaCaptura>
                <LineaCaptura/>
                <Importe>
                  <xsl:value-of select="Importe/text()"/>
                </Importe>
                <FechaVigencia>
                  <xsl:value-of select="FechaVigencia/text()"/>
                </FechaVigencia>
                <PagoObligadoInternet>
                  <xsl:value-of select="PagoObligadoInternet/text()"/>
                </PagoObligadoInternet>
              </DatosLineaCaptura>
            </xsl:for-each>
          </xsl:for-each>
        </LineasCaptura>
      </DatosGenerales>
      <xsl:for-each select="Conceptos">
        <Conceptos>
          <xsl:for-each select="Concepto">
            <xsl:call-template name="ObtieneConcepto"></xsl:call-template>
          </xsl:for-each>
        </Conceptos>
      </xsl:for-each>
    </SolicitudGeneracionLC>
  </xsl:template>
  <xsl:template name="ObtieneConcepto">
    <xsl:value-of select="userCSharp:InicializaEscribio('false')"/>
    <xsl:variable name="vConcepto" select="userCSharp:ObtieneConceptoActual()"/>
    <xsl:if test="string-length($vConcepto) > 0">
      <xsl:for-each select="../../Conceptos/Concepto">
        <xsl:variable name="vActual" select="concat(NumeroSecuencia/text(),'|', Clave/text(), '|', DatosIcep/FechaCausacion/text(), '|', DatosIcep/ClavePeriodo/text() ,'|', DatosIcep/ClavePeriodicidad/text())"/>
        <xsl:variable name="vCantidadActual" select="CantidadPagar/text()" />
        <xsl:if test="string(userCSharp:YaSeEscribio()) = 'false' and $vConcepto = $vActual ">
          <Concepto>
            <NumeroSecuencia>
              <xsl:value-of select="userCSharp:ObtieneNoSecuencia()"/>
            </NumeroSecuencia>
            <Clave>
              <xsl:value-of select="Clave/text()"/>
            </Clave>
            <Descripcion>
              <xsl:value-of select="Descripcion/text()"/>
            </Descripcion>
            <ClaveGenerica>
              <xsl:value-of select="ClaveGenerica/text()"/>
            </ClaveGenerica>
            <ImporteTotalAbonos>
              0
            </ImporteTotalAbonos>
            <ImporteTotalCargos>
              0
            </ImporteTotalCargos>
            <CantidadPagar>
              <xsl:value-of select="CantidadPagar/text()"/>
            </CantidadPagar>
            <DatosIcep>
              <ClavePeriodicidad>
                <xsl:value-of select="DatosIcep/ClavePeriodicidad/text()"/>
              </ClavePeriodicidad>
              <DescripcionPeriodicidad>
                <xsl:value-of select="DatosIcep/DescripcionPeriodicidad/text()"/>
              </DescripcionPeriodicidad>
              <ClavePeriodo>
                <xsl:value-of select="DatosIcep/ClavePeriodo/text()"/>
              </ClavePeriodo>
              <DescripcionPeriodo>
                <xsl:value-of select="DatosIcep/DescripcionPeriodo/text()"/>
              </DescripcionPeriodo>
              <xsl:if test="DatosIcep/Ejercicio/text()">
                <Ejercicio>
                  <xsl:value-of select="DatosIcep/Ejercicio/text()"/>
                </Ejercicio>
              </xsl:if>
              <xsl:if test="DatosIcep/FechaCausacion/text()">
                <FechaCausacion>
                  <xsl:value-of select="DatosIcep/FechaCausacion/text()"/>
                </FechaCausacion>
              </xsl:if>
            </DatosIcep>
            <xsl:value-of select="userCSharp:InicializaEscribio('true')"/>
            <xsl:call-template name="ObtieneTransacciones">
              <xsl:with-param name="pConcepto" select="$vConcepto"/>
            </xsl:call-template>
          </Concepto>
        </xsl:if>
      </xsl:for-each>
    </xsl:if>
  </xsl:template>
  <xsl:template name="ObtieneTransacciones">
    <xsl:param name="pConcepto"/>
    <xsl:value-of select="userCSharp:InicializaTransacciones()"/>
    <xsl:for-each select="../../Conceptos/Concepto">
      <xsl:variable name="vActual" select="concat(NumeroSecuencia/text(),'|', Clave/text(), '|', DatosIcep/FechaCausacion/text(), '|', DatosIcep/ClavePeriodo/text() ,'|', DatosIcep/ClavePeriodicidad/text())"/>
      <xsl:if test="$pConcepto = $vActual ">
        <xsl:for-each select="Transacciones/Transaccion">
          <xsl:value-of select="userCSharp:GeneraTransacciones(Clave/text(), Descripcion/text(), Valor/text(), Tipo/text())"/>
        </xsl:for-each>
      </xsl:if>
    </xsl:for-each>
    <xsl:value-of select="userCSharp:DevuelveTransacciones()" disable-output-escaping="yes"/>
  </xsl:template>
  <msxsl:script language="C#" implements-prefix="userCSharp">
    <![CDATA[      
  
        int consecutivo = 0;
        int noConceptos = 0;
        string conceptos = string.Empty;
        string transacciones = string.Empty;
        bool escrito = false;
        long nuevaCantidad = 0;

        public void AgrupaData(string numeroSecuencia, string clave, string ejercicio, string periodo, string periodicidad, string cantidad)
        {
            string[] arregloConceptos = conceptos.Split('~'); 
            string[] concepto = null; 
            string key = string.Format("{0}|{1}|{2}|{3}|{4}", numeroSecuencia, clave, ejercicio, periodo, periodicidad); 
            bool existeConcepto = false;
           

            if (conceptos.Length > 0)
            {
                foreach (string conceptoActual in arregloConceptos)
                {
                    concepto = conceptoActual.Split('|');
                
                    if (conceptoActual.Equals(key)) 
                    { 
                      nuevaCantidad = nuevaCantidad + long.Parse(cantidad);
                      existeConcepto = true; break; 
                    }
                    else
                    {
                      nuevaCantidad = long.Parse(cantidad);
                    }
                }

                if (!existeConcepto)
                {
                    nuevaCantidad = long.Parse(cantidad);
                    conceptos = string.Format("{0}~{1}", conceptos, key);
                    noConceptos++;
                }
            }
            else
            {
                conceptos = key; noConceptos++;
            }
        }


        public int ObtieneNoTotalConceptos()
        {
            return noConceptos;
        }
        
        public long ObtieneNuevoImporte()
        {
          return nuevaCantidad;
        }
        
        public string ObtieneConceptos() { return conceptos; }        
        
        public string ObtieneConceptoActual() { string[] arregloConceptos = conceptos.Split('~'); string valor = string.Empty; if (arregloConceptos != null && arregloConceptos.Length > 0) { if (consecutivo < arregloConceptos.Length) { valor = arregloConceptos[consecutivo]; consecutivo++; } } return valor; }       
        
        public bool YaSeEscribio() { return escrito; }       
        
        public void InicializaEscribio(string valor) { escrito = bool.Parse(valor); }

        public void GeneraTransacciones(string clave, string descripcion, string importe, string tipo)
        {
            string[] arregloTransacciones = null; 
            string[] transaccion = null; 
            long nuevoImporte = 0; 
            bool transaccionExistente = false;

            if (transacciones.Length > 0)
            {
                arregloTransacciones = transacciones.Split('~');

                foreach (string transaccionActual in arregloTransacciones)
                {
                    transaccion = transaccionActual.Split('|');

                    if (transaccion[0].Equals(clave))
                    {
                        nuevoImporte = long.Parse(transaccion[2]) + long.Parse(importe);

                        transaccionExistente = true;
                        transacciones = transacciones.Replace(transaccionActual, string.Format("{0}|{1}|{2}|{3}", clave, descripcion, nuevoImporte.ToString(), tipo));
                        break;
                    }
                }
                if (!transaccionExistente)
                {
                    transacciones = string.Format("{0}~{1}|{2}|{3}|{4}", transacciones, clave, descripcion, importe, tipo);
                }
            }
            else
            {
                transacciones = string.Format("{0}|{1}|{2}|{3}", clave, descripcion, importe, tipo);
            }
        }

        public string DevuelveTransacciones()
        {
            string[] arregloTransacciones = null; 
            string[] transaccion = null; 
            string xml = string.Empty; 
            string formatoTransaccion = "<Transaccion><Clave>{0}</Clave><Descripcion>{1}</Descripcion><Valor>{2}</Valor><Tipo>{3}</Tipo></Transaccion>"; 
            string formatoTransacciones = "<Transacciones>{0}</Transacciones>"; 
            arregloTransacciones = transacciones.Split('~');
            foreach (string transaccionActual in arregloTransacciones)
            {
                transaccion = transaccionActual.Split('|'); 
                xml = string.Format("{0}{1}", xml, string.Format(formatoTransaccion, transaccion[0], transaccion[1], transaccion[2], transaccion[3]));
            } 
            return string.Format(formatoTransacciones, xml);
        }        
        
        public void InicializaTransacciones() { transacciones = string.Empty; }        
        
        public int ObtieneNoSecuencia() { return consecutivo; }
                  
  ]]>
  </msxsl:script>
</xsl:stylesheet>