using NewBieWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace NewBieWPF.ViewModel
{
    class LoginViewModel:BaseViewModel
    {
        public bool IsLogin { get; set; }
        private String _UserName;
        public String UserName { get => _UserName; set { _UserName = value; OnPropertyChanged(); } }
        private String _PassWord;
        public String PassWord { get => _PassWord; set { _PassWord = value; OnPropertyChanged(); } }
        public ICommand LoginWindowCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }
        public LoginViewModel()
        {
            IsLogin = false;
            PassWord = "";
            UserName = "";
            LoginWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (p == null)
                    return;
                string PassWordEncode = Base64Encode(PassWord);
                PassWordEncode = MD5Encode(PassWordEncode);

                var accCount = DataProvider.Ins.DB.Users.Where(x => x.UserName == UserName && x.PassWord == PassWordEncode).Count();//x thay cho từng record trang bảng try vấn
                //var a = DataProvider.Ins.DB.Users.ToList();

                if (accCount > 0)
                {
                    IsLogin = true;
                    p.Close();
                }
                else
                {
                    IsLogin = false;
                    MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
                }

            });

            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                PassWord = p.Password;
            });

            CloseWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });
        }
        public static string Base64Encode(string str)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(str);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string MD5Encode(string str)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(str);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }

    }
}
