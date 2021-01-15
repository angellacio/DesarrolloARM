//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Util.Processors.Mail:SendMail:0:21/May/2008[ SAT.DyP.Util.Processors:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Mail;
using SAT.DyP.Util.Configuration;
using SAT.DyP.Util.Types;
using SAT.DyP.Util.Logging;
using SAT.DyP.Util.ExceptionHandling;
using System.IO;
using System.Diagnostics;

namespace SAT.DyP.Util.Processors.Mail
{
    /// <summary>
    /// Clase que presta el servicio de envio de correo electrónico.
    /// </summary>
    public class SendMail
    {
        /// <summary>
        /// Envia un mensaje via correo electrónico.
        /// </summary>
        /// <param name="mailMessage">El mensaje de correo a ser enviado.</param>
        public static bool Execute(MessageNotification Message)
        {
            bool bResult = false;

            if (Message == null)
            {
                throw new Exception("Object MessageNotification is null.");
            }

            if (string.IsNullOrEmpty(Message.Email))
            {
                throw new ArgumentNullException("No existe la dirección de correo electrónico del destinatario.");
            }

            try
            {
                MailMessage _mailMessage = CreateNewMessage(Message);

                // FSM @ PPMC 67498
                if (Message.Attachments != null)
                    foreach (MessageNotificationAttachment Mna in Message.Attachments)
                        _mailMessage.Attachments.Add(new Attachment(Mna.ContentStream, Mna.ContentName));

                // FSM @ PPMC 67498

               SmtpClient _client = new SmtpClient();
                _client.Host = ConfigurationManager.ApplicationSettings.ReadSetting(newConfigurationConstants.MAIL_SERVER);
                NetworkCredential _permisos = new NetworkCredential("declaraciones.electronicas", "");
                _client.Credentials = _permisos;

                _client.Send(_mailMessage);

                if (IsLoggingEnabled)
                {
                    LogSentMessage(_mailMessage);
                }

                bResult = true;
            }
            catch (ArgumentNullException ex)
            {
                EventLogHelper.WriteWarningEntry(ex.Message, CoreLogEventIdentifier.PROCESSOR_MAIL_EVENT_ID);
            }
            catch (Exception ex)
            {
                EventLogHelper.WriteWarningEntry("Fail send mail." + ex.Message, CoreLogEventIdentifier.PROCESSOR_MAIL_EVENT_ID);
            }

            return bResult;
        }

        /// <summary>
        /// Método de diagnóstico que, cuando está habilitado por un setting de la base de datos,
        /// guarda cada mensaje enviado en un archivo de texto en un directorio.
        /// </summary>
        /// <param name="_mailMessage">El mensaje a guardar</param>
        private static void LogSentMessage(MailMessage _mailMessage)
        {
            string path = null;

            try
            {
                path = ConfigurationManager.ApplicationSettings.ReadSetting(newConfigurationConstants.MAIL_DEBUG_LOG_SENT_MESSAGES_PATH);
            }
            catch (Exception ex)
            {
                string warningMessage = String.Format(
                                                      "No se pudo escribir mensaje de correo enviado porque el parámetro de configuración '{0}' no existe. {1}",
                                                      newConfigurationConstants.MAIL_DEBUG_LOG_SENT_MESSAGES_PATH, ex
                                                      );
                EventLogHelper.WriteEntry(warningMessage, System.Diagnostics.EventLogEntryType.Warning, CoreLogEventIdentifier.PROCESSOR_MAIL_EVENT_ID);
                return;
            }

            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception ex)
            {
                string warningMessage = String.Format(
                                                      "No se pudo escribir mensaje de correo enviado porque directorio '{0}' no existe y no se puede crear. {1}",
                                                      newConfigurationConstants.MAIL_DEBUG_LOG_SENT_MESSAGES_PATH, ex
                                                      );
                EventLogHelper.WriteEntry(warningMessage, System.Diagnostics.EventLogEntryType.Warning, CoreLogEventIdentifier.PROCESSOR_MAIL_EVENT_ID);
                return;
            }

            try
            {
                string fullPath = Path.Combine(path, String.Format("MailMessage-{0}.txt", Guid.NewGuid().ToString()));
                StreamWriter writer = File.CreateText(fullPath);
                writer.Write(_mailMessage.Body);
                writer.Flush();
                writer.Close();
            }
            catch (Exception ex)
            {
                string warningMessage = String.Format("No se pudo escribir el mensaje de correo enviado al log por el siguiente error: {0}", ex);
                EventLogHelper.WriteEntry(warningMessage, System.Diagnostics.EventLogEntryType.Warning, CoreLogEventIdentifier.PROCESSOR_MAIL_EVENT_ID);
                return;
            }
        }

        private static bool IsLoggingEnabled
        {
            get
            {
                string logEnabled = "";
                try
                {
                    logEnabled = ConfigurationManager.ApplicationSettings.ReadSetting(newConfigurationConstants.MAIL_DEBUG_LOG_SENT_MESSAGES);
                }
                catch
                {
                    // no pasa nada si no existe el config setting, de hecho, para eso es, para ponerlo sólo cuando se necesita.
                    return false;
                }

                if (logEnabled == "true")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Crea una nueva instancia de MailMessage para preparar un envío
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static System.Net.Mail.MailMessage CreateNewMessage(MessageNotification message)
        {
            MailMessage _newMessage = null;

            try
            {
                string _emailFrom = ConfigurationManager.ApplicationSettings.ReadSetting(newConfigurationConstants.MAIL_FROM);

                _newMessage = new MailMessage();
                _newMessage.From = new MailAddress(_emailFrom);

                /* FSM @ PPMC 65622 */

                foreach (string strTo in message.Email.Split(';'))
                    _newMessage.To.Add(new MailAddress(strTo.Trim()));

                /* FSM @ PPMC 65622 */

                _newMessage.IsBodyHtml = true;
                _newMessage.Subject = message.Subject;
                _newMessage.Body = message.Message;

            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("Error al crear el mensaje de correo:{0}", ex.Message);
                EventLogHelper.WriteErrorEntry(errorMessage, CoreLogEventIdentifier.PROCESSOR_MAIL_EVENT_ID);
            }

            return _newMessage;
        }
    }
}