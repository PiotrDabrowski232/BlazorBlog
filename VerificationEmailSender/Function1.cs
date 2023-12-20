using System;
using Blog.Logic.Dto.Messages;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace VerificationEmailSender
{
    public class Function1
    {
        [FunctionName("SendVerificationMessage")]
        public void Run([ServiceBusTrigger("verificationqueue", Connection = "VerificationQueue")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");

            var messageReceived = JsonConvert.DeserializeObject<MessageDto>(myQueueItem);
        }
    }
}
