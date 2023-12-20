using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace VerificationEmailSender
{
    public class Function1
    {
        [FunctionName("SendVerificationEmail")]
        public void Run([QueueTrigger("verificationqueue", Connection = "verificationqueueConnection")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
