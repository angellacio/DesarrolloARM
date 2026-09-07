// See https://aka.ms/new-console-template for more information
using conectRabbitMQ;
//using RabbitMQ.Client;
//using System.Text;

//class Program
//{
//    static async Task Main()
//    {
//        var factory = new ConnectionFactory()
//        {
//            HostName = "b-51bfae8b-1efc-44b8-8fcd-34f5bba79b45.mq.us-east-1.amazonaws.com",
//            UserName = "brokerdpascliente",
//            Password = "dpasadminmq11*",
//            Port = 5671,
//            Ssl = new SslOption
//            {
//                Enabled = true,
//                ServerName = "b-51bfae8b-1efc-44b8-8fcd-34f5bba79b45.mq.us-east-1.amazonaws.com"
//            }
//        };

//        try
//        {
//            using var connection = await factory.CreateConnectionAsync();
//            using var channel = await connection.CreateChannelAsync();

//            Console.WriteLine("Conectado correctamente a RabbitMQ");

//            string queueName = "q.dpa.request";

//            await channel.QueueDeclareAsync(
//                queue: queueName,
//                durable: true,
//                exclusive: false,
//                autoDelete: false,
//                arguments: null);

//            string message = "Hola desde .NET";
//            var body = Encoding.UTF8.GetBytes(message);

//            await channel.BasicPublishAsync(
//                exchange: "",
//                routingKey: queueName,
//                body: body);

//            Console.WriteLine("Mensaje enviado");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine("Error:");
//            Console.WriteLine(ex.ToString());
//        }
//    }
//}



public class Program
{
    private static async Task Main(string[] args)
    {
        int nOpion = -1;


        nOpion = Menu();
        //frmConectRabbitMQ Rabbit = new frmConectRabbitMQ();

        //frmConectRabbitMQ.RabbitMQ_CS_EscribeQueu("q.dpa.request", "¡Hola RabbitMQ desde C#! StringConexion");
        //frmConectRabbitMQ.RabbitMQ_EscribeQueu("q.dpa.response", "¡Hola RabbitMQ desde C#!");
        //frmConectRabbitMQ.RabbitMQ_EscribeQueu_FWAnt("q.dpa.response", "¡Hola RabbitMQ desde C#! FrameWork 7.3");
        //frmConectRabbitMQ.RabbitMQ_CS_EscribeQueu("q.dpa.response", "¡Hola RabbitMQ desde C#! StringConexion");


    }

    public static int Menu()
    {
        string? sOpcion = null;
        int nOpion = -1;

        Console.Clear();

        Console.WriteLine("****************************************************************************************");
        Console.WriteLine("  Hola, proceso manuel para la conexion a ***************** RabbitMQ *****************");
        Console.WriteLine("");
        Console.WriteLine("  1 - Enviar a la cola Request");
        Console.WriteLine("  2 - Enviar a la cola Response");
        Console.WriteLine("  3 - Leer Individaul la cola Response");
        Console.WriteLine("  4 - Leer Aray la cola Response");
        Console.WriteLine("  5 - Leer Aray la cola Response, eliminando mensajes.");
        Console.WriteLine("  9 - SALIR");
        Console.WriteLine("");
        Console.WriteLine("****************************************************************************************");
        Console.Write("Selecciona una opcion: ");
        sOpcion = Console.ReadLine();

        try
        {
            nOpion = int.Parse(sOpcion);
        }
        catch
        {
            nOpion = -1;
        }

        switch (nOpion)
        {
            case 1:
                Menu1_1();
                break;
            case 2:
                EnviarMensajeRQ();
                break;
            case 3:
                LeerColaIndividual(false);
                break;
            case 4:
                LeerCola_Array(true);
                break;
            case 5:
                LeerCola_Array(true);
                break;
            case 9:

                break;
            default:
                Console.WriteLine(" //// La opcion selecionado no es valido");
                Menu();
                break;
        }

        return nOpion;
    }

    public static int Menu1_1()
    {
        string? sOpcion = null;
        int nOpion = -1;

        Console.Clear();

        Console.WriteLine("****************************************************************************************");
        Console.WriteLine("  Envio de colas *****************");
        Console.WriteLine("");
        Console.WriteLine("  1 - Enviar Persona Fisica con un tramite");
        Console.WriteLine("  2 - Enviar Persona Moral con un tramite");
        Console.WriteLine("  3 - Enviar Persona Fisica con varios tramite");
        Console.WriteLine("  4 - Enviar Persona Moral con varios tramite");
        Console.WriteLine("  9 - REGRESAR");
        Console.WriteLine("");
        Console.WriteLine("****************************************************************************************");
        Console.Write("Selecciona una opcion: ");
        sOpcion = Console.ReadLine();

        try
        {
            nOpion = int.Parse(sOpcion);
        }
        catch
        {
            nOpion = -1;
        }

        switch (nOpion)
        {
            case 1:
                EnviarMensajeMQ("q.dpa.request", "SolicitudDPAs_F_T1.json");
                break;
            case 2:
                EnviarMensajeMQ("q.dpa.request", "SolicitudDPAs_M_T1.json");
                break;
            case 3:
                EnviarMensajeMQ("q.dpa.request", "SolicitudDPAs_F_TV.json");
                break;
            case 4:
                EnviarMensajeMQ("q.dpa.request", "SolicitudDPAs_M_TV.json");
                break;
            case 9:
                Menu();
                break;
            default:
                Menu1_1();
                break;
        }

        return nOpion;
    }

    public static async void EnviarMensajeRQ()
    {
        frmConectRabbitMQ manejoRabbitMQ;
        int nMensajes = -1;
        string sMensajes = string.Empty;


        Console.WriteLine("****************************************************************************************");
        Console.WriteLine("  Hola, proceso manuel para la conexion a ***************** RabbitMQ ***************** Cola Responce");
        Console.WriteLine("");
        Console.Write("  *** Cuantos registros si quieren ingresar: ");
        sMensajes = Console.ReadLine();

        try
        {
            nMensajes = int.Parse(sMensajes);
        }
        catch
        {
            nMensajes = -1;
        }

        if (nMensajes > 0)
        {
            manejoRabbitMQ = new frmConectRabbitMQ();
            for (int i = 0; i < nMensajes; i++)
            {
                if (manejoRabbitMQ == null) manejoRabbitMQ = new frmConectRabbitMQ();
                manejoRabbitMQ.RabbitMQ_EscribeQueu_FWAnt("q.dpa.response", $"{{\"solicitud\":\"1340012505191000{i}\",\"idDocumento\":{15 * (i + 1)},\"codigo\":0,\"mensaje\":\"registrado correctamente\"}}");
            }
        }
        else if (nMensajes == -1)
        {
            Console.WriteLine(" //// La opcion selecionado no es valido");
            EnviarMensajeRQ();
        }

        Console.WriteLine("");
        Console.WriteLine("");
        Console.WriteLine("Presiona [enter] para regresar al menu inicial.");
        Console.WriteLine("");
        Console.WriteLine("");
        Console.ReadLine();
        Menu();
    }
    public static async void EnviarMensajeMQ(string sCola, string sArchivo)
    {
        frmConectRabbitMQ manejoRabbitMQ = new frmConectRabbitMQ();

        manejoRabbitMQ.RabbitMQ_EscribeQueu_FWAnt(sCola, File.ReadAllText(string.Format(@"C:\InfAco\Otros\ProyectosVarios\FuncionesAzure\conectRabbitMQ\{0}", sArchivo)));

        Console.WriteLine("");
        Console.WriteLine("");
        Console.WriteLine("Presiona [enter] para regresar al menu inicial.");
        Console.WriteLine("");
        Console.WriteLine("");
        Console.ReadLine();
        Menu1_1();
    }

    public static async void LeerColaIndividual(Boolean bolEliminarMensajesMQ)
    {
        frmConectRabbitMQ manejoRabbitMQ = new frmConectRabbitMQ();
        List<string> lsDatosQueue = null;
        Task<List<string>> tskDatoQueue = null;

        //tkDatosQueue = await frmConectRabbitMQ.RabbitMQ_LeeQueu("q.dpa.response");
        tskDatoQueue = manejoRabbitMQ.RabbitMQ_LeeQueu_Individual("q.dpa.response", bolEliminarMensajesMQ);

        //Task.WaitAll(tskDatoQueue);

        //lsDatosQueue = tskDatoQueue.Result;

        //var dTipoRespuesta = new
        //{
        //    solicitud = string.Empty,
        //    idDocumento = string.Empty,
        //    codigo = -1,
        //    mensaje = string.Empty
        //};

        //lsDatosQueue.ForEach(itemResp =>
        //{
        //    string sMensajeQue = itemResp;

        //    try
        //    {
        //        var datoRespuesta = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(sMensajeQue, dTipoRespuesta);

        //        //Console.WriteLine($" -- Solicitud: {datoRespuesta.solicitud} -- IdDocumento: {datoRespuesta.idDocumento} -- Codigo: {datoRespuesta.codigo} -- Mensaje: {datoRespuesta.mensaje}");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"");
        //        Console.WriteLine($"Error al convertir el mensaje: {sMensajeQue} ** mensaje de error: {ex.Message}");
        //        Console.WriteLine($"");
        //    }
        //    finally { }
        //});

        Console.WriteLine($"");
        //Console.WriteLine($"Datos encontrados {lsDatosQueue.Count}");
        Console.WriteLine($"");



        Console.WriteLine("Presiona [enter] para salir.");
        Console.ReadLine();
        Menu();
    }
    public static async void LeerCola_Array(Boolean bolEliminarMensajesMQ)
    {
        frmConectRabbitMQ manejoRabbitMQ = new frmConectRabbitMQ();
        List<string> lsDatosQueue = null;
        Task<List<string>> tskDatoQueue = null;

        //tkDatosQueue = await frmConectRabbitMQ.RabbitMQ_LeeQueu("q.dpa.response");
        tskDatoQueue = manejoRabbitMQ.RabbitMQ_LeeQueu_Array("q.dpa.response", bolEliminarMensajesMQ);

        Console.WriteLine($"");
        //Console.WriteLine($"Datos encontrados {lsDatosQueue.Count}");
        Console.WriteLine($"");



        Console.WriteLine("Presiona [enter] para salir.");
        Console.ReadLine();
        Menu();
    }
}