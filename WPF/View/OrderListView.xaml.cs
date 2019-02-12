using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF.View.DialogWindow;

namespace WPF.View
{
    /// <summary>
    /// Логика взаимодействия для OrderListView.xaml
    /// </summary>
    public partial class OrderListView : UserControl
    {
        public OrderListView()
        {
            InitializeComponent();
        }

        private void btnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            OrderDialog view = new OrderDialog();
            OrderCommand order = new OrderCommand();

            order.Clients = (ClientCommand)DataContext;
            order.operationType = OperationType.Insert;
            view.DataContext = order;
            view.ShowDialog();
        }

        private void btnEditOrder_Click(object sender, RoutedEventArgs e)
        {
            OrderDialog view = new OrderDialog();
            OrderCommand order = (OrderCommand)((Button)sender).DataContext;
            order.operationType = OperationType.Update;
            view.DataContext = order;
            view.ShowDialog();
        }
    }
}
