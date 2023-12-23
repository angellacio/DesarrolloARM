using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FuncionesExtras.Entidades
{
    public class EntDatArchivo
    {
        public EntDatArchivo()
        {
            D01_RenglonDec = -1;
            D00_RutaCompleta = string.Empty;
            D00_RutaNombre = string.Empty;
            D02_TextArchivo = string.Empty;
            D10_Variable = string.Empty;
            D11_Condiciones = string.Empty;
            D12_ComponenteComp = string.Empty;
            D01_MetodoRenglon = -1;
            D13_MetodoComp = string.Empty;
        }
        public EntDatArchivo(EntDatArchivo itemDatos)
        {
            D01_RenglonDec = itemDatos.D01_RenglonDec;
            D00_RutaCompleta = itemDatos.D00_RutaCompleta;
            D00_RutaNombre = itemDatos.D00_RutaNombre;
            D02_TextArchivo = itemDatos.D02_TextArchivo;
            D10_Variable = itemDatos.D10_Variable;
            D11_Condiciones = itemDatos.D11_Condiciones;
            D12_ComponenteComp = itemDatos.D12_ComponenteComp;
            D01_MetodoRenglon = itemDatos.D01_MetodoRenglon;
            D13_MetodoComp = itemDatos.D13_MetodoComp;
        }

        public override string ToString()
        {
            //return $"{D01_Renglon}°{D00_RutaNombre}°{D10_Variable}°{D12_ComponenteComp}°{D12_ComponenteDLL}°{D12_ComponenteClase}°{D13_MetodoComp}°{D13_MetodoFuncion}°{D13_MetodoParametros}";
            return $"{D00_RutaNombre}°{D01_RenglonDec}°{D12_ComponenteComp}°{D10_Variable}°{D01_MetodoRenglon}°{D13_MetodoFuncion}°{D13_MetodoComp}°{D02_TextArchivo}";
        }
        public int D01_RenglonDec { get; set; }
        public string D00_RutaCompleta { get; set; }
        public string D00_RutaNombre { get; set; }
        public string D02_TextArchivo { get; set; }
        public string D10_Variable { get; set; }
        public string D11_Condiciones { get; set; }
        public string D12_ComponenteComp { get; set; }
        public string D12_ComponenteDLL
        {
            get
            {
                string[] sResul = D12_ComponenteComp.Split('.');
                if (sResul.Length > 1)
                {
                    return sResul[0].Replace("BLL", "").Trim();
                }
                else
                {
                    return D12_ComponenteComp;
                }
            }
        }
        public string D12_ComponenteClase
        {
            get
            {
                string[] sResul = D12_ComponenteComp.Split('.');
                if (sResul.Length > 1)
                {
                    return sResul[1];
                }
                else
                {
                    return D12_ComponenteComp;
                }
            }
        }
        public int D01_MetodoRenglon { get; set; }
        public string D13_MetodoComp { get; set; }
        public string D13_MetodoFuncion
        {
            get
            {
                string[] sResul = D13_MetodoComp.Split('(');
                if (sResul.Length > 1)
                {
                    return sResul[0];
                }
                else
                {
                    sResul = D13_MetodoComp.Split(' ');
                    return sResul[0];
                }
            }
        }
        public string D13_MetodoParametros
        {
            get
            {
                string[] sResul = D13_MetodoComp.Split('(');
                if (sResul.Length > 1)
                {
                    return sResul[1];
                }
                else
                {
                    sResul = D13_MetodoComp.Split(' ');
                    string sPAll = "";
                    int nR = 0;
                    foreach (string sP in sResul)
                    {
                        if (nR > 0)
                        {
                            sPAll = $"{sPAll} {sP}";
                        }
                        nR++;
                    }
                    return sPAll;
                }
            }
        }
    }
}
