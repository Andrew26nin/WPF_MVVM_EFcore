using DataPostgres;
using DataPostgres.DataRepository;
using DataPostgres.DataRepository.Interface;
using System.Collections.Generic;
namespace BuisnessLogic
{
    public class BuisnessLogic : IBuisnessLogic
    {
        private IClientRepository _clientRepository;
        private IOrderRepository _orderRepository;
        #region Конструктор
        /// <summary>
        /// Конструкторы класса бизнес-логики
        /// </summary>
        public BuisnessLogic()
        {
            _clientRepository = new ClientRepository();
            _orderRepository = new OrderRepository();
        }
        public BuisnessLogic(IClientRepository clientRepository, IOrderRepository orderRepository)
        {
            _clientRepository = clientRepository;
            _orderRepository = orderRepository;
        }
        #endregion
        #region CRUD Client
        public IList<Client> GetAllClients()
        {
            return _clientRepository.GetAll();
        }
        /// <summary>
        /// Валидация клиента
        /// </summary>
        /// <param name="clients">массив клиентов</param>
        /// <returns>Возвращает флаг проверки</returns>
        private bool ValidationClient(Client[] clients)
        {
            foreach (var client in clients)
            {
                if (client.Name == null || client.Address == null)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Логика добавления клиента
        /// </summary>
        /// <param name="clients"></param>
        public void AddClient(params Client[] clients)
        {
            if (ValidationClient(clients) == false)
            {
                return;
            }
            _clientRepository.InsertMany(clients);
        }

        /// <summary>
        /// Логика обновления записи клиента
        /// </summary>
        /// <param name="clients"></param>
        public void UpdateClient(params Client[] clients)
        {
            if (ValidationClient(clients) == false)
            {
                return;
            }
            _clientRepository.Update(clients);
        }

        /// <summary>
        /// Логика удаления клиента
        /// </summary>
        /// <param name="clients"></param>
        public void RemoveClient(params Client[] clients)
        {
            _clientRepository.Delete(clients);
        }

        #endregion

        #region CRUD Order
        /// <summary>
        /// Получение списка заказов по номеру клиента
        /// </summary>
        /// <param name="ClientId"></param>
        /// <returns></returns>
        public IList<Order> GetOrderByClientId(int ClientId)
        {
            return _orderRepository.GetList(x => x.ClientId.Equals(ClientId));
        }
        /// <summary>
        /// Валидация заказа
        /// </summary>
        /// <param name="order">Экземпляр заказа</param>
        /// <returns>Возвращает флаг проверки</returns>
        private bool ValidationOrder(Order order)
        {
            if (order.Description == null)
                return false;
            return true;
        }
        /// <summary>
        /// Логика добавления заказа
        /// </summary>
        /// <param name="order"></param>
        public void AddOrder(Order order)
        {
            if (ValidationOrder(order) == false)
                return;
            _orderRepository.InsertMany(order);
        }

        /// <summary>
        /// Логика обновления записи заказа
        /// </summary>
        /// <param name="order"></param>
        public void UpdateOrder(Order order)
        {
            if (ValidationOrder(order) == false)
                return;
            _orderRepository.Update(order);
        }

        /// <summary>
        /// Логика удаления заказа
        /// </summary>
        /// <param name="order"></param>
        public void RemoveOrder(Order order)
        {
            _orderRepository.Delete(order);
        }
        #endregion
    }
}
