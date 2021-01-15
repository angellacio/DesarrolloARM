<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:template match="/">
		<xsl:choose>
			<xsl:when test="mensajeAcuse/MedioPresentacion = 2 or mensajeAcuse/MedioPresentacion = 3 or mensajeAcuse/MedioPresentacion = 4">
				<center>
					<table >
						<tr >
							<td  align="center">
								<th>
									<font SIZE="4" COLOR="black" FACE="Arial">
										El Estado de su Declaración es el siguiente:
									</font>
								</th>
							</td>

						</tr>
					</table>
					<br/>
					<table >
						<tr >
							<td align="center">

								<xsl:choose>
									<xsl:when test="mensajeAcuse/MedioPresentacion = 3">
										<font SIZE="2" COLOR="black" FACE="Arial" >Contribuyente: </font>
									</xsl:when>
									<xsl:when test="mensajeAcuse/MedioPresentacion = 4">
										<font SIZE="2" COLOR="black" FACE="Arial" >Usuario Autenticado: </font>
									</xsl:when>
								</xsl:choose>
								<font SIZE="3" COLOR="black" FACE="Verdana">
									<xsl:value-of select="mensajeAcuse/RfcAutenticacion"/>
								</font>
							</td>
						</tr>

						<tr>
							<td align="center">

								<font SIZE="2" COLOR="black" FACE="Arial" >Archivo Recibido: </font>
								<font SIZE="3" COLOR="black" FACE="Verdana">
									<xsl:value-of select="mensajeAcuse/NombreArchivo"/>
								</font>

							</td>
						</tr>
						<tr>
							<td align="center">

								<font SIZE="2" COLOR="black" FACE="Arial" >Tamaño de Archivo: </font>
								<font SIZE="3" COLOR="black" FACE="Verdana">
									<xsl:value-of select="mensajeAcuse/TamañoArchivo"/>
								</font>
								<font SIZE="2" COLOR="black" FACE="Arial" > Bytes </font>

							</td>
						</tr>
						<tr>
							<td align="center">

								<font SIZE="2" COLOR="black" FACE="Arial" >Fecha de Recepción: </font>
								<font SIZE="3" COLOR="black" FACE="Verdana">
									<xsl:value-of select="mensajeAcuse/FechaRecepcion"/>
								</font>

							</td>
						</tr>
						<tr>
							<td align="center">

								<font SIZE="2" COLOR="black" FACE="Arial" >Hora de Recepción: </font>
								<font SIZE="3" COLOR="black" FACE="Verdana">
									<xsl:value-of select="mensajeAcuse/HoraRecepcion"/>
								</font>

							</td>
						</tr>
						<tr>
							<td align="center">
								<font SIZE="2" COLOR="black" FACE="Arial" >Folio de Recepción: </font>
								<font SIZE="3" COLOR="black" FACE="Verdana">
									<xsl:value-of select="mensajeAcuse/Folio"/>
								</font>
							</td>
						</tr>
					</table>
					<br></br>
					<table width="600">
						<tr>
							<td align="center">
								<font SIZE="3" COLOR="black" FACE="Verdana" >									
									El archivo de su declaración fue recibido y será procesado por 
									el SAT, el cual puede ser aceptado o rechazado como resultado de 
									su validación. Para obtener el acuse de su declaración, favor de 
									acceder a la opción de impresión de acuses.
								</font>
							</td>
						</tr>
					</table >
					<br></br>
					<table >
						<tr>
							<td width="350" align="center">
								<font SIZE="3" COLOR="black" FACE="Verdana">
									Espere por   favor su   acuse  con sello
									digital, el cual le llegará a la dirección
									de   su   correo   electrónico.   Si no lo
									recibe        de        inmediato,      le
									recomendamos    acceder    a    la  opción
									de      "Reimpresión     de        Acuses",
									ubicada    en    esta   misma página en la
									sección    "Oficina     Virtual    (e-SAT)/
									Operaciones",     donde      lo      podrá
									obtener oportunamente.
								</font>
							</td>
						</tr>
					</table >

				</center>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
</xsl:stylesheet>