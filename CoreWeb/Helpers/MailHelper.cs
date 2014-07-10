using System.Collections;
using System.Net.Mail;
using System.Web;

namespace CoreWeb.Helpers
{
    /// <summary>
    /// Helper class for all email related tasks.
    /// </summary>
    public class MailHelper
    {
        /// <summary>
        /// This function is used to send email with attachments
        /// The strFilenames should follow this format filename1|filename2|filename3....
        /// </summary>
        public static void SendMailWithPostedAttachments(string pstrFrom, string pstrTo, string pstrSubject, string pstrBody, string smtpHost, ArrayList parrFileNames)
        {
            using (SmtpClient scClient = new SmtpClient(smtpHost))
            {
                using (MailMessage mmMessage = new MailMessage(pstrFrom, pstrTo, pstrSubject, pstrBody))
                {
                    mmMessage.IsBodyHtml = true;
                    foreach (object objAttachment in parrFileNames)
                    {
                        HttpPostedFile objPostedFile = (HttpPostedFile)objAttachment;
                        mmMessage.Attachments.Add(new Attachment(objPostedFile.InputStream, System.IO.Path.GetFileName(objPostedFile.FileName)));

                    }
                    scClient.Send(mmMessage);
                }
            }
        }

        /// <summary>
        /// Send a formatted email.
        /// </summary>
        public static void SendFormattedMail(MailAddress pobjMailAddressFrom, MailAddressCollection pobjMailAddressTo,
            string pstrSubject, string pstrHTMLBody, string pstrTextBody, string smtpHost, 
            ArrayList parrPostedFiles = null, ArrayList parrFiles = null)
        {
            using (SmtpClient objSMTPClient = new SmtpClient(smtpHost))
            {
                using (MailMessage objMessage = new MailMessage())
                {
                    foreach (MailAddress objMailToAddress in pobjMailAddressTo)
                    {
                        objMessage.To.Add(objMailToAddress);
                    }

                    objMessage.From = pobjMailAddressFrom;
                    objMessage.Subject = pstrSubject;

                    AlternateView objAlternateViewText = null;
                    if (!string.IsNullOrEmpty(pstrTextBody))
                    {
                        objAlternateViewText = AlternateView.CreateAlternateViewFromString(pstrTextBody, null, "text/plain");
                        objMessage.AlternateViews.Add(objAlternateViewText);
                    }
                    AlternateView objAlternateViewHTML = null;
                    if (!string.IsNullOrEmpty(pstrHTMLBody))
                    {
                        objAlternateViewHTML = AlternateView.CreateAlternateViewFromString(pstrHTMLBody, null, "text/html");
                        objMessage.AlternateViews.Add(objAlternateViewHTML);
                    }

                    //---------Adds Posted Files as attachments--------
                    if (parrPostedFiles != null)
                    {
                        foreach (object objAttachment in parrPostedFiles)
                        {
                            HttpPostedFile objPostedFile = (HttpPostedFile)objAttachment;
                            objMessage.Attachments.Add(new Attachment(objPostedFile.InputStream, objPostedFile.FileName.Substring(objPostedFile.FileName.LastIndexOf("\\"))));
                        }
                    }

                    //---------Adds Files from the Server as attachments--------
                    if (parrFiles != null)
                    {
                        foreach (object objAttachment in parrFiles)
                        {
                            string strFileName = (string)objAttachment;
                            objMessage.Attachments.Add(new Attachment(strFileName));
                            //NOTE: Full path must be specified 
                        }
                    }
                    objSMTPClient.Send(objMessage);
                }
            }
        }
    }
}
