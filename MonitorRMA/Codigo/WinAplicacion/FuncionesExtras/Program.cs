// See https://aka.ms/new-console-template for more information
using static FuncionesExtras.ConsDatCunWeb;

Console.WriteLine("Seleccione una opcion. ");
Console.WriteLine("     0.- Salir .....");
Console.WriteLine("     1.- Extrae datos de Cuenta Unica.");
Console.WriteLine("");
OpcionMenu();

static void OpcionMenu()
{
    int NOpcion = -1;
    FuncionesExtras.ConsDatCunWeb? FuncionDatos = null;
    try
    {
        NOpcion = Convert.ToInt32(ObtenDatoPantalla("Opcion seleccionada:", TipoDato.Entero));
        switch (NOpcion)
        {
            case 0:
                break;
            case 1:
                FuncionDatos = new FuncionesExtras.ConsDatCunWeb();
                FuncionDatos.ConsultaDatosRuta();
                break;
            default: throw new Exception("Opcion invalida.");
        }
    }
    catch
    {
        Console.WriteLine("////////////// OPCION INVALIDA ///////////////. Favor de reintentar: ");
        OpcionMenu();
    }
}

static string ObtenDatoPantalla(string SDatoObtener, TipoDato TDato)
{
    string sResult = "";
    try
    {
        Console.Write(SDatoObtener);
        string? sDatoConsole = Console.ReadLine();

        sDatoConsole ??= "";
        sResult = sDatoConsole;
        switch (TDato)
        {
            case TipoDato.Entero:
                int.Parse(sDatoConsole);
                break;
            case TipoDato.Fecha:
                DateTime.Parse(sDatoConsole);
                break;
            case TipoDato.SiNo:
                Boolean.Parse(sDatoConsole);
                break;
            default:
                break;
        }
    }
    catch
    {
        Console.WriteLine("////////////// DATO INCORRECTO ///////////////. Favor de reintentar: ");
        ObtenDatoPantalla(SDatoObtener, TDato);
    }
    return sResult;
}