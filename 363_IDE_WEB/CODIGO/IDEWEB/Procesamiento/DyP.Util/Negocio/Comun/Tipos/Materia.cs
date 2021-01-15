
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Tipos:Materia:0:21/Mayo/2008[SAT.DyP.Negocio.Comun.Tipos:1.0:21/Mayo/2008])
	
using System;
using System.Collections.Generic;
using System.Text;

namespace SAT.DyP.Negocio.Comun.Tipos
{
  public class Materia
  {

 

    public static bool EsAvisosEnCero(string materia)
    {
      int idMateria = 0;

      if (int.TryParse(materia, out idMateria))
      {
        return EsAvisosEnCero(idMateria);
      }

      return false;
    }

    public static bool EsAnuales(string materia)
    {
      int idMateria = 0;

      if (int.TryParse(materia, out idMateria))
      {
        return EsAnuales(idMateria);
      }

      return false;
    }

    public static bool EsInformativasDeTerceros(string materia)
    {
      int idMateria = 0;

      if (int.TryParse(materia, out idMateria))
      {
        return EsInformativasDeTerceros(idMateria);
      }

      return false;
    }

    public static bool EsIETU(string materia)
    {
      int idMateria = 0;

      if (int.TryParse(materia, out idMateria))
      {
        return EsIETU(idMateria);
      }

      return false;
    }

    // TODO: SACAR A CONFIGURACIÓN LOS VALORES DE CADA MATERIA
    public static bool EsAvisosEnCero(int materia)
    {
      return materia == 2; // Este es el único lugar del código donde debe estar fijo este valor
    }

    public static bool EsAnuales(int materia)
    {
      return materia == 21; // Este es el único lugar del código donde debe estar fijo este valor
    }

    public static bool EsInformativasDeTerceros(int materia)
    {
      return materia == 205; // Este es el único lugar del código donde debe estar fijo este valor
    }

    public static bool EsIETU(int materia)
    {
      return materia == 207; // Este es el único lugar del código donde debe estar fijo este valor
    }

  }
}