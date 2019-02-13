using BuisnessLogic;
using DataPostgres;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF.Common;

namespace WPF.ViewModel
{
    public class OrderViewModel : ViewModelBase, IDataErrorInfo
    {
        protected OrderViewModel _originalValue;
        private int _number;
        private string _description;

        //private ICommand _updateCommand;
        //private ICommand _deleteCommand;
        //private ICommand _cancelCommand;

        //private OrderViewModel _originalValue;

        public ClientViewModel Clients { get; set; }

        #region Свойства
        public int Number
        {
            get { return _number; }
            set
            {
                _number = value;
                OnPropertyChanged("Number");
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        #endregion




        internal OrderViewModel(Order order)
        {
            Number = order.Number;
            Description = order.Description;
            _originalValue = (OrderViewModel)MemberwiseClone();
        }

        internal OrderViewModel() { }




        private ICommand _updateCommand;
        private ICommand _deleteCommand;
        private ICommand _cancelCommand;



        #region ICommand
        public OperationType operationType { get; set; }

        public ICommand UpdateCommand
        {
            get
            {
                if (_updateCommand == null)
                {
                    _updateCommand = new CommandBase(i => this.Update(), null);
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
                    _deleteCommand = new CommandBase(i => this.Delete(), null);
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
                    _cancelCommand = new CommandBase(i => this.Undo(), null);
                }
                return _cancelCommand;
            }
        }

        #endregion

        #region Команды

        private void Update()
        {
            IBuisnessLogic buisnessLogic = new BuisnessLogic.BuisnessLogic();

            if (operationType == OperationType.Insert)
            {
                buisnessLogic.AddOrder(new Order
                {
                    Description = Description,
                    ClientId = Clients.ClientId
                });
                Clients.Orders = Clients.GetOrder();
            }
            else if (operationType == OperationType.Update)
            {
                buisnessLogic.UpdateOrder(new Order
                {
                    Number = Number,
                    Description = Description,
                    ClientId = Clients.ClientId
                });
                _originalValue = (OrderViewModel)MemberwiseClone();
            }
        }


        private void Delete()
        {
            IBuisnessLogic buisnessLogic = new BuisnessLogic.BuisnessLogic();
            buisnessLogic.RemoveOrder(new Order
            {
                Number = Number,
                Description = Description,
                ClientId = Clients.ClientId
            });
            Clients.Orders = Clients.GetOrder();
        }

        private void Undo()
        {
            if (operationType == OperationType.Update)
            {
                Description = _originalValue.Description;
            }
        }

        #endregion


        #region IDataErrorInfo Members

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                if (columnName == "Description")
                {
                    if (Description == null)
                        return "Пожалуйста введите описание заказа";
                    if (Description.Trim() == string.Empty)
                        return "Необходимо описание заказа";
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
