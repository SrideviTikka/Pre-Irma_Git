using PreIRMA.WebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Net;
using System.Text;


namespace PreIRMA.WebSite.Controllers
{
    public abstract class BaseController : Controller
    {
        #region Send Mail
        /// <summary>
        /// Author: Satish
        /// Date: 8-05-2017
        /// Description: This region is Used for sending mails.
        /// </summary>
        /// <returns></returns>

        internal string SendMail(EmailAttributesModel mailitem)
        {
            string strMail = string.Empty;
            try
            {
                SmtpClient smtpClient = new SmtpClient(AppConfig.SMTPServerName, Convert.ToInt32(AppConfig.SMTPServerPort));
                smtpClient.Credentials = new NetworkCredential(AppConfig.SMTPEMAILFROM, AppConfig.SMTPEmailFromPassword);
                smtpClient.EnableSsl = true;
                MailMessage message = null;
                message = new MailMessage(mailitem.From, mailitem.To);
                message.To.Add(mailitem.To);
                message.Subject = mailitem.Subject;
                message.Body = mailitem.MessageBody;
                message.IsBodyHtml = true;
                if (mailitem.CC != "")
                {
                    message.To.Add(mailitem.CC);
                }
                message.BodyEncoding = UTF8Encoding.UTF8;
                message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                if (mailitem.AlternateView != null)
                {                   
                    message.AlternateViews.Add(mailitem.AlternateView);
                }
                smtpClient.Send(message);
                strMail = "1";
            }
            catch (Exception exception)
            {
                strMail = exception.Message;
            }

            return strMail;
        }

        #endregion

        #region AppConfig

        public static class AppConfig
        {
            internal static string SMTPServerName
            {
                get
                {
                    return ConfigurationManager.AppSettings.Get("SMTP_SERVERNAME");
                }
            }

            internal static string SMTPServerPort
            {
                get
                {
                    return ConfigurationManager.AppSettings.Get("SMTP_SERVERPORT");
                }
            }
            internal static string SMTPEMAILFROM
            {
                get
                {
                    return ConfigurationManager.AppSettings.Get("SMTP_EMAILFROM");
                }
            }
            internal static string SMTPEmailFromPassword
            {
                get
                {
                    return ConfigurationManager.AppSettings.Get("SMTP_EMAILFROMPASSWORD");
                }
            }
            internal static string SMTPEmailCC
            {
                get
                {
                    return ConfigurationManager.AppSettings.Get("SMTP_SMTPEmailCC");
                }
            }
            internal static string SMTPEmailARS
            {
                get
                {
                    return ConfigurationManager.AppSettings.Get("SMTP_SMTPEmailARS");
                }
            }
            internal static string SMTPEmailIRMA
            {
                get
                {
                    return ConfigurationManager.AppSettings.Get("SMTP_SMTPEmailIRMA");
                }
            }
            internal static string IRMAVERSION
            {
                get
                {
                    return ConfigurationManager.AppSettings.Get("SMTP_IRMAVERSION");
                }
            }
            internal static string SMTPPHNNO
            {
                get
                {
                    return ConfigurationManager.AppSettings.Get("SMTP_SMTPPHNNO");
                }
            }
        }
        #endregion

    }
}
