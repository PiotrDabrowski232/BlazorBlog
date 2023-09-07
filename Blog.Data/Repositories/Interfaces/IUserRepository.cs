using Blog.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public User GetById(Guid id);

        public User GetByContainedString(string containString);

        public void SoftDelete(string userEmail);
    }
}
