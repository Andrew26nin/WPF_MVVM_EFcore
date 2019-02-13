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
        IList<Client> GetAllClients();
        //Cities GetCitiesByName(string citiesName);
        void AddClient(params Client[] client);
        void UpdateClient(params Client[] client);
        void RemoveClient(params Client[] client);
        //IList<Streets> GetStreetssByCitiesName(string citiesName);
        IList<Order> GetOrderByClientId(int ClientId);
        void AddOrder(Order order);
        void UpdateOrder(Order order);
        void RemoveOrder(Order order);
    }
}
