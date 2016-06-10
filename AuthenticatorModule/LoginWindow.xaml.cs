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

namespace AuthenticatorMinta
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        public static bool Login(out string loginName, out string password){
            loginName = "";
            password = "";
            LoginWindow wnd = new LoginWindow();
            bool? result = wnd.ShowDialog();
            if (result.HasValue && result.Value)
            {
                loginName = wnd.login.Text;
                password = wnd.password.Password;
                return true;
            }
            return false;
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
