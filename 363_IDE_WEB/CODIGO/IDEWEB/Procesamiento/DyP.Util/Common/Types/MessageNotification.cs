//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Util.Types:MessageNotification:0:21/May/2008[SAT.DyP.Util.Types:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SAT.DyP.Util.Types
{
    /// <summary>
    /// Clase que representa los datos básicos del envió de una notificación
    /// </summary>
    [Serializable]
    public class MessageNotification
    {
        private string _Email;
        private string _Subject;
        private string _Message;
        private List<MessageNotificationAttachment> _Attachments;

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        public string Subject
        {
            get { return _Subject; }
            set { _Subject = value; }
        }

        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        public List<MessageNotificationAttachment> Attachments
        {
            get { return _Attachments; }
            set { _Attachments = value; }
        }
    }


    [Serializable]
    public class MessageNotificationAttachment
    {
        private Stream contentStream;
        private string contentName;

        public Stream ContentStream
        {
            get { return contentStream; }
            set { contentStream = value; }
        }

        public string ContentName
        {
            get { return contentName; }
            set { contentName = value; }
        }
    }
}
