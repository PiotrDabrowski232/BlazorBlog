using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Blog.VerificationAzureFunction
{
    public class Function1
    {
        [FunctionName("Function1")]
        public void Run([QueueTrigger("verificationqueue", Connection = "Endpoint=sb://blazorblog-emailverification-queue.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=tBZYG0T+CJxyGkW/aKCiIrmeoIac86WLP+ASbIRcVtk=")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
