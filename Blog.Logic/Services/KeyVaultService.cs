using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Blog.Logic.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Logic.Services
{
    public class KeyVaultService : IKeyVaultService
    {
        public async Task<string> VaultDownloader(string key)
        {
            var kvUri = $"https://applicationemailholder.vault.azure.net/";
            var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());

            var secret = client.GetSecret($"{key}").Value;
            return secret.Value;
        }
    }
}
