using Data.DataRepository.Interface;
using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataRepository
{
    public class OrderRepository : DataRepository<Order>, IOrderRepository
    {
    }
}
