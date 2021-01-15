//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Util.Processors.Mail:CreateMailTemplate:0:21/May/2008[ SAT.DyP.Util.Processors:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using SAT.DyP.Util.Processors.Mail.Data;
using SAT.DyP.Util.ExceptionHandling;
using SAT.DyP.Util.Processors.Xslt;

namespace SAT.DyP.Util.Processors.Mail
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateMailTemplate
    {
        /// <summary>
        /// Creación del  para mandar correo
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="templateCode"></param>
        /// <returns></returns>
        public static string Execute(object entity, string templateCode)
        {
            string _result = string.Empty;
            TemplateProcessor _template = new TemplateProcessor();
            try
            {
                string _templateContent = LoadTemplate(templateCode);

                if (!string.IsNullOrEmpty(_templateContent))
                {
                    _template.Initialize(_templateContent);

                    _result = _template.Transform(entity);
                }
                else
                {
                    throw new SAT.DyP.Util.Types.PlatformException("Invalid XSLT Template");
                }
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionHandler.HandleException(ex);
                if (rethrow)                   
                    throw new SAT.DyP.Util.Types.PlatformException(ex.Message, ex);
            }

            return _result;
        }

        private static string LoadTemplate(string templateCode)
        {
            DALTemplate _dao = new DALTemplate();
            string _result=_dao.LoadTemplateContent(templateCode);

            return _result;
        }
    }
}
