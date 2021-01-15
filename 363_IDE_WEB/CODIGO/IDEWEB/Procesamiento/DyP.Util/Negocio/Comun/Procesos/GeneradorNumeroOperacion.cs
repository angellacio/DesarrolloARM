
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Procesos:GeneradorNumeroOperacion:0:21/Mayo/2008[SAT.DyP.Negocio.Comun.Procesos:1.1:21/Mayo/2008])
	
using System;
using System.Collections.Generic;
using System.Text;
using SAT.DyP.Negocio.Comun.Procesos.Helper;
using SAT.DyP.Util.ExceptionHandling;

namespace SAT.DyP.Negocio.Comun.Procesos
{
    /// <summary>
    /// Clase generadora del número de operación
    /// </summary>
    [Serializable]
    public class GeneradorNumeroOperacion
    {

        private static string KEY_SIN_OPERACIONES_EN_CERO = "SAT.DyP.Util::SinOperacionEnCero";

        /// <summary>
        /// Permite generar el número de operación
        /// </summary>
        /// <param name="IdProceso">Id del Proceso o Folio</param>
        /// <param name="RFC">RFC</param>
        /// <param name="FechaRecepcion">Fecha de recepción</param>
        /// <returns>Número de operación</returns>
        public static string Execute(long IdProceso, string RFC, string FechaRecepcion)
        {
            string _numeroOperacion = string.Empty;
            string _data = null;
            string valorLlave = "no";
            bool sinOpCero = false;
           
            try
            {
		RFC = RFC.Trim();
                if(RFC!=null && FechaRecepcion!=null)
                    _data=String.Format("{0}{1}", RFC, FechaRecepcion);

                long _cifraVerificador = CalculaCifraVerificadora(_data);
                valorLlave = SAT.DyP.Util.Configuration.ConfigurationManager.ApplicationSettings.ReadSetting(KEY_SIN_OPERACIONES_EN_CERO);
                sinOpCero = ((valorLlave.CompareTo("si") == 0) || (valorLlave.CompareTo("1") == 0) || valorLlave.CompareTo("true") == 0);
                _numeroOperacion = CreateNumber(IdProceso, _cifraVerificador, sinOpCero);

            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionHandler.HandleException(ex);
                if (rethrow)                    
                    throw new GeneradorException(ex.Message, ex);
            }

            return _numeroOperacion;
        }

        /// <summary>
        /// Calcula la cifra verificadora, aplicando el algoritmo tantas
        /// veces como el número de pasos sean necesarios
        /// </summary>
        /// <param name="data">Cadena de entrada</param>
        /// <returns>Cifra verificadora</returns>
        private static long CalculaCifraVerificadora(string data)
        {
            if (data == null)
                return 0;

            string _sourceData = data;

            //se obtiene el número de pasos necesarios
            int _stepCount = CalculateSteps(_sourceData);
            long _amount = 0;

            for (int _step = 1; _step <= _stepCount; _step++)
            {
                //first iteration
                for (int j = 1; j <= _sourceData.Length; j++)
                {
                    int _power = ArithmeticHelper.Power(j, 2);
                    _amount += ArithmeticHelper.Asc(_sourceData.Substring(j - 1, 1)) * (_power * 3);
                }

                int _length = _sourceData.Length;

                //second iteration
                for (int j = 1; j <= _sourceData.Length; j++)
                {
                    string _first = _sourceData.Substring(j - 1, 1);
                    string _second = _sourceData.Substring(_length - j, 1);

                    _amount += (ArithmeticHelper.Asc(_first) + ArithmeticHelper.Asc(_second)) * _length;
                }

                //first text rotation
                _sourceData = RotateText(_sourceData);

                //thirty iteration
                int _middle = _sourceData.Length / 2;

                int _lengthData = _sourceData.Length;
                string _substr1 = string.Empty;
                string _substr2 = string.Empty;

                for (int j = 1; j <= _sourceData.Length; j++)
                {
                    if (j <= _middle)
                    {
                        _substr1 = _sourceData.Substring(j - 1, 1);
                        _substr2 = _sourceData.Substring((j - 1) + _middle, 1);

                        _amount += ArithmeticHelper.Asc(_substr1) +
                                   (3 * ArithmeticHelper.Asc(_substr2));
                    }
                    else
                    {
                        _substr1 = _sourceData.Substring(j - 1, 1);
                        _substr2 = _sourceData.Substring((j - 1) - _middle, 1);

                        _amount += ArithmeticHelper.Asc(_substr1) +
                                   (5 * ArithmeticHelper.Asc(_substr2));
                    }
                }

                //Second text rotation
                _sourceData = RotateText(_sourceData);
            }


            return _amount % 10000;
        }

        /// <summary>
        /// Genera el número de operación en formato hexadecimal
        /// </summary>
        /// <param name="processId">Id del Proceso</param>
        /// <param name="verifiyData">Data Verificador</param>
        /// <param name="notZeroOp">Genera número de operación diferente de cero (true) o en cero (false).</param>
        /// <returns>Número de operación</returns>
        private static string CreateNumber(long processId, long verifiyData, bool notZeroOp)
        {
            string _number = string.Empty;

            if ((processId > 0 && verifiyData > 0) || notZeroOp)
            {
                double _temp = verifiyData + ((processId % 52) * 10000);
                _temp = (_temp * 375940);

                double _temp2 = ArithmeticHelper.Module(_temp, 524287);

                _number = String.Format("{0:X}", Convert.ToInt64(_temp2));
            }
            else
                _number = string.Format("{0:x}",0);

            return _number;
        }

        /// <summary>
        /// Mueve cada caracter a la posicion inmediata izquerda
        /// exceptuando al primero que ocupara la última posicion,
        /// hecho lo anterior, aplica un XOR a nivel de bits a la cadena
        /// </summary>
        /// <param name="data">Cadena de entrada</param>
        /// <returns>Cadena de salida</returns>
        private static string RotateText(string data)
        {
            if (data == null)
                return null;

            string _sourceData = string.Concat(data.Substring(1), data.Substring(0, 1));
            string _temp = _sourceData.Substring(0, 1);
            byte j;

            for (int i = 1; i < data.Length; i++)
            {
                int _x = ArithmeticHelper.Asc(_temp.Substring(i - 1, 1));
                int _y = ArithmeticHelper.Asc(_sourceData.Substring(i, 1));

                int _exclusive_or = _x ^ _y;

                j = Convert.ToByte(_exclusive_or);

                _temp = string.Concat(_temp, Convert.ToChar(j).ToString());
            }

            return _temp;
        }


        /// <summary>
        /// Calcula el número de pasos para el algoritmo
        /// </summary>
        /// <param name="data">Cadena de entrada</param>
        /// <returns></returns>
        private static int CalculateSteps(string data)
        {
            if (data == null) 
                return 0;

            int _steps = 0;
            int _sum = 0;

            foreach (char letter in data.ToCharArray())
                _sum += Convert.ToInt32(letter);

            int _mod = _sum % 3;

            switch (_mod)
            {
                case 0: _steps = 3; break;
                case 1: _steps = 5; break;
                case 2: _steps = 7; break;
            }

            return _steps;
        }
    }
}
