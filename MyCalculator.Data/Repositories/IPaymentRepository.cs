using MyCalculator.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalculator.Data.Repositories
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        public List<Payment> GetUserPaymentList(string userId);
    }
}
