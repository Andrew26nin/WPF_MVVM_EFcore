using DataPostgres.DataRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataPostgres.DataRepository
{
    public class OrderRepository : DataRepository<Order>, IOrderRepository
    {
    }
}
