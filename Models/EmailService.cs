using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Net.Mail;


namespace OlwandleHotel.Models
{
    public class EmailService
    {
        private SmtpClient smtpClient;
        private const string Host = "Host", Port = "Port", EmailFrom = "EmailFrom", PassKey = "PassKey";
        protected MailAddress mailFrom { get; set; }
        public EmailService()
        {
            smtpClient = new SmtpClient(ConfigurationManager.AppSettings[Host], int.Parse(ConfigurationManager.AppSettings[Port]));
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings[EmailFrom], ConfigurationManager.AppSettings[PassKey]);
            mailFrom = new MailAddress(ConfigurationManager.AppSettings[EmailFrom], "Car Wash Ltd");
        }
        public void SendEmail(EmailContent emailContent)
        {
            MailMessage message = new MailMessage();
            message.From = mailFrom;
            foreach (MailAddress address in emailContent.mailTo)
                message.To.Add(address);
            foreach (MailAddress address in emailContent.mailCc)
                message.CC.Add(address);
            message.Subject = emailContent.mailSubject;
            message.Priority = emailContent.mailPriority;
            message.Body = emailContent.mailBody + "<br/<br/>" + emailContent.mailFooter;
            message.IsBodyHtml = true;
            foreach (Attachment attachment in emailContent.mailAttachments)
                message.Attachments.Add(attachment);

            try
            {
                smtpClient.Send(message);
            }
            catch { }
        }

        //public class Attachment
        //{
        //    public string fileName { get; set; }
        //    public byte content { get; set; }
        //    public string type { get; set; }//For example, application/pd f or image/jpeg
        //    public string disposition { get; set; }//Can be either attachment or inline
        //}
    }
    public class EmailContent
    {
        public List<MailAddress> mailTo { get; set; }
        public List<MailAddress> mailCc { get; set; }
        public string mailSubject { get; set; }
        public string mailBody { get; set; }
        public string mailFooter { get; set; }
        public MailPriority mailPriority { get; set; }
        public List<Attachment> mailAttachments { get; set; }
    }
}