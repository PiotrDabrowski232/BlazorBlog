using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Logic.Services.Interfaces
{
    public interface IKeyVaultService
    {
        public Task<string> VaultDownloader(string key);
    }
}
