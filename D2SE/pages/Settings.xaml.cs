using System;
using System.Windows;
using System.Windows.Controls;

namespace D2SoloEnabler.pages
{

    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        public Settings()
        {
            InitializeComponent();
        }

        public event EventHandler Closed;

        private void OnCloseClick(object sender, RoutedEventArgs e)
        {
            Closed?.Invoke(this, EventArgs.Empty);
        }

        private void OnSaveClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
