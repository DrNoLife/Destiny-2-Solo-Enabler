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

namespace D2SoloEnabler
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About
    {
        public About()
        {
            InitializeComponent();
        }

        public event EventHandler Closed;

        private void OnCloseButtonClicked(object sender, RoutedEventArgs e)
        {
            Closed?.Invoke(this, EventArgs.Empty);
        }

        private void OnButtonRepoClicked(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/DrNoLife/Destiny-2-Solo-Enabler");
        }
    }
}