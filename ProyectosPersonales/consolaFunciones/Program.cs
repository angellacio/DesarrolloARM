using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using ent = Personal.Entidades;

namespace consolaFunciones
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string sOpcion = "";
            Console.WriteLine("     *************************************************************** ");
            Console.WriteLine("");
            Console.WriteLine("                        Seleccione una Opción ");
            Console.WriteLine("");
            Console.WriteLine("     1 - Crea Catalogo de generos ");
            Console.WriteLine("");
            Console.WriteLine("     *************************************************************** ");
            sOpcion = Console.ReadLine();
            switch (sOpcion)
            {
                case "1":
                    CreaCatalogoGeneros();
                    break;
            }
        }

        static void CreaCatalogoGeneros()
        {
            AdministraCatalogos admCat = new AdministraCatalogos();
            admCat.CreaJSonAdministracion($"C:\\InfAco\\99_DatosPersonales\\Repositorios\\GitLab\\DesarrolloARM\\ProyectosPersonales\\ManejoCanciones\\AplicaAdministra.json");
        }

    }
}
