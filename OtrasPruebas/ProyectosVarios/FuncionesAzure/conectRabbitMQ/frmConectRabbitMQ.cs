using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using rbmqE = RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Security;
using System.Reflection;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
//using static System.Formats.Asn1.AsnWriter;

namespace conectRabbitMQ
{
    public class frmConectRabbitMQ
    {
        private string RabbitMQ_String { get { return ConfigurationManager.AppSettings["RabbitMQ_String"].ToString(); } }
        private string RabbitMQ_URL { get { return ConfigurationManager.AppSettings["RabbitMQ_URL"].ToString(); } }
        private int RabbitMQ_Port { get { return int.Parse(ConfigurationManager.AppSettings["RabbitMQ_Port"].ToString()); } }
        private string RabbitMQ_User { get { return ConfigurationManager.AppSettings["RabbitMQ_User"].ToString(); } }
        private string RabbitMQ_Pasword { get { return ConfigurationManager.AppSettings["RabbitMQ_Pasword"].ToString(); } }
        private Boolean RabbitMQ_Ssl { get { return ConfigurationManager.AppSettings["RabbitMQ_Ssl"].ToString() == "1"; } }
        private ConnectionFactory factory { get; set; }

        public frmConectRabbitMQ()
        {
            factory = new ConnectionFactory
            {
                HostName = RabbitMQ_URL,

                UserName = RabbitMQ_User,
                Password = RabbitMQ_Pasword,

                Port = RabbitMQ_Port
            };

            // Habilitar SSL/TLS
            if (RabbitMQ_Ssl) // Opcional: Para desarrollo, si el certificado es auto-firmado
            {
                factory.Ssl = new SslOption()
                {
                    Enabled = true,
                    AcceptablePolicyErrors = SslPolicyErrors.RemoteCertificateNameMismatch | SslPolicyErrors.RemoteCertificateChainErrors
                };
            }
        }

        public async Task RabbitMQ_EscribeQueu_FWAnt(string sCola, string sMensaje)
        {
            IConnection connection = null;
            IChannel channel = null;

            try
            {
                // 1. Crea conexion a RabbitMQ. Parametros separados.
                connection = await factory.CreateConnectionAsync();
                channel = await connection.CreateChannelAsync();

                // 2. Declarar la cola (idempotente: solo se crea si no existe)
                channel.QueueDeclareAsync(queue: sCola, durable: true, exclusive: false, autoDelete: false, arguments: null);

                var body = Encoding.UTF8.GetBytes(sMensaje);

                // 3. Publicar el mensaje
                channel.BasicPublishAsync(exchange: "", routingKey: sCola, body: body);

                Console.WriteLine($"[x] Enviado: {sMensaje}");

            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("******************Error Funcion RabbitMQ_EscribeQueu(" + sCola + ", " + sMensaje + ")");
                Console.WriteLine(ex.ToString());
                Console.WriteLine();
                Console.WriteLine();
            }
            finally
            {
                if (channel != null)
                {
                    await channel.CloseAsync();
                    await channel.DisposeAsync();
                }
                if (connection != null)
                {
                    await connection.CloseAsync();
                    await connection.DisposeAsync();
                }
                if (factory != null)
                {
                    factory = null;
                }
            }
        }

        public async Task<List<string>> RabbitMQ_LeeQueu_Array(string sCola, Boolean bolAutoDelete)
        {
            List<string> lstResult = new List<string>();

            IConnection connection = null;
            IChannel channel = null;

            try
            {
                // 1. Crea conexion a RabbitMQ. Parametros separados.
                connection = await factory.CreateConnectionAsync();
                channel = await connection.CreateChannelAsync();

                // 2. Declarar la cola (idempotente: solo se crea si no existe)
                channel.QueueDeclareAsync(queue: sCola, durable: true, exclusive: false, autoDelete: false, arguments: null);

                //channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 10, global: false);

                // 3. Crear el consumidor
                var consumer = new rbmqE.AsyncEventingBasicConsumer(channel);

                consumer.ReceivedAsync += async (model, ea) =>
                {
                    //Console.WriteLine($"[x] RM Inicio: Array {ea.Body.ToArray()}");
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    lstResult.Add(message);

                    //Console.WriteLine($" - [x] RM Mensaje recibido: {message}");

                    // Confirmar entrega manual
                    //await channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);

                    //Console.WriteLine($" - [x] RM Fin: {ea.Body.ToString()}");
                };

                // 4. Iniciar el consumo
                channel.BasicConsumeAsync(queue: sCola, autoAck: bolAutoDelete, consumer: consumer);

                // Simular trabajo asíncrono
                await Task.Delay(10000);

                Console.WriteLine($"");
                Console.WriteLine($"Se encontraron {lstResult.Count} registros.");

                
                

            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("******************Error Funcion RabbitMQ_LeeQueu(" + sCola + ")");
                Console.WriteLine(ex.ToString());
                Console.WriteLine();
                Console.WriteLine();
            }
            finally
            {
                if (channel != null)
                {
                    await channel.CloseAsync();
                    await channel.DisposeAsync();
                }
                if (connection != null)
                {
                    await connection.CloseAsync();
                    await connection.DisposeAsync();
                }
                if (factory != null)
                {
                    factory = null;
                }
            }
            return lstResult;
        }
        public async Task<List<string>> RabbitMQ_LeeQueu_Individual(string sCola, Boolean bolAutoDelete)
        {
            int nRenglonesC = 15, nMensajesE = 0;

            List<string> lstResult = new List<string>();

            IConnection connection = null;
            IChannel channel = null;

            try
            {
                // 1. Crea conexion a RabbitMQ. Parametros separados.
                connection = await factory.CreateConnectionAsync();
                channel = await connection.CreateChannelAsync();

                for (int nRenglon = 0; nRenglonesC - 1 >= nRenglon; nRenglon++)
                {
                    var result = await channel.BasicGetAsync(sCola, autoAck: false);

                    if (result == null)// No hay más mensajes en la cola
                        break;

                    // Confirmar que el mensaje fue procesado
                    await channel.BasicAckAsync(result.DeliveryTag, multiple: false);

                    var message = Encoding.UTF8.GetString(result.Body.Span.ToArray());
                    

                    nMensajesE++;

                    Console.WriteLine($"Mensaje {nRenglon + 1}: {message}");
                }

                Console.WriteLine($"Siclos {nRenglonesC} :: MensajesEncontrados {nMensajesE}");
                //// Simular trabajo asíncrono
                //await Task.Delay(100);

            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("******************Error Funcion RabbitMQ_LeeQueu(" + sCola + ")");
                Console.WriteLine(ex.ToString());
                Console.WriteLine();
                Console.WriteLine();
            }
            finally
            {
                if (channel != null)
                {
                    await channel.CloseAsync();
                    await channel.DisposeAsync();
                }
                if (connection != null)
                {
                    await connection.CloseAsync();
                    await connection.DisposeAsync();
                }
                if (factory != null)
                {
                    factory = null;
                }
            }
            return lstResult;
        }

        //public async void RabbitMQ_LeeQueu_CerrarCon()
        //{
        //    try
        //    {
        //        if (channel != null)
        //        {
        //            channel.CloseAsync();
        //            channel.Dispose();
        //        }
        //        if (connection != null)
        //        {
        //            connection.CloseAsync();
        //            connection.Dispose();
        //        }
        //        if (factory != null)
        //        {
        //            factory = null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine();
        //        Console.WriteLine("******************Error Funcion RabbitMQ_LeeQueu_CerrarCon()");
        //        Console.WriteLine(ex.ToString());
        //        Console.WriteLine();
        //        Console.WriteLine();
        //    }
        //}

        //public static async void RabbitMQ_EscribeQueu(string sCola, string sMensaje)
        //{
        //    // 1. Crea conexion a RabbitMQ. Parametros separados.
        //    ConnectionFactory factory = new ConnectionFactory
        //    {
        //        HostName = RabbitMQ_URL,

        //        UserName = RabbitMQ_User,
        //        Password = RabbitMQ_Pasword,

        //        Port = RabbitMQ_Port
        //    };

        //    try
        //    {
        //        // Habilitar SSL/TLS
        //        if (RabbitMQ_Ssl) // Opcional: Para desarrollo, si el certificado es auto-firmado
        //        {
        //            factory.Ssl = new SslOption()
        //            {
        //                Enabled = true,
        //                AcceptablePolicyErrors = SslPolicyErrors.RemoteCertificateNameMismatch | SslPolicyErrors.RemoteCertificateChainErrors
        //            };
        //        }

        //        using IConnection connection = await factory.CreateConnectionAsync();
        //        using IChannel channel = await connection.CreateChannelAsync();

        //        // 2. Declarar la cola (idempotente: solo se crea si no existe)
        //        channel.QueueDeclareAsync(queue: sCola, durable: true, exclusive: false, autoDelete: false, arguments: null);

        //        var body = Encoding.UTF8.GetBytes(sMensaje);

        //        // 3. Publicar el mensaje
        //        channel.BasicPublishAsync(exchange: "", routingKey: sCola, body: body);

        //        Console.WriteLine($"[x] Enviado: {sMensaje}");

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine();
        //        Console.WriteLine("******************Error Funcion RabbitMQ_EscribeQueu(" + sCola + ", " + sMensaje + ")");
        //        Console.WriteLine(ex.ToString());
        //        Console.WriteLine();
        //        Console.WriteLine();
        //    }
        //    finally { }
        //}

        //public static async void RabbitMQ_LeeQueu(string sCola)
        //{
        //    ConnectionFactory factory = null;
        //    IConnection connection = null;
        //    IChannel channel = null;

        //    // 1. Crea conexion a RabbitMQ. Parametros separados.
        //    factory = new ConnectionFactory
        //    {
        //        HostName = RabbitMQ_URL,

        //        UserName = RabbitMQ_User,
        //        Password = RabbitMQ_Pasword,

        //        Port = RabbitMQ_Port
        //    };

        //    try
        //    {
        //        // Habilitar SSL/TLS
        //        if (RabbitMQ_Ssl) // Opcional: Para desarrollo, si el certificado es auto-firmado
        //        {
        //            factory.Ssl = new SslOption()
        //            {
        //                Enabled = true,
        //                AcceptablePolicyErrors = SslPolicyErrors.RemoteCertificateNameMismatch | SslPolicyErrors.RemoteCertificateChainErrors
        //            };
        //        }

        //        connection = await factory.CreateConnectionAsync();
        //        channel = await connection.CreateChannelAsync();

        //        // 2. Declarar la cola (idempotente: solo se crea si no existe)
        //        channel.QueueDeclareAsync(queue: sCola, durable: true, exclusive: false, autoDelete: false, arguments: null);

        //        var consumer = new AsyncDefaultBasicConsumer(channel);
        //        channel.BasicConsumeAsync(sCola, false, consumer);

        //        while (true)
        //        {
        //            var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
        //            var body = ea.Body;
        //            var message = Encoding.UTF8.GetString(body);

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine();
        //        Console.WriteLine("******************Error Funcion RabbitMQ_LeeQueu(" + sCola + ")");
        //        Console.WriteLine(ex.ToString());
        //        Console.WriteLine();
        //        Console.WriteLine();
        //    }
        //    finally
        //    {
        //        if (channel != null)
        //        {
        //            channel.CloseAsync();
        //            channel.Dispose();
        //        }
        //        if (connection != null)
        //        {
        //            connection.CloseAsync();
        //            connection.Dispose();
        //        }
        //    }
        //}
    }
}
