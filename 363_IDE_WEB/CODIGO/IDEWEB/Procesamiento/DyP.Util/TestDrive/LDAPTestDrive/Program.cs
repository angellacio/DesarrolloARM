//@(#)SCADE2(W:SKD08212CO2:DyP.Util.TestDrive.LDAPTestDrive:Program:0:21/Mayo/2008[DyP.Util.TestDrive.LDAPTestDrive:1.0:21/Mayo/2008])
	
using System;
using System.Collections.Generic;
using System.Text;
using SAT.DyP.Presentacion.Seguridad.Autenticacion.Componentes;
using SAT.DyP.Util.Logging;

namespace DyP.Util.TestDrive.LDAPTestDrive
{
    class Program
    {
        static void Main(string[] args)
        {
            LdapProvider directory = new LdapProvider();

            try
            {
                string username = "";
                string contraseña = "";

                Console.WriteLine("Indique la cuenta de usuario de eDirectory (Novell):");
                username = Console.ReadLine();


                Console.WriteLine("Indique la contraseña:");
                contraseña = Console.ReadLine();


                bool resultado= directory.ValidateUser(username, contraseña);

                if (resultado)
                {
                    Console.WriteLine("Los datos del usuario {0} son válidos", username);
                }
                else
                {
                    Console.WriteLine("Usuario / contraseña incorrectos");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Existe un error:{0}",ex.Message);
                Console.WriteLine(ex.StackTrace);
                EventLogHelper.WriteErrorEntry(ex.Message, 100);

            }

            Console.WriteLine("Para terminar presione <ENTER>....");
            Console.ReadLine();
        }
    }
}
