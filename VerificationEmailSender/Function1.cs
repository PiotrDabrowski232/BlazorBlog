using System;
using System.Net.Mail;
using System.Net;
using System.Text.Json;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using VerificationEmailSender.Models;
using VerificationEmailSender.Services;

namespace VerificationEmailSender
{
    public class Function1
    {

        [FunctionName("SendVerificationEmail")]
        public void Run([ServiceBusTrigger("verificationqueue", Connection = "VerificationQueue")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");

            var messageReceived = JsonConvert.DeserializeObject<MessageDto>(myQueueItem);

            var vault = new KeyVaultService();

            var senderMailName = vault.VaultDownloader("blogblazorEmailName").Result;
            var senderMailPassword = vault.VaultDownloader("blogblazorEmailPassword").Result;



            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(senderMailName);
            mailMessage.To.Add(messageReceived.Email);
            mailMessage.Subject = "Verificaation Code";
            mailMessage.Body = messageReceived.MessageBody();

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(senderMailName, senderMailPassword);
            smtpClient.EnableSsl = true;


            smtpClient.Send(mailMessage);

        }
    }
}
