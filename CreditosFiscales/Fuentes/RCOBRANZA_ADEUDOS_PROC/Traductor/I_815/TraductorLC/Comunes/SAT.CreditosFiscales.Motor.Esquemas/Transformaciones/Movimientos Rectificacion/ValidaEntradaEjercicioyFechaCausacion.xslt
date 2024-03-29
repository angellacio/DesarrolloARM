<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" xmlns:userCSharp="http://schemas.microsoft.com/BizTalk/2003/userCSharp" exclude-result-prefixes="msxsl var userCSharp" version="1.0">
  <xsl:output omit-xml-declaration="yes" method="xml" version="1.0" />
  <xsl:template match="/">
    <xsl:apply-templates select="/MovimientoRectificacion" />
  </xsl:template>
  <xsl:template match="/MovimientoRectificacion">
    <xsl:for-each select="Mensaje/ResolucionesDeterminante/ResolucionDeterminante/Conceptos">
      <!--Inicia Movimientos Rectificacion Correcto-->
      <xsl:variable name="var:LenFC" select="string-length(Rectificaciones/Correcto/FechaCausacion/text())" />
      <xsl:variable name="var:LenEjercicio" select="string-length(Rectificaciones/Correcto/Ejercicio/text())" />
      <xsl:choose>
        <xsl:when test="$var:LenFC > 0 and $var:LenEjercicio > 0">
          <xsl:variable name="var:ErrorPadre" select="userCSharp:StringConcat('La Fecha de causación y el ejercicio son excluyentes para el concepto padre:  ', Rectificaciones/Correcto/Clave/text() )" />
          <xsl:message terminate="yes">
            <xsl:value-of select="$var:ErrorPadre" />
          </xsl:message>
        </xsl:when>
        <xsl:otherwise>
          <Exito>OK</Exito>
        </xsl:otherwise>
      </xsl:choose>
      <xsl:for-each select="Rectificaciones/Correcto/Hijos/Hijo">
        <xsl:variable name="var:LenFCHijo" select="string-length(FechaCausacion/text())" />
        <xsl:variable name="var:LenEjercicioHijo" select="string-length(Ejercicio/text())" />
        <xsl:choose>
          <xsl:when test="$var:LenFCHijo > 0 and $var:LenEjercicioHijo > 0">
            <xsl:variable name="var:ErrorHijo" select="userCSharp:StringConcat('La Fecha de causación y el ejercicio son excluyentes para el concepto hijo:  ', Clave/text() )" />
            <xsl:message terminate="yes">
              <xsl:value-of select="$var:ErrorHijo" />
            </xsl:message>
          </xsl:when>
          <xsl:otherwise>
            <Exito>OK</Exito>
          </xsl:otherwise>
        </xsl:choose>
      </xsl:for-each>
      <!--Fin Movimientos Rectificacion Correcto-->
      <!--Inicia Movimientos Rectificacion Erroneo-->
      <xsl:variable name="var:LenFCErroneo" select="string-length(Rectificaciones/Erroneo/FechaCausacion/text())" />
      <xsl:variable name="var:LenEjercicioErroneo" select="string-length(Rectificaciones/Erroneo/Ejercicio/text())" />
      <xsl:choose>
        <xsl:when test="$var:LenFCErroneo > 0 and $var:LenEjercicioErroneo > 0">
          <xsl:variable name="var:ErrorPadre" select="userCSharp:StringConcat('La Fecha de causación y el ejercicio son excluyentes para el concepto padre:  ', Rectificaciones/Erroneo/Clave/text() )" />
          <xsl:message terminate="yes">
            <xsl:value-of select="$var:ErrorPadre" />
          </xsl:message>
        </xsl:when>
        <xsl:otherwise>
          <Exito>OK</Exito>
        </xsl:otherwise>
      </xsl:choose>
      <xsl:for-each select="Rectificaciones/Erroneo/Hijos/Hijo">
        <xsl:variable name="var:LenFCHijo" select="string-length(FechaCausacion/text())" />
        <xsl:variable name="var:LenEjercicioHijo" select="string-length(Ejercicio/text())" />
        <xsl:choose>
          <xsl:when test="$var:LenFCHijo > 0 and $var:LenEjercicioHijo > 0">
            <xsl:variable name="var:ErrorHijo" select="userCSharp:StringConcat('La Fecha de causación y el ejercicio son excluyentes para el concepto hijo:  ', Clave/text() )" />
            <xsl:message terminate="yes">
              <xsl:value-of select="$var:ErrorHijo" />
            </xsl:message>
          </xsl:when>
          <xsl:otherwise>
            <Exito>OK</Exito>
          </xsl:otherwise>
        </xsl:choose>
      </xsl:for-each>
      <!--Fin Movimientos Rectificacion Erroneo-->
    </xsl:for-each>
  </xsl:template>
  <msxsl:script language="C#" implements-prefix="userCSharp">
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

    ret = DateTime.Parse(val1) &lt;= DateTime.Parse(val2);

    return ret;
    }

    public bool LogicalEq(string val1, string val2)
    {
    bool ret = false;
    double d1 = 0;
    double d2 = 0;
    if (IsNumeric(val1, ref d1) &amp;&amp; IsNumeric(val2, ref d2))
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


  </msxsl:script>
</xsl:stylesheet>