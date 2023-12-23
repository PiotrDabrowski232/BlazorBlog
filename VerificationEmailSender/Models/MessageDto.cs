using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace VerificationEmailSender.Models
{
    internal class MessageDto
    {
        public string Email { get; set; }
        public string VerificationCode { get; set; }

        public  string MessageBody()
        {
            return $"Hello {Email}, here you have your verification code: {VerificationCode}\n" +
                $"This code will be valid 10 minutes.";
        }
    }
}
