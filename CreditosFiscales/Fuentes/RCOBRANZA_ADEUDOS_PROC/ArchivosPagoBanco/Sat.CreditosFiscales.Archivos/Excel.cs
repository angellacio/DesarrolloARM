using System;
using System.Collections.Generic;
using LinqToExcel;
using System.Linq;
namespace Sat.CreditosFiscales.Archivos
{
    public class Excel
    {
        public Excel()
        {
        }

        public List<DyP> ToEntidadHojaExcelList(string pathDelFicheroExcel)
        {
            var book = new ExcelQueryFactory(pathDelFicheroExcel);

            var resultado = (from row in book.Worksheet("PagosLocalizados")
                             let item = new DyP
                             {
                                 ClaveBanco = row["ClaveBanco"].Cast<string>(),
                                 LineaCaptura = row["﻿LineaCaptura"].Cast<string>(),
                                 FechaDePago = row["FechaDePago"].Cast<string>(),
                                 Hora = row["Hora"].Cast<string>(),
                                 Importe = row["Importe"].Cast<string>(),
                                 MedioRecepcion = row["NumeroOperacionBanco"].Cast<string>(),
                                 NumeroOperacionBanco = row["MedioRecepcion"].Cast<string>(),
                                 Version = row["Version"].Cast<string>()
                             }
                             select item).ToList();

            book.Dispose();
            return resultado;
        }
    }
}
