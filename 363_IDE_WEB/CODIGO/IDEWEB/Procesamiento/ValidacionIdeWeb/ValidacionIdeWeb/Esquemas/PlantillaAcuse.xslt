<?xml version="1.0" encoding="iso-8859-1"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
    <xsl:output omit-xml-declaration="yes" method="html"/>
	
	<!--ACUSE ACEPTACION DECLARACION ANUAL-->
    <xsl:template match="/AcuseAceptacionAnualIDE">
	<xsl:variable name="ejer"> <xsl:value-of select="@ejercicio"/> </xsl:variable>
        <table width="1200" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td valign="top" width="100%">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tr>
                                        <td width="25%">
										<!--image align="left" height="106" width="130" src="https://www.acuse.sat.gob.mx/REIMPRESIONINTERNET/images/REIMaguila.gif"> </image-->
										<image align="left" height="106" width="130" src="http://aplicacionesinternasidc1.mat.sat.gob.mx:85/images/REIMaguila.gif"> </image>
										</td>
                                        <td width="75%" valign="bottom">
                                            <table align="left" height="107" width="450" cellspacing="0" cellpadding="0" border="0">
                                                <tr>
                                                    <td height="20" width="93%" align="center">
                                                            <h4>SERVICIO DE ADMINISTRACIÓN TRIBUTARIA</h4>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td height="20" width="93%" align="center">
                                                            <h4>ACUSE DE ACEPTACIÓN</h4>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td height="30" width="93%" align="center">
                                                            <h3>
																<xsl:choose>
																	<xsl:when test="floor('2014')&gt;floor($ejer)">
																		DECLARACIÓN INFORMATIVA ANUAL DEL IMPUESTOS A LOS DEPOSITOS EN EFECTVIVO
																	</xsl:when>
																	<xsl:otherwise>
																		DECLARACIÓN INFORMATIVA ANUAL A LOS DEPOSITOS EN EFECTIVO
																	</xsl:otherwise>
																</xsl:choose>														
															</h3>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <hr />
                            </td>
                        </tr>
					    <tr>
							<td><b>
								<xsl:choose>
									<xsl:when test="floor('2014')&gt;floor($ejer)">
										ACUSE DE RECIBO DE ACEPTACIÓN DE LA DECLARACIÓN INFORMATIVA ANUAL DEL IMPUESTOS A LOS DEPOSITOS EN EFECTVIVO
									</xsl:when>
									<xsl:otherwise>
										ACUSE DE RECIBO DE ACEPTACIÓN DE LA DECLARACIÓN INFORMATIVA ANUAL A LOS DEPOSITOS EN EFECTIVO
									</xsl:otherwise>
								</xsl:choose>
							</b></td>
							</tr>
						<tr>
							<td>&#160; </td>
						</tr>
                        <tr>
                            <td>
                                <table border="0" width="100%" cellpadding="0" cellspacing="00">
                                    <tr>
                                        <th width="40%" align="left">
											<b>R.F.C.:</b> 
                                        </th>
                                        <td width="60%" align="left">
											<xsl:value-of select="@rfc"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th align="left">
											<b>Nombre, Denominación o Razón Social:</b>
										</th>
                                        <td align="left">
                                            <xsl:value-of select="@denominacion"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th align="left">
											<b>Fecha de Presentación:</b>
										</th>
                                        <td align="left">
											<xsl:call-template name="formatDate">
												<xsl:with-param name="dateTime" select="@fechaPresentacion" />
											</xsl:call-template>											
                                        </td>
                                    </tr>
                                    <tr>
                                        <th align="left">
											<b>Hora de Presentación:</b>
										</th>
                                        <td align="left">
											<xsl:call-template name="formatTime">
												<xsl:with-param name="dateTime" select="@fechaPresentacion" />
											</xsl:call-template>											
                                        </td>
                                    </tr>
                                    <tr>
                                        <th align="left">
											<b>Folio de Recepción:</b>
										</th>
                                        <td>
                                            <xsl:value-of select="@folioRecepcion"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th align="left">
											<b>Número de Operación:</b>
										</th>
                                        <td align="left">
                                            <xsl:value-of select="@numeroOperacion"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th align="left">
											<b>Nombre del Archivo Enviado:</b>
										</th>
                                        <td align="left">
                                            <xsl:value-of select="@nombreArchivo"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th align="left">
											<b>Ejercicio Fiscal:</b>
										</th>
                                        <td align="left">
											<xsl:value-of select="@ejercicio"/>
										</td>
                                    </tr>
                                    <tr>
                                        <th align="left">
											<b>Tamaño del Archivo:</b>
										</th>
                                        <td align="left">
                                            <xsl:value-of select="concat(@tamanoArchivo, ' Bytes')"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th align="left">
											<b>Tipo de Declaración:</b>
										</th>
                                        <td align="left">
											<xsl:value-of select="@tipo"/>
										</td>
                                    </tr>
									<xsl:if test="floor('2014')&gt;floor($ejer)">
												<tr> 
												<th align="left">
													<b>Total Recaudado:</b>
												</th>
												<td align="left">
													<xsl:value-of select="@totalRecaudado"/>
												</td>
											</tr>
											<tr>
												<th align="left">
													<b>Total Enterado:</b>
												</th>
												<td align="left">
													<xsl:value-of select="@totalEnterado"/>
												</td>
											</tr>
									</xsl:if>

									<tr>
										<th align="left">
											<b>Periodo:</b>
										</th>
										<td align="left">
											Anual
										</td>
									</tr>
									<tr>
										<th align="left">
											<b>Fecha y Hora de Emisión de este Acuse:</b>
										</th>
										<td align="left">
											<xsl:value-of select="@fechaHoraEmisionAcuse"/>
										</td>
									</tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <hr />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td width="100%">
                    <table border="0" width="100%">
						<tr>
							<td></td>
						</tr>
                        <tr>
                            <td>
                                <b>Cadena Original:</b>
                            </td>
                        </tr>
						<tr>
							<td></td>
						</tr>
						<tr>
							<td><xsl:value-of select="@cadenaOriginal"/></td>
						</tr>
                        <tr>
                            <td>
                                <hr/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Sello Digital:</b>
                            </td>
                        </tr>
					    <!--tr>
							<td><xsl:value-of select="@sello"/></td>
						</tr-->
						<xsl:for-each select="@sello">
                            <tr>
                                <td>
                                    <div>
                                        <xsl:call-template name="Cortadora">
                                            <xsl:with-param name="strInput" select="concat('||', ., '||')" />
                                            <xsl:with-param name="nLen" select="120" />
                                        </xsl:call-template>
                                    </div>
                                </td>
                            </tr>
                        </xsl:for-each>
						<tr>
							<td></td>
						</tr>
                        <tr>
                            <td></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td width="1002">
                    <table border="0" width="100%">
                        <tr>
                            <td>
                                <hr/>
                            </td>
                        </tr>
                        <tr>
						<td> &#160; </td>
						</tr>
						<tr>
							<td>Sus datos personales son incorporados y protegidos en los sistemas del SAT, de conformidad 
							con los Lineamientos de Protección de Datos Personales y con las diversas disposiciones fiscales 
							y legales sobre confidencialidad y protección de datos, a fin de ejercer las facultades conferidas 
							a la autoridad fiscal.</td>
						</tr>
						<tr>
						<td> &#160; </td>
						</tr>
						<tr>
							<td>Si desea modificar o corregir sus datos personales puede acudir a la Administración Desconsentrada de 
							Servicios al Contribuyente que le corresponda y/o a través de la dirección www.sat.gob.mx.</td>
						</tr>
                    </table>
                </td>
            </tr>
        </table>
    </xsl:template>

	<!--ACUSE ACEPTACION DECLARACION MENSUAL-->
	<xsl:template match="/AcuseAceptacionMensualIDE">
	<xsl:variable name="ejer"> <xsl:value-of select="@ejercicio"/> </xsl:variable>	
		<table width="1200" border="0" cellspacing="0" cellpadding="0">
			<tr>
				<td valign="top" width="100%">
					<table width="100%" border="0" cellspacing="0" cellpadding="0">
						<tr>
							<td>
								<table cellspacing="0" cellpadding="0" width="100%" border="0">
									<tr>
										<td width="25%">
										<!--image align="left" height="106" width="130" src="https://www.acuse.sat.gob.mx/REIMPRESIONINTERNET/images/REIMaguila.gif"> </image-->
										<image align="left" height="106" width="130" src="http://aplicacionesinternasidc1.mat.sat.gob.mx:85/images/REIMaguila.gif"> </image>
										</td>
										<td width="75%" valign="bottom">
											<table align="left" height="107" width="450" cellspacing="0" cellpadding="0" border="0">
												<tr>
													<td height="20" width="93%" align="center">
															<h4>SERVICIO DE ADMINISTRACIÓN TRIBUTARIA</h4>
													</td>
												</tr>
												<tr>
													<td height="20" width="93%" align="center">
															<h4>ACUSE DE ACEPTACIÓN</h4>
													</td>
												</tr>
												<tr>
													<td height="30" width="93%" align="center">
															<h3>
																<xsl:choose>
																	<xsl:when test="floor('2014')&gt;floor($ejer)">
																		DECLARACIÓN INFORMATIVA MENSUAL DEL IMPUESTOS A LOS DEPOSITOS EN EFECTVIVO
																	</xsl:when>
																	<xsl:otherwise>
																		DECLARACIÓN INFORMATIVA MENSUAL A LOS DEPOSITOS EN EFECTIVO
																	</xsl:otherwise>
																</xsl:choose>
															</h3>
													</td>
												</tr>
											</table>
										</td>
									</tr>
								</table>
							</td>
						</tr>
						 <tr>
                            <td>
                                <hr />
                            </td>
                        </tr>
						
						<tr>
						<td><b>
							<xsl:choose>
								<xsl:when test="floor('2014')&gt;floor($ejer)">
									ACUSE DE RECIBO DE ACEPTACIÓN DE LA DECLARACIÓN INFORMATIVA MENSUAL DEL IMPUESTOS A LOS DEPOSITOS EN EFECTVIVO
								</xsl:when>
								<xsl:otherwise>
									ACUSE DE RECIBO DE ACEPTACIÓN DE LA DECLARACIÓN INFORMATIVA MENSUAL A LOS DEPOSITOS EN EFECTIVO
								</xsl:otherwise>
							</xsl:choose>
						</b></td>
						</tr>
						<tr>
							<td>&#160; </td> 
						</tr>
						<tr>
							<td>
								<table border="0" width="100%" cellpadding="0" cellspacing="00">
									<tr>
										<th width="40%" align="left">
											<b>R.F.C.:</b>
										</th>
										<td width="60%" align="left">
											<xsl:value-of select="@rfc"/>
										</td>
									</tr>
									<tr>
										<th align="left">
											<b>Nombre, Denominación o Razón Social:</b>
										</th>
										<td align="left">
											<xsl:value-of select="@denominacion"/>
										</td>
									</tr>
									<tr>
										<th align="left">
											<b>Fecha de Presentación:</b>
										</th>
										<td align="left">
											<xsl:call-template name="formatDate">
												<xsl:with-param name="dateTime" select="@fechaPresentacion" />
											</xsl:call-template>
										</td>
									</tr>
									<tr>
										<th align="left">
											<b>Hora de Presentación:</b>
										</th>
										<td align="left">
											<xsl:call-template name="formatTime">
												<xsl:with-param name="dateTime" select="@fechaPresentacion" />
											</xsl:call-template>
										</td>
									</tr>
									<tr>
										<th align="left">
											<b>Folio de Recepción:</b>
										</th>
										<td>
											<xsl:value-of select="@folioRecepcion"/>
										</td>
									</tr>
									<tr>
										<th align="left">
											<b>Número de Operación:</b>
										</th>
										<td align="left">
											<xsl:value-of select="@numeroOperacion"/>
										</td>
									</tr>
									<tr>
										<th align="left">
											<b>Nombre del Archivo Enviado:</b>
										</th>
										<td align="left">
											<xsl:value-of select="@nombreArchivo"/>
										</td>
									</tr>
									<tr>
										<th align="left">
											<b>Ejercicio Fiscal:</b>
										</th>
										<td align="left">
											<xsl:value-of select="@ejercicio"/>
										</td>
									</tr>
									<tr>
										<th align="left">
											<b>Tamaño del Archivo:</b>
										</th>
										<td align="left">
											<xsl:value-of select="concat(@tamanoArchivo, ' Bytes')"/>
										</td>
									</tr>
									<tr>
										<th align="left">
											<b>Tipo de Declaración:</b>
										</th>
										<td align="left">
											<xsl:value-of select="@tipo"/>
										</td>
									</tr>
									<tr>
										<th align="left">
											<b>Periodo:</b>
										</th>
										<td align="left">
											<xsl:value-of select="@periodo"/>
										</td>
									</tr>
									<xsl:if test="floor('2014')&gt;floor($ejer)">
												<tr> 
												<th align="left">
													<b>Total Recaudado:</b>
												</th>
												<td align="left">
													<xsl:value-of select="@totalRecaudado"/>
												</td>
											</tr>
											<tr>
												<th align="left">
													<b>Total Enterado:</b>
												</th>
												<td align="left">
													<xsl:value-of select="@totalEnterado"/>
												</td>
											</tr>
									</xsl:if>
									<tr>
										<th align="left">
											<b>Fecha y Hora de Emisión de este Acuse:</b>
										</th>
										<td align="left">
											<xsl:value-of select="@fechaHoraEmisionAcuse"/>
										</td>
									</tr>
								</table>
							</td>
						</tr>
						<tr>
							<td>
								<hr />
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td width="100%">
					<table border="0" width="100%">
						<tr>
							<td></td>
						</tr>
                        <tr>
                            <td>
                                <b>Cadena Original:</b>
                            </td>
                        </tr>
						<tr>
							<td></td>
						</tr>
						<tr>
							<td><xsl:value-of select="@cadenaOriginal"/></td>
						</tr>
                        <tr>
                            <td>
                                <hr/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Sello Digital:</b>
                            </td>
                        </tr>
						<tr>
							<td></td>
						</tr>
						 <xsl:for-each select="@sello">
                            <tr>
                                <td>
                                    <div>
                                        <xsl:call-template name="Cortadora">
                                            <xsl:with-param name="strInput" select="concat('||', ., '||')" />
                                            <xsl:with-param name="nLen" select="120" />
                                        </xsl:call-template>
                                    </div>
                                </td>
                            </tr>
                        </xsl:for-each>
                        <tr>
                            <td></td>
                        </tr>
					</table>
				</td>
			</tr>
			<tr>
				<td width="100%">
					  <table border="0" width="100%">
                        <tr>
                            <td>
                                <hr/>
                            </td>
                        </tr>
                        <tr>
						<td> &#160; </td>
						</tr>
						<tr>
							<td>Sus datos personales son incorporados y protegidos en los sistemas del SAT, de conformidad 
							con los Lineamientos de Protección de Datos Personales y con las diversas disposiciones fiscales 
							y legales sobre confidencialidad y protección de datos, a fin de ejercer las facultades conferidas 
							a la autoridad fiscal.</td>
						</tr>
						<tr>
						<td> &#160; </td>
						</tr>
						<tr>
							<td>Si desea modificar o corregir sus datos personales puede acudir a la Administración Desconsentrada de 
							Servicios al Contribuyente que le corresponda y/o a través de la dirección www.sat.gob.mx.</td>
						</tr>
                    </table>
				</td>
			</tr>
		</table>
	</xsl:template>

	<!--ACUSE RECHAZO DECLARACION ANUAL-->
	<xsl:template match="/AcuseRechazoAnualIDE">
	<xsl:variable name="ejer"> <xsl:value-of select="@ejercicio"/> </xsl:variable>
		<table width="1200" border="0" cellspacing="0" cellpadding="0">
			<tr>
				<td valign="top" width="1002">
					<table width="100%" border="0" cellspacing="0" cellpadding="0">
						<tr>
							<td>
								<table cellspacing="0" cellpadding="0" width="100%" border="0">
									<tr>
										<td width="25%">
										<image align="left" height="106" width="130" src="http://aplicacionesinternasidc1.mat.sat.gob.mx:85/images/REIMaguila.gif"> </image>
										</td>
										<td width="75%" valign="bottom">
											<table align="left" height="107" width="450" cellspacing="0" cellpadding="0" border="0">
												<tr>
													<td height="20" width="93%" align="center">
															<h4>SERVICIO DE ADMINISTRACIÓN TRIBUTARIA</h4>
													</td>
												</tr>
												<tr>
													<td height="20" width="93%" align="center">
															<h4>NOTIFICACIÓN DE RECHAZO</h4>
													</td>
												</tr>
												<tr>
													<td height="30" width="93%" align="center">
															<h3>
																<xsl:choose>
																	<xsl:when test="floor('2014')&gt;floor($ejer)">
																		DECLARACIÓN INFORMATIVA ANUAL DEL IMPUESTOS A LOS DEPOSITOS EN EFECTVIVO
																	</xsl:when>
																	<xsl:otherwise>
																		DECLARACIÓN INFORMATIVA ANUAL A LOS DEPOSITOS EN EFECTIVO
																	</xsl:otherwise>
																</xsl:choose>	
															</h3>
													</td>
												</tr>
											</table>
										</td>
									</tr>
								</table>
							</td>
						</tr>
						<tr>
							<td>
								<hr />
							</td>
						</tr>
						<tr>
							<td>
								<table border="0" width="100%" cellpadding="0" cellspacing="00">
								<tr>
								<td colspan="2"><b>Estimado Contribuyente:</b></td><td></td>
								</tr>
								<tr>
								<td colspan="2"> &#160; </td>
								</tr>
								<tr>
								<td colspan="2"><b>Su información fue rechazada por la o las siguientes inconsistencias:</b><br></br></td><td></td><br></br>
								</tr>
								<tr>
								<td colspan="2"> &#160; </td>
								</tr>
								<tr>
								 <td colspan="2">
									<table border="0" width="100%">
										<tr>
											<td></td><td></td>
										</tr>
										<tr>
											<td>
												<b>MOTIVOS DE RECHAZO</b>
											</td>
											<td></td>
										</tr>
										<tr>
											<td></td><td></td>
										</tr>
										<xsl:for-each select="/AcuseRechazoAnualIDE/Error">
											<tr>
												
												<td colspan="2">
													<LI width="100%">
														<xsl:value-of select="@descripcion"/>
													</LI>
												</td>
												
											</tr>
										</xsl:for-each>
									</table>
								 </td>
								</tr>
									<tr>
									<td> &#160; </td>
									</tr>
									<tr>
									<td colspan="2"><b>Realice las correcciones necesarias y vuelva a enviar su Declaración.</b></td><td></td><br></br>
									</tr> 
									<tr>
									<td> &#160; </td>
									</tr> 									
									<tr>
										<th width="40%" align="left">
											<b>R.F.C.:</b>
										</th>
										<td width="60%" align="left">
											<xsl:value-of select="@rfc"/>
										</td>
									</tr>
									<tr>
										<th align="left">
											<b>Nombre, Denominación o Razón Social:</b>
										</th>
										<td align="left">
											<xsl:value-of select="@denominacion"/>
										</td>
									</tr>
									<tr>
										<th align="left">
											<b>Fecha de Presentación:</b>
										</th>
										<td align="left">
											<xsl:call-template name="formatDate">
												<xsl:with-param name="dateTime" select="@fechaPresentacion" />
											</xsl:call-template>
										</td>
									</tr>
									<tr>
										<th align="left">
											<b>Hora de Presentación:</b>
										</th>
										<td align="left">
											<xsl:call-template name="formatTime">
												<xsl:with-param name="dateTime" select="@fechaPresentacion" />
											</xsl:call-template>
										</td>
									</tr>
									<tr>
										<th align="left">
											<b>Folio de Recepción:</b>
										</th>
										<td>
											<xsl:value-of select="@folioRecepcion"/>
										</td>
									</tr>									
									<tr>
										<th align="left">
											<b>Nombre del Archivo Enviado:</b>
										</th>
										<td align="left">
											<xsl:value-of select="@nombreArchivo"/>
										</td>
									</tr>
									<tr>
										<th align="left">
											<b>Ejercicio Fiscal:</b>
										</th>
										<td align="left">
											<xsl:value-of select="@ejercicio"/>
										</td>
									</tr>
									<tr>
										<th align="left">
											<b>Tamaño del Archivo:</b>
										</th>
										<td align="left">
											<xsl:value-of select="concat(@tamanoArchivo, ' Bytes')"/>
										</td>
									</tr>
									<tr>
										<th align="left">
											<b>Tipo de Declaración:</b>
										</th>
										<td align="left">
											<xsl:value-of select="@tipo"/>
										</td>
									</tr>
									<tr>
										<th align="left">
											<b>Periodo:</b>
										</th>
										<td align="left">
											Anual
										</td>
									</tr>
									<tr>
										<th align="left">
											<b>Fecha y Hora de Emisión de este Acuse:</b>
										</th>
										<td align="left">
											<xsl:value-of select="@fechaRechazo"/>
										</td>
									</tr>
									<tr>
									<td colspan="2">
											<hr />
										</td>
									</tr>						
								</table>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
			</tr>
			<tr>
				<td width="1002">
					<table border="0" width="100%">
						<tr>
						<td> &#160; </td>
						</tr>
						<tr>
						<td> &#160; </td>
						</tr>
						<tr>
							<td>Sus datos personales son incorporados y protegidos en los sistemas del SAT, de conformidad 
							con los Lineamientos de Protección de Datos Personales y con las diversas disposiciones fiscales 
							y legales sobre confidencialidad y protección de datos, a fin de ejercer las facultades conferidas 
							a la autoridad fiscal.</td>
						</tr>
						<tr>
						<td> &#160; </td>
						</tr>
						<tr>
							<td>Si desea modificar o corregir sus datos personales puede acudir a la Administración Desconsentrada de 
							Servicios al Contribuyente que le corresponda y/o a través de la dirección www.sat.gob.mx.</td>
						</tr>
						<tr>
							<td>
								<hr/>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
	</xsl:template>

	<!--ACUSE RECHAZO DECLARACION MENSUAL-->
	<xsl:template match="/AcuseRechazoMensualIDE">
	<xsl:variable name="ejer"> <xsl:value-of select="@ejercicio"/> </xsl:variable>
		<table width="1200" border="0" cellspacing="0" cellpadding="0">
			<tr>
				<td valign="top" width="100%">
					<table width="100%" border="0" cellspacing="0" cellpadding="0">
						<tr>
							<td>
								<table cellspacing="0" cellpadding="0" width="100%" border="0">
									<tr>
										<td width="25%"><image align="left" height="106" width="130" src="http://aplicacionesinternasidc1.mat.sat.gob.mx:85/images/REIMaguila.gif"> </image></td>
										<td width="75%" valign="bottom">
											<table align="left" height="107" width="450" cellspacing="0" cellpadding="0" border="0">
												<tr>
													<td height="20" width="93%" align="center">
															<h4>SERVICIO DE ADMINISTRACIÓN TRIBUTARIA</h4>
													</td>
												</tr>
												<tr>
													<td height="20" width="93%" align="center">
															<h4>NOTIFICACIÓN DE RECHAZO</h4>
													</td>
												</tr>
												<tr>
													<td height="30" width="93%" align="center">
															<h3>
															    <xsl:choose>
																	<xsl:when test="floor('2014')&gt;floor($ejer)">
																		DECLARACIÓN INFORMATIVA MENSUAL DEL IMPUESTOS A LOS DEPOSITOS EN EFECTVIVO
																	</xsl:when>
																	<xsl:otherwise>
																		DECLARACIÓN INFORMATIVA MENSUAL A LOS DEPOSITOS EN EFECTIVO
																	</xsl:otherwise>
																</xsl:choose>
															</h3>
													</td>
												</tr>
											</table>
										</td>
									</tr>
								</table>
							</td>
						</tr>
						<tr>
							<td>
								<hr />
							</td>
						</tr>
						<tr>
							<td>
								<table border="0" width="100%" cellpadding="0" cellspacing="00">
								<tr>
								<td colspan="2"><b>Estimado Contribuyente:</b></td><td></td>
								</tr>
								<tr>
								<td colspan="2"> &#160; </td>
								</tr>
								<tr>
								<td colspan="2"><b>Su información fue rechazada por la o las siguientes inconsistencias:</b><br></br></td><td></td><br></br>
								</tr>
								<tr>
								<td colspan="2"> &#160; </td>
								</tr>
								<tr>
								 <td colspan="2">
									<table border="0" width="100%">
										<tr>
											<td></td><td></td>
										</tr>
										<tr>
											<td>
												<b>MOTIVOS DE RECHAZO</b>
											</td>
											<td></td>
										</tr>
										<tr>
											<td></td><td></td>
										</tr>
										<xsl:for-each select="/AcuseRechazoMensualIDE/Error">
											<tr>
												
												<td colspan="2">
													<LI width="100%">
														<xsl:value-of select="@descripcion"/>
													</LI>
												</td>
												
											</tr>
										</xsl:for-each>
									</table>
								 </td>
								</tr>
								
									<tr>
									<td> &#160; </td>
									</tr>
									<tr>
									<td colspan="2"><b>Realice las correcciones necesarias y vuelva a enviar su Declaración.</b></td><td></td><br></br>
									</tr> 
									<tr>
									<td> &#160; </td>
									</tr>
								
									<tr>
										<th width="40%" align="left">
											<b>R.F.C.:</b>
										</th>
										<td width="60%" align="left">
											<xsl:value-of select="@rfc"/>
										</td>
									</tr>
									<tr>
										<th align="left">
											<b>Nombre, Denominación o Razón Social:</b>
										</th>
										<td align="left">
											<xsl:value-of select="@denominacion"/>
										</td>
									</tr>
									<tr>
										<th align="left">
											<b>Fecha de Presentación:</b>
										</th>
										<td align="left">
											<xsl:call-template name="formatDate">
												<xsl:with-param name="dateTime" select="@fechaPresentacion" />
											</xsl:call-template>
										</td>
									</tr>
									<tr>
										<th align="left">
											<b>Hora de Presentación:</b>
										</th>
										<td align="left">
											<xsl:call-template name="formatTime">
												<xsl:with-param name="dateTime" select="@fechaPresentacion" />
											</xsl:call-template>
										</td>
									</tr>
									<tr>
										<th align="left">
											<b>Folio de Recepción:</b>
										</th>
										<td>
											<xsl:value-of select="@folioRecepcion"/>
										</td>
									</tr>
									<tr>
										<th align="left">
											<b>Nombre del Archivo Enviado:</b>
										</th>
										<td align="left">
											<xsl:value-of select="@nombreArchivo"/>
										</td>
									</tr>
									<tr>
										<th align="left">
											<b>Ejercicio Fiscal:</b>
										</th>
										<td align="left">
											<xsl:value-of select="@ejercicio"/>
										</td>
									</tr>
									<tr>
										<th align="left">
											<b>Tamaño del Archivo:</b>
										</th>
										<td align="left">
											<xsl:value-of select="concat(@tamanoArchivo, ' Bytes')"/>
										</td>
									</tr>
									<tr>
										<th align="left">
											<b>Tipo de Declaración:</b>
										</th>
										<td align="left">
											<xsl:value-of select="@tipo"/>
										</td>
									</tr>
									<tr>
										<th align="left">
											<b>Periodo:</b>
										</th>
										<td align="left">
											<xsl:value-of select="@periodo"/>
										</td>
									</tr>
									<tr>
										<th align="left">
											<b>Fecha y Hora de Emisión de este Acuse:</b>
										</th>
										<td align="left">
											<xsl:value-of select="@fechaRechazo"/>
										</td>
									</tr>
									<tr>
									<td colspan="2">
											<hr />
										</td>
									</tr>
								</table>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
			</tr>
			<tr>
				<td width="1002">
					<table border="0" width="100%">
						<tr>
						<td> &#160; </td>
						</tr>
						<tr>
						<td> &#160; </td>
						</tr>
						<tr>
							<td>Sus datos personales son incorporados y protegidos en los sistemas del SAT, de conformidad 
							con los Lineamientos de Protección de Datos Personales y con las diversas disposiciones fiscales 
							y legales sobre confidencialidad y protección de datos, a fin de ejercer las facultades conferidas 
							a la autoridad fiscal.</td>
						</tr>
						<tr>
						<td> &#160; </td>
						</tr>
						<tr>
							<td>Si desea modificar o corregir sus datos personales puede acudir a la Administración Desconsentrada de 
							Servicios al Contribuyente que le corresponda y/o a través de la dirección www.sat.gob.mx.</td>
						</tr>
						<tr>
							<td>
								<hr/>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
	</xsl:template>
	
	
	
    <xsl:template name="Cortadora">
        <xsl:param name="strInput" />
        <xsl:param name="nLen" />

        <xsl:value-of select="substring($strInput, 1, $nLen)"/>

        <xsl:if test="string-length($strInput)&gt;$nLen">
            <br />
            <xsl:variable name="strResto" select="substring($strInput, ($nLen + 1))" />
            <xsl:call-template name="Cortadora">
                <xsl:with-param name="strInput" select="$strResto" />
                <xsl:with-param name="nLen" select="$nLen" />
            </xsl:call-template>
        </xsl:if>
    </xsl:template>

	<xsl:template name="formatDate">
		<xsl:param name="dateTime" />
		<xsl:variable name="date" select="substring-before($dateTime, ' ')" />
		<xsl:variable name="month" select="substring-before($date, '/')" />
		<xsl:variable name="day" select="substring-before(substring-after($date, '/'), '/')" />
		<xsl:variable name="year" select="substring-after(substring-after($date, '/'), '/')" />
		<xsl:value-of select="concat($day, '/', $month, '/', $year)" />
	</xsl:template>
	
	<xsl:template name="formatTime">
		<xsl:param name="dateTime" />
		<xsl:value-of select="substring-after($dateTime, ' ')" />
	</xsl:template>
	
</xsl:stylesheet>