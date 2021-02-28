using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NewBieWPF.ViewModel
{
    class MainViewModel:BaseViewModel
    {
        //xử lý trong này, là datacontext của main
        public bool isLoaded = false;
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand SuplierWindowCommand { get; set; }
        public ICommand CustomerWindowCommand { get; set; }
        public ICommand ObjectWindowCommand { get; set; }
        public ICommand UserWindowCommand { get; set; }
        public ICommand InputWindowCommand { get; set; }
        public ICommand OutputWindowCommand { get; set; }
        public MainViewModel()
        {
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                isLoaded = true;
                if (p == null)
                    return;
                p.Hide();
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();
                if (loginWindow.DataContext == null)
                    return;
                var loginVM = loginWindow.DataContext as LoginViewModel;
                if(loginVM.IsLogin)
                {
                    p.Show();
                }    
                else
                {
                    p.Close();
                }    
            });

            SuplierWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                SuplierWindow window = new SuplierWindow();
                window.ShowDialog();
            });

            CustomerWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                CustomerWindow window = new CustomerWindow();
                window.ShowDialog();
            });

            ObjectWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                ObjectWindow window = new ObjectWindow();
                window.ShowDialog();
            });

            UserWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                UserWindow window = new UserWindow();
                window.ShowDialog();
            });

            InputWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                InputWindow window = new InputWindow();
                window.ShowDialog();
            });

            OutputWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                OutputWindow window = new OutputWindow();
                window.ShowDialog();
            });
        }
    }
}
