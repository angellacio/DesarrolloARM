<fo:root xmlns:fo="http://www.w3.org/1999/XSL/Format">
	<fo:layout-master-set>
		<fo:simple-page-master master-name="simple" page-height="29.7cm" page-width="21cm" margin-top="1cm" margin-bottom="1cm" margin-left="2cm" margin-right="1.5cm">
			<fo:region-body margin-top="6cm" margin-bottom="1cm" background-repeat="no-repeat" background-position-horizontal="center" background-position-vertical="center" background-image="C:\SAT\Development\LCCreditosFiscales\GeneraPDF\Sat.ServicioImpresion\Sat.ServicioImpresion.Servicios\\Images\Configuracion/center2.png" />
			<fo:region-before extent="6cm" />
			<fo:region-after extent="1cm" />
		</fo:simple-page-master>
	</fo:layout-master-set>
	<fo:page-sequence xmlns:fo="http://www.w3.org/1999/XSL/Format" master-reference="simple" initial-page-number="1">
		<!--Encabezado-->
		<fo:static-content flow-name="xsl-region-before">
			<fo:block>
				<fo:table width="17.5cm" border-collapse="separate">
					<fo:table-column column-width="3.5cm" />
					<fo:table-column column-width="10.5cm" />
					<fo:table-column column-width="3.5cm" />
					<fo:table-body>
						<fo:table-row>
							<!--Logo izquierda-->
							<fo:table-cell>
								<fo:block>
									<fo:external-graphic src="C:\SAT\Development\LCCreditosFiscales\GeneraPDF\Sat.ServicioImpresion\Sat.ServicioImpresion.Servicios\\Images\Configuracion/topLeft.png" vertical-align="middle" content-height="69px" />
								</fo:block>
							</fo:table-cell>
							<fo:table-cell>
								<fo:table width="10.5cm" border-collapse="separate">
									<fo:table-column column-width="10.5cm" />
									<fo:table-body>
										<fo:table-row height="1.5cm">
											<fo:table-cell />
										</fo:table-row>
										<fo:table-row>
											<fo:table-cell>
												<fo:block font-family="Constantia" font-size="9pt" color="black" text-align="center" font-weight="bold" vertical-align="middle">
													CONFIRMACIÓN DE RECTIFICACIONES CONTABLES
												</fo:block>
											</fo:table-cell>
										</fo:table-row>
									</fo:table-body>
								</fo:table>
							</fo:table-cell>
							<!--Logo derecha-->
							<fo:table-cell>
								<fo:block>
									<fo:external-graphic src="C:\SAT\Development\LCCreditosFiscales\GeneraPDF\Sat.ServicioImpresion\Sat.ServicioImpresion.Servicios\\Images\Configuracion/topRight.jpg" vertical-align="middle" content-height="66px" />
								</fo:block>
							</fo:table-cell>
						</fo:table-row>
					</fo:table-body>
				</fo:table>
				<fo:table width="17.5cm" border-collapse="separate" border-bottom-style="solid" border-bottom-width="2.0pt" border-bottom-color="#948A54" border-top-style="solid" border-top-width="2.0pt" border-top-color="#948A54">
					<fo:table-column column-width="2.5cm" />
					<fo:table-column column-width="5.5cm" />
					<fo:table-column column-width="4.0cm" />
					<fo:table-column column-width="5.5cm" />
					<fo:table-body>
						<fo:table-row height="8px" vertical-align="middle">
							<fo:table-cell />
							<fo:table-cell />
							<fo:table-cell />
							<fo:table-cell />
						</fo:table-row>
						<fo:table-row height="18px" vertical-align="middle">
							<fo:table-cell>
								<fo:block font-family="Constantia" font-size="9pt" color="black" text-align="left" font-weight="bold" vertical-align="middle">
									ALR:
								</fo:block>
							</fo:table-cell>
							<fo:table-cell>
								<fo:block font-family="Constantia" font-size="9pt" color="black" text-align="left" vertical-align="middle">
									Guadalupe
								</fo:block>
							</fo:table-cell>
							<fo:table-cell>
								<fo:block font-family="Constantia" font-size="9pt" color="black" text-align="left" font-weight="bold" vertical-align="middle">
									Número de documento:
								</fo:block>
							</fo:table-cell>
							<fo:table-cell>
								<fo:block font-family="Arial" font-size="9pt" color="black" text-align="left" vertical-align="middle">
									44-311300000079
								</fo:block>
							</fo:table-cell>
						</fo:table-row>
						<fo:table-row height="18px">
							<fo:table-cell>
								<fo:block font-family="Constantia" font-size="9pt" color="black" text-align="left" font-weight="bold" vertical-align="middle">
									R.F.C.:
								</fo:block>
							</fo:table-cell>
							<fo:table-cell>
								<fo:block font-family="Arial" font-size="9pt" color="black" text-align="left" vertical-align="middle">
									COTA710218FM5
								</fo:block>
							</fo:table-cell>
							<fo:table-cell>
								<fo:block font-family="Constantia" font-size="9pt" color="black" text-align="left" font-weight="bold" vertical-align="middle">
									Fecha y hora de emisión:
								</fo:block>
							</fo:table-cell>
							<fo:table-cell>
								<fo:block font-family="Arial" font-size="9pt" color="black" text-align="left" vertical-align="middle">
									26/Jul/2013 16:11
								</fo:block>
							</fo:table-cell>
						</fo:table-row>
						<fo:table-row height="18px">
							<fo:table-cell>
								<fo:block font-family="Constantia" font-size="9pt" color="black" text-align="left" font-weight="bold" vertical-align="middle">
									Nombre:
								</fo:block>
							</fo:table-cell>
							<fo:table-cell number-columns-spanned="3">
								<fo:block font-family="Constantia" font-size="9pt" color="black" text-align="left" vertical-align="middle">
									ALEIDA CORREA TORRES
								</fo:block>
							</fo:table-cell>
						</fo:table-row>
					</fo:table-body>
				</fo:table>
			</fo:block>
		</fo:static-content>
		<!--Pie de Pagina-->
		<fo:static-content flow-name="xsl-region-after">
			<fo:block line-height="8pt" space-before.optimum="1.5pt" space-after.optimum="1.5pt" text-align="right">
				<fo:inline font-size="8pt">
					<fo:page-number />
				</fo:inline>
				<fo:inline font-size="8pt">/</fo:inline>
				<fo:inline font-size="8pt">
					<fo:page-number-citation ref-id="LastPage" />
				</fo:inline>
			</fo:block>
		</fo:static-content>
		<!--Cuerpo de la pagina-->
		<fo:flow flow-name="xsl-region-body" font-family="Constantia" font-size="9pt" color="black">
			<fo:block break-before="page">
				<!--Tabla 2-->
				<fo:table width="17.5cm" border-collapse="separate">
					<fo:table-column column-width="3.5cm" />
					<fo:table-column column-width="10.5cm" />
					<fo:table-column column-width="3.5cm" />
					<fo:table-body>
						<!--Espacio-->
						<fo:table-row height="8px" vertical-align="middle">
							<fo:table-cell />
							<fo:table-cell />
							<fo:table-cell />
						</fo:table-row>
						<fo:table-row height="18px" vertical-align="middle">
							<fo:table-cell number-columns-spanned="3">
								<fo:block font-family="Constantia" font-size="10pt" color="black" text-align="center" font-weight="bold" vertical-align="middle">
									Tipo de operación e importe
								</fo:block>
							</fo:table-cell>
						</fo:table-row>
						<fo:table-row height="18px" vertical-align="middle">
							<fo:table-cell />
							<fo:table-cell>
								<!--Tabla interior 1-->
								<fo:table width="13.0cm" vertical-align="bottom" text-align="right" display-align="center" border-collapse="separate">
									<fo:table-column column-width="5.0cm" />
									<fo:table-column column-width="5.0cm" />
									<fo:table-column column-width="3.0cm" />
									<fo:table-body>
										<fo:table-row height="18px" vertical-align="middle">
											<fo:table-cell border-width="thin" border-style="solid" background-color="#D9D9D9">
												<fo:block font-family="Constantia" font-size="8pt" color="black" text-align="center" font-weight="bold" vertical-align="middle">
													Tipo de operación
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border-width="thin" border-style="solid" background-color="#D9D9D9">
												<fo:block font-family="Constantia" font-size="8pt" color="black" text-align="center" font-weight="bold" vertical-align="middle">
													Tipo de rectificación
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border-width="thin" border-style="solid" background-color="#D9D9D9">
												<fo:block font-family="Constantia" font-size="8pt" color="black" text-align="center" font-weight="bold" vertical-align="middle">
													Importe de la operación
												</fo:block>
											</fo:table-cell>
										</fo:table-row>
										<fo:table-row height="18px">
											<fo:table-cell border-width="thin" border-style="solid">
												<fo:block font-family="Arial" font-size="8pt" color="black" text-align="center" vertical-align="middle">
													Movimiento contable
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border-width="thin" border-style="solid">
												<fo:block font-family="Arial" font-size="8pt" color="black" text-align="center" vertical-align="middle">
													Reclasificación de conceptos
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border-width="thin" border-style="solid">
												<fo:block font-family="Arial" font-size="8pt" color="black" text-align="center" vertical-align="middle">
													$ 2,184
												</fo:block>
											</fo:table-cell>
										</fo:table-row>
									</fo:table-body>
								</fo:table>
								<!--Fin Tabla interior 1-->
							</fo:table-cell>
							<fo:table-cell />
						</fo:table-row>
						<fo:table-row height="18px">
							<fo:table-cell number-columns-spanned="3">
								<fo:block />
							</fo:table-cell>
						</fo:table-row>
						<fo:table-row height="18px" vertical-align="middle">
							<fo:table-cell number-columns-spanned="3">
								<fo:block font-family="Constantia" font-size="10pt" color="black" text-align="center" font-weight="bold" vertical-align="middle">
									Rectificaciones contables
								</fo:block>
							</fo:table-cell>
						</fo:table-row>
						<fo:table-row height="18px" vertical-align="middle">
							<fo:table-cell number-columns-spanned="3">
								<!--Tabla interior 1-->
								<fo:table width="6.25cm" vertical-align="bottom" text-align="left" border-collapse="separate" display-align="center">
									<fo:table-column column-width="3.50cm" />
									<fo:table-column column-width="2.75cm" />
									<fo:table-body>
										<fo:table-row height="18px" vertical-align="middle">
											<fo:table-cell border-left-width="thin" border-left-style="solid" border-top-width="thin" border-top-style="solid" background-color="#D9D9D9">
												<fo:block font-family="Constantia" font-size="8pt" color="black" text-align="center" font-weight="bold" vertical-align="middle">
													Núm. de documento a rectificar
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border-left-width="thin" border-left-style="solid" border-right-width="thin" border-right-style="solid" border-top-width="thin" border-top-style="solid" background-color="#D9D9D9">
												<fo:block font-family="Constantia" font-size="8pt" color="black" text-align="center" font-weight="bold" vertical-align="middle">
													Fecha de emisión de núm de documento a rectificar
												</fo:block>
											</fo:table-cell>
										</fo:table-row>
										<fo:table-row height="18px">
											<fo:table-cell border-left-width="thin" border-left-style="solid" border-top-width="thin" border-top-style="solid" border-bottom-width="thin" border-bottom-style="solid">
												<fo:block font-family="Arial" font-size="8pt" color="black" text-align="center" vertical-align="middle">
													44-311300000058
												</fo:block>
											</fo:table-cell>
											<fo:table-cell border-width="thin" border-style="solid">
												<fo:block font-family="Arial" font-size="8pt" color="black" text-align="center" vertical-align="middle">
													26/Jul/2013
												</fo:block>
											</fo:table-cell>
										</fo:table-row>
									</fo:table-body>
								</fo:table>
								<!--Fin Tabla interior 1-->
							</fo:table-cell>
						</fo:table-row>
						<fo:table-row height="8px" vertical-align="middle">
							<fo:table-cell number-columns-spanned="3">
								<fo:block />
							</fo:table-cell>
						</fo:table-row>
						<fo:table-row>
							<fo:table-cell number-columns-spanned="3">
								<fo:table width="17.5cm" border-collapse="separate">
									<fo:table-column />
									<fo:table-column />
									<fo:table-column />
									<fo:table-body>
										<fo:table-row height="18px" vertical-align="middle">
											<fo:table-cell />
										</fo:table-row>
										<fo:table-row height="18px" vertical-align="middle">
											<fo:table-cell>
												<fo:table width="17.5cm" border-collapse="separate" text-align="center" vertical-align="middle" display-align="center">
													<fo:table-column column-width="8.65cm" />
													<fo:table-column column-width="0.2cm" />
													<fo:table-column column-width="8.65cm" />
													<fo:table-body>
														<fo:table-row height="18px" vertical-align="middle">
															<fo:table-cell background-color="#D9D9D9">
																<fo:block font-family="Constantia" font-size="8pt" color="black" text-align="center" font-weight="bold" vertical-align="middle">
																	Dice
																</fo:block>
															</fo:table-cell>
															<fo:table-cell />
															<fo:table-cell background-color="#D9D9D9">
																<fo:block font-family="Constantia" font-size="8pt" color="black" text-align="center" font-weight="bold" vertical-align="middle">
																	Debe decir
																</fo:block>
															</fo:table-cell>
														</fo:table-row>
														<fo:table-row height="5px" vertical-align="middle">
															<fo:table-cell />
															<fo:table-cell />
														</fo:table-row>
														<fo:table-row vertical-align="top" display-align="before">
															<fo:table-cell>
																<fo:table width="8.65cm" vertical-align="bottom" text-align="right" border-collapse="separate" display-align="center">
																	<fo:table-column column-width="1.2cm" />
																	<fo:table-column column-width="1.2cm" />
																	<fo:table-column column-width="1.8cm" />
																	<fo:table-column column-width="1.3cm" />
																	<fo:table-column column-width="1.8cm" />
																	<fo:table-column column-width="1.3cm" />
																	<fo:table-body>
																		<fo:table-row height="18px" vertical-align="middle" background-color="#D9D9D9">
																			<fo:table-cell>
																				<fo:block font-family="Constantia" font-size="7pt" color="black" text-align="center" font-weight="bold" vertical-align="middle">
																					Crédito
																				</fo:block>
																			</fo:table-cell>
																			<fo:table-cell>
																				<fo:block font-family="Constantia" font-size="7pt" color="black" text-align="center" font-weight="bold" vertical-align="middle">
																					Clave de Cargo
																				</fo:block>
																			</fo:table-cell>
																			<fo:table-cell>
																				<fo:block font-family="Constantia" font-size="7pt" color="black" text-align="center" font-weight="bold" vertical-align="middle">
																					Descripción
																				</fo:block>
																			</fo:table-cell>
																			<fo:table-cell>
																				<fo:block font-family="Constantia" font-size="7pt" color="black" text-align="center" font-weight="bold" vertical-align="middle">
																					Clave de Abono
																				</fo:block>
																			</fo:table-cell>
																			<fo:table-cell>
																				<fo:block font-family="Constantia" font-size="7pt" color="black" text-align="center" font-weight="bold" vertical-align="middle">
																					Descripción
																				</fo:block>
																			</fo:table-cell>
																			<fo:table-cell>
																				<fo:block font-family="Constantia" font-size="7pt" color="black" text-align="center" font-weight="bold" vertical-align="middle">
																					Importe
																				</fo:block>
																			</fo:table-cell>
																		</fo:table-row>
																		<fo:table-row height="5px">
																			<fo:table-cell number-columns-spanned="6">
																				<fo:block />
																			</fo:table-cell>
																		</fo:table-row>
																		<fo:table-row xmlns:fo="http://www.w3.org/1999/XSL/Format" height="18px" font-size="6pt" vertical-align="top">
																			<fo:table-cell border="0.5px solid black">
																				<fo:block font-family="Arial" text-align="left" vertical-align="middle">
																					700778
																				</fo:block>
																			</fo:table-cell>
																			<fo:table-cell border="0.5px solid black">
																				<fo:block font-family="Arial" text-align="left" vertical-align="middle">
																					100014
																				</fo:block>
																			</fo:table-cell>
																			<fo:table-cell border="0.5px solid black">
																				<fo:block font-family="Arial" text-align="left" vertical-align="middle">
																					Descrípcion Temporal
																				</fo:block>
																			</fo:table-cell>
																			<fo:table-cell border="0.5px solid black" number-rows-spanned="1">
																				<fo:block font-family="Arial" text-align="center" vertical-align="middle">
																					920048
																				</fo:block>
																			</fo:table-cell>
																			<fo:table-cell border="0.5px solid black" number-rows-spanned="1">
																				<fo:block font-family="Arial" text-align="justify" vertical-align="middle">
																					IMPORTE SIN EL PAGO INICIAL
																				</fo:block>
																			</fo:table-cell>
																			<fo:table-cell border="0.5px solid black">
																				<fo:block font-family="Arial" text-align="right" vertical-align="middle">
																					$ 1,092
																				</fo:block>
																			</fo:table-cell>
																		</fo:table-row>
																		<fo:table-row height="5px" vertical-align="middle">
																			<fo:table-cell border-width="thin" border-style="solid" number-columns-spanned="5" background-color="#D9D9D9">
																				<fo:block font-family="Arial" font-size="8pt" color="black" text-align="center" font-weight="bold" vertical-align="middle">
																					Total
																				</fo:block>
																			</fo:table-cell>
																			<fo:table-cell border-width="thin" border-style="solid" number-columns-spanned="2" background-color="#D9D9D9">
																				<fo:block font-family="Arial" font-size="8pt" color="black" text-align="right" font-weight="bold" vertical-align="middle">
																					$ 1,092
																				</fo:block>
																			</fo:table-cell>
																		</fo:table-row>
																	</fo:table-body>
																</fo:table>
															</fo:table-cell>
															<fo:table-cell />
															<fo:table-cell>
																<fo:block font-family="Arial" font-size="8pt" color="black" text-align="right" vertical-align="middle">
																	<fo:table width="8.65cm" vertical-align="bottom" text-align="right" border-collapse="separate" display-align="center">
																		<fo:table-column column-width="1.2cm" />
																		<fo:table-column column-width="1.2cm" />
																		<fo:table-column column-width="1.8cm" />
																		<fo:table-column column-width="1.3cm" />
																		<fo:table-column column-width="1.8cm" />
																		<fo:table-column column-width="1.3cm" />
																		<fo:table-body>
																			<fo:table-row height="18px" vertical-align="middle" background-color="#D9D9D9">
																				<fo:table-cell>
																					<fo:block font-family="Constantia" font-size="7pt" color="black" text-align="center" font-weight="bold" vertical-align="middle">
																						Crédito
																					</fo:block>
																				</fo:table-cell>
																				<fo:table-cell>
																					<fo:block font-family="Constantia" font-size="7pt" color="black" text-align="center" font-weight="bold" vertical-align="middle">
																						Clave de Cargo
																					</fo:block>
																				</fo:table-cell>
																				<fo:table-cell>
																					<fo:block font-family="Constantia" font-size="7pt" color="black" text-align="center" font-weight="bold" vertical-align="middle">
																						Descripción
																					</fo:block>
																				</fo:table-cell>
																				<fo:table-cell>
																					<fo:block font-family="Constantia" font-size="7pt" color="black" text-align="center" font-weight="bold" vertical-align="middle">
																						Clave de Abono
																					</fo:block>
																				</fo:table-cell>
																				<fo:table-cell>
																					<fo:block font-family="Constantia" font-size="7pt" color="black" text-align="center" font-weight="bold" vertical-align="middle">
																						Descripción
																					</fo:block>
																				</fo:table-cell>
																				<fo:table-cell>
																					<fo:block font-family="Constantia" font-size="7pt" color="black" text-align="center" font-weight="bold" vertical-align="middle">
																						Importe
																					</fo:block>
																				</fo:table-cell>
																			</fo:table-row>
																			<fo:table-row height="5px">
																				<fo:table-cell number-columns-spanned="6">
																					<fo:block />
																				</fo:table-cell>
																			</fo:table-row>
																			<fo:table-row xmlns:fo="http://www.w3.org/1999/XSL/Format" height="18px" font-size="6pt" vertical-align="top">
																				<fo:table-cell border="0.5px solid black">
																					<fo:block font-family="Arial" text-align="left" vertical-align="middle">
																						700778
																					</fo:block>
																				</fo:table-cell>
																				<fo:table-cell border="0.5px solid black">
																					<fo:block font-family="Arial" text-align="left" vertical-align="middle">
																						100001
																					</fo:block>
																				</fo:table-cell>
																				<fo:table-cell border="0.5px solid black">
																					<fo:block font-family="Arial" text-align="left" vertical-align="middle">
																						GASTOS DE EJECUCION POR REQUERIMIENTO DE CREDITOS (A.L.).
																					</fo:block>
																				</fo:table-cell>
																				<fo:table-cell border="0.5px solid black" number-rows-spanned="1">
																					<fo:block font-family="Arial" text-align="center" vertical-align="middle">
																						920048
																					</fo:block>
																				</fo:table-cell>
																				<fo:table-cell border="0.5px solid black" number-rows-spanned="1">
																					<fo:block font-family="Arial" text-align="justify" vertical-align="middle">
																						IMPORTE SIN EL PAGO INICIAL
																					</fo:block>
																				</fo:table-cell>
																				<fo:table-cell border="0.5px solid black">
																					<fo:block font-family="Arial" text-align="right" vertical-align="middle">
																						$ 1,092
																					</fo:block>
																				</fo:table-cell>
																			</fo:table-row>
																			<fo:table-row height="5px" vertical-align="middle">
																				<fo:table-cell border-width="thin" border-style="solid" number-columns-spanned="5" background-color="#D9D9D9">
																					<fo:block font-family="Arial" font-size="8pt" color="black" text-align="center" font-weight="bold" vertical-align="middle">
																						Total
																					</fo:block>
																				</fo:table-cell>
																				<fo:table-cell border-width="thin" border-style="solid" number-columns-spanned="2" background-color="#D9D9D9">
																					<fo:block font-family="Arial" font-size="8pt" color="black" text-align="right" font-weight="bold" vertical-align="middle">
																						$ 1,092
																					</fo:block>
																				</fo:table-cell>
																			</fo:table-row>
																		</fo:table-body>
																	</fo:table>
																</fo:block>
															</fo:table-cell>
														</fo:table-row>
													</fo:table-body>
												</fo:table>
											</fo:table-cell>
										</fo:table-row>
									</fo:table-body>
								</fo:table>
							</fo:table-cell>
						</fo:table-row>
						<!-- todo Bien -->
					</fo:table-body>
				</fo:table>
				<!--Hasta aqui voy-->
				<fo:table width="17.5cm" border-collapse="separate">
					<fo:table-column column-width="17.5cm" />
					<fo:table-body>
						<fo:table-row height="0.2cm">
							<fo:table-cell>
								<fo:block />
							</fo:table-cell>
						</fo:table-row>
						<fo:table-row>
							<fo:table-cell>
								<fo:block font-family="Constantia" font-size="10pt" color="black" text-align="center" font-weight="bold" vertical-align="middle">
									Motivo de la rectificación:
								</fo:block>
							</fo:table-cell>
						</fo:table-row>
						<fo:table-row height="0.2cm">
							<fo:table-cell>
								<fo:block />
							</fo:table-cell>
						</fo:table-row>
						<fo:table-row>
							<fo:table-cell>
								<fo:table width="17.5cm" border-collapse="separate" text-align="center" vertical-align="middle">
									<fo:table-column column-width="17.5cm" />
									<fo:table-body>
										<fo:table-row height="2.0cm">
											<fo:table-cell border-style="solid" border-width="0.5px" border-color="#000000">
												<fo:block margin-left="5px" text-align="justify">

												</fo:block>
											</fo:table-cell>
										</fo:table-row>
									</fo:table-body>
								</fo:table>
							</fo:table-cell>
						</fo:table-row>
						<fo:table-row height="0.2cm">
							<fo:table-cell>
								<fo:block />
							</fo:table-cell>
						</fo:table-row>
						<fo:table-row />
						<fo:table-row height="0.4cm">
							<fo:table-cell>
								<fo:block />
							</fo:table-cell>
						</fo:table-row>
						<fo:table-row />
						<fo:table-row height="0.3cm">
							<fo:table-cell>
								<fo:block />
							</fo:table-cell>
						</fo:table-row>
					</fo:table-body>
				</fo:table>
			</fo:block>
			<fo:block space-before.optimum="1.5pt" space-after.optimum="1.5pt" keep-together="always" id="LastPage" line-height="1pt" font-size="1pt" />
		</fo:flow>
	</fo:page-sequence>
</fo:root>