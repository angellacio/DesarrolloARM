
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Tipos:MedioDePresentacion:0:21/Mayo/2008[SAT.DyP.Negocio.Comun.Tipos:1.0:21/Mayo/2008])
	
using System;
using System.Collections.Generic;
using System.Text;
using SAT.DyP.Util.Types;

namespace SAT.DyP.Negocio.Comun.Tipos
{
    /// <summary>
    /// Clase del Medio de Presentación
    /// </summary>
    public class MedioDePresentacion
    {
        #region Instance members
        internal MedioDePresentacion(int idMedio, string letra)
        {
            _idMedio = idMedio;
            _letra = letra;
        }

        private int _idMedio;

        public int IdMedio
        {
            get { return _idMedio; }
        }

        public string IdMedioAsString
        {
            get { return _idMedio.ToString(); }
        }

        private string _letra;

        public string Letra
        {
            get { return _letra; }
        }
        #endregion

        #region Static members

        // 'well known' values
        private static MedioDePresentacion _modulo = new MedioDePresentacion(2, "M");
        private static MedioDePresentacion _internet = new MedioDePresentacion(3, "I");
        private static MedioDePresentacion _moduloPapel = new MedioDePresentacion(4, "4"); // TODO: INVESTIGAR SI HAY UNA LETRA PARA 4, AL PARECER NO, PUES SOLO AVISO CERO USA LETRAS Y NO USA ESTE VALOR.
        private static MedioDePresentacion _envioExpress = new MedioDePresentacion(9, "E"); 

        private static Dictionary<int, MedioDePresentacion> _mediosById;

        static MedioDePresentacion()
        {
            _mediosById = new Dictionary<int, MedioDePresentacion>();
            _mediosById.Add(_modulo.IdMedio, _modulo);
            _mediosById.Add(_internet.IdMedio, _internet);
            _mediosById.Add(_moduloPapel.IdMedio, _moduloPapel);
            _mediosById.Add(_envioExpress.IdMedio, _envioExpress);
        }

        public static bool EsInternet(int idMedio)
        {
            return (idMedio == _internet.IdMedio);
        }

        public static bool EsInternet(string idMedio)
        {
            return (idMedio == _internet.Letra || idMedio == _internet.IdMedio.ToString());
        }

        public static bool EsModuloSAT(int idMedio)
        {
            return (idMedio == _modulo.IdMedio);
        }

        public static bool EsModuloSAT(string idMedio)
        {
            return (idMedio == _modulo.Letra || idMedio == _modulo.IdMedio.ToString());
        }

        public static bool EsModuloSATEnPapel(int idMedio)
        {
            return (idMedio == _moduloPapel.IdMedio);
        }

        public static bool EsModuloSATEnPapel(string idMedio)
        {
            return (idMedio == _moduloPapel.Letra || idMedio == _moduloPapel.IdMedio.ToString());
        }

        public static bool EsEnvioExpress(int idMedio)
        {
            return (idMedio == _envioExpress.IdMedio);
        }

        public static bool EsEnvioExpress(string idMedio)
        {
            return (idMedio == _envioExpress.Letra || idMedio == _envioExpress.IdMedio.ToString());
        }

        public static string GetLetra(string idMedioPresentacion)
        {
            // si ya es una letra, devolvemos lo mismo
            foreach (MedioDePresentacion medio in _mediosById.Values)
            {
                if (medio.Letra == idMedioPresentacion) return idMedioPresentacion;
            }

            // si es un número, lo convertimos a entero para buscar la letra

            try
            {
                return GetLetra(Convert.ToInt32(idMedioPresentacion));
            }
            catch
            {
                string errorMessage = String.Format("El valor de cadena '{0}' no es un identificador conocido de medio de presentación.", idMedioPresentacion);
                throw new BusinessException(errorMessage);
            }
        }

        public static string GetLetra(int idMedioPresentacion)
        {
            if (!_mediosById.ContainsKey(idMedioPresentacion))
            {
                string errorMessage = String.Format("El medio de presentación {0} no tiene un identificador de letra conocido, tal como 'I' ó 'M'.", idMedioPresentacion);
                throw new BusinessException(errorMessage);
            }
            else
            {
                return _mediosById[idMedioPresentacion].Letra;
            }
        }
        #endregion
    }
}
