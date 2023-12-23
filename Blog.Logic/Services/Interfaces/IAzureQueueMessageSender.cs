using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Logic.Services.Interfaces
{
    public interface IAzureQueueMessageSender
    {
        public Task SendMessageAsync(string message, string queueName);
    }
}
