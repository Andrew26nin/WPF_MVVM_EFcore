using DataPostgres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BuisnessLogic
{
    public interface IBuisnessLogic
    {
        #region Client
        IList<Client> GetAllClients();     
        void AddClient(params Client[] client);
        void UpdateClient(params Client[] client);
        void RemoveClient(params Client[] client);
        #endregion

        #region Order
        IList<Order> GetOrderByClientId(int ClientId);
        void AddOrder(Order order);
        void UpdateOrder(Order order);
        void RemoveOrder(Order order);
        #endregion
    }
}
