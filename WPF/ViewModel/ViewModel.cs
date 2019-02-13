using BuisnessLogic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF.Common;
using WPF.Interface;
using WPF.View.DialogWindow;


namespace WPF.ViewModel
{
    public class ViewModel : ViewModelBase
    {
        private static ViewModel _instance;

        public static ViewModel Instance()
        {
            if (_instance == null)
                _instance = new ViewModel();
            return _instance;
        }



        private ViewModel()
        {
            ClientList = GetClient();
            //Orders = GetOrder();


        }
        //protected ObservableCollection<OrderViewModel> _order;
        //public ObservableCollection<OrderViewModel> Orders
        //{
        //    get { return GetOrder(); }
        //    set
        //    {
        //        _order = value;
        //        OnPropertyChanged("Orders");
        //    }
        //}

        private ClientViewModel _selectedClient;
        public ClientViewModel SelectedClient
        {
            get => _selectedClient;
            set
            {
                _selectedClient = value;
                OnPropertyChanged("SelectedClient");
            }
        }


        


        #region ShowAddCommand
        private ICommand _showAddCommand;

        public ICommand ShowAddCommand
        {
            get
            {
                if (_showAddCommand == null)
                {
                    _showAddCommand = new CommandBase(i => ShowAddDialog(), null);
                }
                return _showAddCommand;
            }
        }

        private void ShowAddDialog()
        {
            ClientViewModel client = new ClientViewModel();
            client.operationType = OperationType.Insert;

            IModalDialog dialog = new ClientViewDialog();
            dialog.BindViewModel(client);
            dialog.ShowDialog();
        }

        #endregion


        #region Список клиентов

        private ObservableCollection<ClientViewModel> _clientList;

        public ObservableCollection<ClientViewModel> ClientList
        {
            get => GetClient();
            set
            {
                _clientList = value;
                OnPropertyChanged("ClientList");
            }
        }




        internal ObservableCollection<ClientViewModel> GetClient()
        {
            if (_clientList == null)
                _clientList = new ObservableCollection<ClientViewModel>();
            _clientList.Clear();

            IBuisnessLogic buisnessLogic = new BuisnessLogic.BuisnessLogic();

            foreach (var i in buisnessLogic.GetAllClients())
            {
                ClientViewModel client = new ClientViewModel(i);
                _clientList.Add(client);
            }
            return _clientList;
        }



        //public ObservableCollection<OrderViewModel> GetOrder()
        //{
        //    _order = new ObservableCollection<OrderViewModel>();
        //    IBuisnessLogic buisnessLogic = new BuisnessLogic.BuisnessLogic();
        //    foreach (var i in buisnessLogic.GetOrderByClientId(SelectedClient.ClientId))
        //    //foreach (var i in buisnessLogic.GetOrderByClientId(ClientId))
        //    {
        //        OrderViewModel order = new OrderViewModel(i);
        //        order.Clients = SelectedClient;
        //        _order.Add(order);
        //    }
        //    return _order;

        //}
        #endregion

    }
}
