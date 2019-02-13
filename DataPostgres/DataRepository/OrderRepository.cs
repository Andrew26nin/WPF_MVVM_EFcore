using DataPostgres.DataRepository.Interface;

namespace DataPostgres.DataRepository
{
    public class OrderRepository : DataRepository<Order>, IOrderRepository
    {
    }
}
