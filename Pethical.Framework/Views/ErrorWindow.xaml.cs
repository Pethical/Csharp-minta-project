using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pethical.Framework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ErrorWindow : Window
    {
        /// <summary>
        /// Új hibajelentő ablakot hoz létre
        /// </summary>
        /// <param name="exception">A hibát leíró kivétel</param>
        public ErrorWindow(Exception exception)
        {
            InitializeComponent();            
            DataContext = exception;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
