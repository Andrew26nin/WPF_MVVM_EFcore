using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Interface
{
    public interface IModalDialog
    {
        void BindViewModel<TViewModel>(TViewModel viewModel);

        void ShowDialog();

        void Close();
    }
}
