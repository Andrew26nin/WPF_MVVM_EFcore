using BuisnessLogic;
using Data.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF.Common;
using WPF.Interface;
using WPF.View.DialogWindow;

namespace WPF.ViewModel.ClientVM
{
    public class ClientCommand : ClientViewModel, IDataErrorInfo
    {
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
