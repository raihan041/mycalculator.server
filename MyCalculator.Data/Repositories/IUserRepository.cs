using MyCalculator.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalculator.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        public User GetByEmail(string email);
    }
}
