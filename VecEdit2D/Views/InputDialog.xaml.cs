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

namespace VecEdit2D.Views
{
    public partial class InputDialog : Window
    {
        public InputDialog(string windowTitle, string info = "")
        {
            InitializeComponent();
            this.Title = windowTitle;
            infoLabel.Content = info;
            outputBox.Text = "";
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            outputBox.SelectAll();
            infoLabel.Focus();
        }

    }
}
