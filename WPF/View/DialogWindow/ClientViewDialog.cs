using System;
using WPF.Interface;

namespace WPF.View.DialogWindow
{
    public class ClientViewDialog : IModalDialog
    {
        private ClientDialog view;

        public void BindViewModel<TViewModel>(TViewModel viewModel)
        {
            GetDialog().DataContext = viewModel;
        }

        public void Close()
        {
            GetDialog().Close();
        }

        public void ShowDialog()
        {
            GetDialog().ShowDialog();
        }


        private ClientDialog GetDialog()
        {
            if (view == null)
            {
                view = new ClientDialog();
                view.Closed += View_Closed;
            }
            return view;
        }

        void View_Closed(object sender, EventArgs e)
        {
            view = null;
        }
    }
}
