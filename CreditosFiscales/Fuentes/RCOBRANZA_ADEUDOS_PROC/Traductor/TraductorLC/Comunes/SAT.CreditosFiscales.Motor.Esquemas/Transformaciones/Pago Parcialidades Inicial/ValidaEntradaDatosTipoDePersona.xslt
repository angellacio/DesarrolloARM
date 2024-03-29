<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" exclude-result-prefixes="msxsl var userCSharp" version="1.0" xmlns:userCSharp="http://schemas.microsoft.com/BizTalk/2003/userCSharp">
  <xsl:output omit-xml-declaration="yes" method="xml" version="1.0" />
  <xsl:template match="/">
    <xsl:apply-templates select="/SolicitudCreditosFiscales" />
  </xsl:template>
  <xsl:template match="/SolicitudCreditosFiscales">    
    <xsl:variable name="var:PagoObligadoInternet" select="Mensaje/DatosGenerales/LineaCaptura/PagoObligadoInternet/text()" />
    <xsl:variable name="var:PersonaFisica" select="2"/>
    <xsl:variable name="var:PersonaMoral" select="4"/>
    <xsl:variable name="var:LenNombre" select="string-length(Mensaje/DatosGenerales/Nombre/text())"/>
    <xsl:variable name="var:LenPaterno" select="string-length(Mensaje/DatosGenerales/ApellidoPaterno/text())"/>
    <xsl:variable name="var:LenRazonSocial" select="string-length(Mensaje/DatosGenerales/RazonSocial/text())"/>
    <xsl:choose>
      <xsl:when test="$var:PagoObligadoInternet = $var:PersonaFisica ">
        <xsl:choose>
          <xsl:when test="$var:LenNombre > 0 and $var:LenPaterno > 0 ">
            <Exito>OK</Exito>
          </xsl:when>
          <xsl:otherwise>
            <xsl:variable name="var:ErrorPersonaFisica" select="userCSharp:StringConcat('El tipo de persona física debe tener valores para Nombre y Apellido Paterno')" />
            <xsl:message terminate="yes">
              <xsl:value-of select="$var:ErrorPersonaFisica"/>
            </xsl:message>
          </xsl:otherwise>
        </xsl:choose>
      </xsl:when>
      <xsl:otherwise>
        <xsl:choose>
          <xsl:when test="$var:LenRazonSocial > 0">
            <Exito>OK</Exito>
          </xsl:when>
          <xsl:otherwise>
            <xsl:variable name="var:ErrorPersonaMoral" select="userCSharp:StringConcat('El tipo de persona moral debe tener valores para Razón Social')" />
            <xsl:message terminate="yes">
              <xsl:value-of select="$var:ErrorPersonaMoral"/>
            </xsl:message>
          </xsl:otherwise>
        </xsl:choose>
      </xsl:otherwise>
    </xsl:choose>   
  </xsl:template>
  <msxsl:script language="C#" implements-prefix="userCSharp">
    <![CDATA[
public string StringConcat(string param0)
{
   return param0;
}

public string DateCurrentDate()
{
	DateTime dt = DateTime.Now;
	return dt.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
}


public string StringConcat(string param0, string param1)
{
   return param0 + param1;
}

public bool LogicalLte(string val1, string val2)
{
	bool ret = false;     

  ret = DateTime.Parse(val1) <= DateTime.Parse(val2);            

  return ret;
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