using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF.Common;
using BuisnessLogic;
using WPF.Interface;
using WPF.View.DialogWindow;
using DataPostgres;

namespace WPF.ViewModel
{
    public class ClientViewModel : ViewModelBase, IDataErrorInfo
    {
        #region Поля
        private string _name;
        private string _address;
        private bool _isVIP;
        protected ObservableCollection<OrderViewModel> _order;
        protected ClientViewModel _originalValue;
        #endregion


        #region Свойства

        public int ClientId { get; set; }
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                OnPropertyChanged("Address");
            }
        }
        public bool IsVIP
        {
            get { return _isVIP; }
            set
            {
                _isVIP = value;
                OnPropertyChanged("IsVIP");
            }
        }
        public ObservableCollection<OrderViewModel> Orders
        {
            get { return GetOrder(); }
            set
            {
                _order = value;
                OnPropertyChanged("Orders");
            }
        }

        #endregion

        internal ClientViewModel() { }
        public ViewModel Container
        {
            get { return ViewModel.Instance(); }
        }


        public ObservableCollection<OrderViewModel> GetOrder()
        {
            _order = new ObservableCollection<OrderViewModel>();
            IBuisnessLogic buisnessLogic = new BuisnessLogic.BuisnessLogic();           
            foreach (var i in buisnessLogic.GetOrderByClientId(ClientId))
            {
                OrderViewModel order = new OrderViewModel(i);
                order.Clients = this;
                _order.Add(order);
            }
            return _order;
        }
        
        


        public ClientViewModel(Client c)
        {
            ClientId = c.Id;
            Name = c.Name;
            Address = c.Address;
            IsVIP = c.IsVIP;
            _originalValue = (ClientViewModel)MemberwiseClone();
        }
        


        private ICommand _showEditCommand;
        private ICommand _updateCommand;
        private ICommand _deleteCommand;
        private ICommand _cancelCommand;
        public OperationType operationType { get; set; }


        #region ICommand

        public ICommand ShowEditCommand
        {
            get
            {
                if (_showEditCommand == null)
                {
                    _showEditCommand = new CommandBase(i => ShowEditDialog(), null);
                }
                return _showEditCommand;
            }
        }

        public ICommand UpdateCommand
        {
            get
            {
                if (_updateCommand == null)
                {
                    _updateCommand = new CommandBase(i => Update(), null);
                }
                return _updateCommand;
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                {
                    _deleteCommand = new CommandBase(i => Delete(), null);
                }
                return _deleteCommand;
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                {
                    _cancelCommand = new CommandBase(i => Undo(), null);
                }
                return _cancelCommand;
            }
        }
        #endregion

        #region Опреление команд
        /// <summary>
        /// Показать Диалоговое окно
        /// </summary>
        private void ShowEditDialog()
        {
            operationType = OperationType.Update;
            IModalDialog dialog = new ClientViewDialog();
            dialog.BindViewModel(this);
            dialog.ShowDialog();
        }

        private void Update()
        {
            IBuisnessLogic buisnessLogic = new BuisnessLogic.BuisnessLogic();
            if (operationType == OperationType.Insert)
            {
                buisnessLogic.AddClient(new Client
                {
                    Id = ClientId,
                    Name = Name,
                    Address = Address,
                    IsVIP = IsVIP
                });
                Container.ClientList = Container.GetClient();
            }
            else if (operationType == OperationType.Update)
            {
                buisnessLogic.UpdateClient(new Client
                {
                    Id = ClientId,
                    Name = Name,
                    Address = Address,
                    IsVIP = IsVIP
                });
                _originalValue = (ClientViewModel)MemberwiseClone();
            }
        }

        private void Delete()
        {
            IBuisnessLogic buisnessLogic = new BuisnessLogic.BuisnessLogic();
            buisnessLogic.RemoveClient(new Client
            {
                Id = ClientId,
                Name = Name,
                Address = Address,
                IsVIP = IsVIP
            });
            Container.ClientList = Container.GetClient();
        }

        private void Undo()
        {
            if (operationType == OperationType.Update)
            {
                Name = _originalValue.Name;
                Address = _originalValue.Address;
                IsVIP = _originalValue.IsVIP;
            }
        }
        #endregion

        #region IDataErrorInfo Members
        /// <summary>
        /// Реализация простой валидации
        /// </summary>
        /// <param name="columnName">Указать по имени на элемент ввода</param>
        /// <returns>Строчка валидации</returns>
        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                if (columnName == "Name")
                {
                    if (Name == null)
                        return "Пожалуйста введите данные клиента";
                    if (Name.Trim() == string.Empty)
                        return "Требуется запись ФИО";
                }
                if (columnName == "Address")
                {
                    if (Name == null)
                        return "Пожалуйста введите адрес";
                    if (Name.Trim() == string.Empty)
                        return "Требуется указать Адрес";
                }
                return null;
            }
        }
        string IDataErrorInfo.Error
        {
            get { return string.Empty; }
        }
        #endregion
    }
}
