
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Procesos:ArithmeticHelper:0:21/Mayo/2008[SAT.DyP.Negocio.Comun.Procesos:1.1:21/Mayo/2008])
	

using System;
using System.Collections.Generic;
using System.Text;

namespace SAT.DyP.Negocio.Comun.Procesos.Helper
{
    /// <summary>
    /// ArithmeticHelper class
    /// </summary>
    internal static class ArithmeticHelper
    {
        /// <summary>
        /// Get ASCII Code from char
        /// </summary>
        /// <param name="letter">character</param>
        /// <returns>ASCII Code</returns>
        public static int Asc(string letter)
        {
            return Convert.ToInt32(Convert.ToChar(letter));
        }

        /// <summary>
        /// Raises a number to the power of another number
        /// </summary>
        /// <param name="number">Number</param>
        /// <param name="exponent">Exponent</param>
        /// <returns>number raised to the exponent power</returns>
        public static int Power(int number, int exponent)
        {
            return (int)Math.Pow(Convert.ToDouble(number), Convert.ToDouble(exponent));
        }

        /// <summary>
        /// Return the integer portion of number
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int Fix(double x)
        {
            return (int)Math.Truncate(x);
        }

        /// <summary>
        /// Calculate modulus for the operation of x and y
        /// </summary>
        /// <param name="first">first operator</param>
        /// <param name="second">second operator</param>
        /// <returns></returns>
        public static double Module(double first, double second)
        {
            long _x = Convert.ToInt64(Math.Round(first));
            long _y = Convert.ToInt64(Math.Round(second));

            double _result = _x - (_y * Fix(_x / _y));

            return _result;
        }
    }
}
