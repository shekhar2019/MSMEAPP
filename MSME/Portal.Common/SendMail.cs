
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace Portal.Common
{
    public class SendMail
    {
        #region "Send Mail"

        public bool SendEmail(string strToMail, string strSubject, string strBody)
        {
            bool bResult = false;
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    string displayName = Convert.ToString(ConfigurationManager.AppSettings["smtpDisplayName"]);
                    mail.To.Add(strToMail.Trim());
                    mail.From = new MailAddress(Convert.ToString(ConfigurationManager.AppSettings["smtpUser"]), displayName);
                    mail.Subject = strSubject.Trim();
                    mail.Body = strBody.Trim();
                    mail.IsBodyHtml = true;
                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Host = Convert.ToString(ConfigurationManager.AppSettings["smtpServer"]);
                        smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"]);

                        smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(Convert.ToString(ConfigurationManager.AppSettings["smtpUser"]), Convert.ToString(ConfigurationManager.AppSettings["smtpPass"]));
                        smtp.Timeout = 20000;
                        smtp.Send(mail);
                        bResult = true;
                    }
                }
                return bResult;
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }
        public bool SendEmail(string mailFrom, string strToMail, string strCCMail, string strSubject, string strBody)
        {
            bool bResult = false;
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.To.Add(strToMail.Trim());
                    mail.CC.Add(strCCMail.Trim());
                    string displayName = Convert.ToString(ConfigurationManager.AppSettings["smtpDisplayName"]);

                    mail.From = new MailAddress(Convert.ToString(ConfigurationManager.AppSettings["smtpUser"]), displayName);
                    mail.Subject = strSubject.Trim();
                    mail.Body = strBody.Trim();
                    mail.IsBodyHtml = true;
                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Host = Convert.ToString(ConfigurationManager.AppSettings["smtpServer"]);
                        smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"]);
                        smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(Convert.ToString(ConfigurationManager.AppSettings["smtpUser"]), Convert.ToString(ConfigurationManager.AppSettings["smtpPass"]));
                        smtp.Timeout = 20000;
                        smtp.Send(mail);
                        bResult = true;
                    }
                }
                return bResult;
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }
        public bool SendEmail(string mailFrom, string strToMail, string strSubject, string strBody)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    string displayName = Convert.ToString(ConfigurationManager.AppSettings["smtpDisplayName"]);

                    mailFrom = Convert.ToString(ConfigurationManager.AppSettings["smtpUser"]);
                    mail.To.Add(strToMail.Trim());
                    mail.From = new MailAddress(mailFrom, displayName);
                    mail.Subject = strSubject.Trim();
                    mail.Body = strBody.Trim();
                    mail.IsBodyHtml = true;
                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Host = Convert.ToString(ConfigurationManager.AppSettings["smtpServer"]);
                        smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"]);
                        smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(Convert.ToString(ConfigurationManager.AppSettings["smtpUser"]), Convert.ToString(ConfigurationManager.AppSettings["smtpPass"]));
                        smtp.Timeout = 20000;
                        smtp.Send(mail);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }
        public bool SendEmail(string mailFrom, string strToMail, string strSubject, string strBody, byte[] attachment, string fileName)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mailFrom = Convert.ToString(ConfigurationManager.AppSettings["smtpUser"]);
                    string displayName = Convert.ToString(ConfigurationManager.AppSettings["smtpDisplayName"]);
                    mail.To.Add(strToMail.Trim());
                    mail.From = new MailAddress(mailFrom, displayName);
                    mail.Subject = strSubject.Trim();
                    mail.Body = strBody.Trim();
                    mail.IsBodyHtml = true;
                    using (MemoryStream ms = new MemoryStream(attachment))
                    {
                        mail.Attachments.Add(new Attachment(ms, fileName));
                        using (SmtpClient smtp = new SmtpClient())
                        {
                            smtp.Host = Convert.ToString(ConfigurationManager.AppSettings["smtpServer"]);
                            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"]);
                            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
                            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtp.UseDefaultCredentials = false;
                            smtp.Credentials = new NetworkCredential(Convert.ToString(ConfigurationManager.AppSettings["smtpUser"]), Convert.ToString(ConfigurationManager.AppSettings["smtpPass"]));
                            smtp.Timeout = 20000;
                            smtp.Send(mail);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }
       
        public bool SendEmail(string mailFrom, string strToMail, string strCCMail, string strSubject, string strBody, byte[] attachment, string fileName)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    string displayName = Convert.ToString(ConfigurationManager.AppSettings["smtpDisplayName"]);

                    mailFrom = Convert.ToString(ConfigurationManager.AppSettings["smtpUser"]);
                    mail.To.Add(strToMail.Trim());
                    mail.CC.Add(strCCMail.Trim());
                    mail.From = new MailAddress(mailFrom, displayName);
                    mail.Subject = strSubject.Trim();
                    mail.Body = strBody.Trim();
                    mail.IsBodyHtml = true;
                    using (MemoryStream ms = new MemoryStream(attachment))
                    {
                        mail.Attachments.Add(new Attachment(ms, fileName));



                        using (SmtpClient smtp = new SmtpClient())
                        {
                            smtp.Host = Convert.ToString(ConfigurationManager.AppSettings["smtpServer"]);
                            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"]);
                            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
                            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtp.UseDefaultCredentials = false;
                            smtp.Credentials = new NetworkCredential(Convert.ToString(ConfigurationManager.AppSettings["smtpUser"]), Convert.ToString(ConfigurationManager.AppSettings["smtpPass"]));
                            smtp.Timeout = 20000;
                            smtp.Send(mail);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                return false;
                // throw ex;
            }
        }

        public bool SendEmail(string smtpUser, string strToMail, string strSubject, string strBody, byte[] attachment, string fileName,string smtpPassword,string smtpDisplayName,string smtpServer,int smtpPort,bool EnableSsl)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    string mailFrom = smtpUser;
                    string displayName = smtpDisplayName;
                    mail.To.Add(strToMail.Trim());
                    mail.From = new MailAddress(mailFrom, displayName);
                    mail.Subject = strSubject.Trim();
                    mail.Body = strBody.Trim();
                    mail.IsBodyHtml = true;
                    using (MemoryStream ms = new MemoryStream(attachment))
                    {
                        mail.Attachments.Add(new Attachment(ms, fileName));
                        using (SmtpClient smtp = new SmtpClient())
                        {
                            smtp.Host = smtpServer;
                            smtp.Port = smtpPort;
                            smtp.EnableSsl = EnableSsl;
                            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtp.UseDefaultCredentials = false;
                            smtp.Credentials = new NetworkCredential(smtpUser, smtpPassword);
                            smtp.Timeout = 20000;
                            smtp.Send(mail);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }
        #endregion
    }
}
