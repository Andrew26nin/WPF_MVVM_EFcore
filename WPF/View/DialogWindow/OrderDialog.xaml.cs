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
using System.Windows.Shapes;

namespace WPF.View.DialogWindow
{
    /// <summary>
    /// Логика взаимодействия для OrderDialog.xaml
    /// </summary>
    public partial class OrderDialog : Window
    {
        public OrderDialog()
        {
            InitializeComponent();
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
