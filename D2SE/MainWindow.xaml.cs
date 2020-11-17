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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Check if rule is active.
        static string fwRuleName = "Destiny 2 - Solo-Enabler";
        static string portRangeToBlock = "27000-27200,3097";
        bool isActive = Soloplay.DoesFWRuleExist(fwRuleName);

        BrushConverter bc = new BrushConverter();


        public MainWindow()
        {
            InitializeComponent();

            // Change values of whatev stuff, dependent on if rule is active or no.
            statusHandling(isActive);
        }

        // On click of big boyo!
        private void soloplayButton_Click(object sender, RoutedEventArgs e)
        {
            // If the rule is indeed active. Remove FW rule. Check if still is active. Then do UI changed based on that.
            if (isActive)
            {
                Soloplay.RemoveFirewallRule(fwRuleName);
                isActive = Soloplay.DoesFWRuleExist(fwRuleName);
                statusHandling(isActive);
            }

            // If the rule is not active. We then add the FW rules. Check if they now exist. Then do UI changed based on that.
            else
            {
                // Name. Portrange. Outbound yes/no. UDP yes/no.
                Soloplay.CreateFWRule(ruleName: fwRuleName, portValue: portRangeToBlock, isOut: true, isUDP: true);
                Soloplay.CreateFWRule(ruleName: fwRuleName, portValue: portRangeToBlock, isOut: true, isUDP: false);
                Soloplay.CreateFWRule(ruleName: fwRuleName, portValue: portRangeToBlock, isOut: false, isUDP: true);
                Soloplay.CreateFWRule(ruleName: fwRuleName, portValue: portRangeToBlock, isOut: false, isUDP: false);

                isActive = true;
                statusHandling(isActive);
            }
        }

        private void statusHandling(bool isActive)
        {
            //currentStatus.Text = isActive ? "Enabled." : "Disabled.";
            buttonStatus.Content = isActive ? "ON" : "OFF";

            // Change styling dependent of state
            if (isActive)
            {
                SoloplayButton.BorderBrush = (Brush)bc.ConvertFrom("#f4d210");
                SoloplayButton.Background = (Brush)bc.ConvertFrom("#927826");
                buttonStatus.Foreground = (Brush)bc.ConvertFrom("#ffffff");
            } else
            {
                SoloplayButton.BorderBrush = (Brush)bc.ConvertFrom("#ffffff");
                SoloplayButton.Background = (Brush)bc.ConvertFrom("#2a2e32");
                buttonStatus.Foreground = (Brush)bc.ConvertFrom("#aaabad");
            }
        }

        // Well lol, could probably do this some other way. Looks stupid with a method for just this.. but whatever honestly.
        private void D2SEShutDown_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SoloplayButton_MouseEnter(object sender, MouseEventArgs e)
        {
            // Change hover color, depending on the isActive state
            if (isActive)
                SoloplayButton.Background = (Brush)bc.ConvertFrom("#8a7224");
            else
                SoloplayButton.Background = (Brush)bc.ConvertFrom("#31363b");
        }

        private void SoloplayButton_MouseLeave(object sender, MouseEventArgs e)
        {
            // Revert back to the normal colors, depending on the isActive state
            if (isActive)
                SoloplayButton.Background = (Brush)bc.ConvertFrom("#927826");
            else
                SoloplayButton.Background = (Brush)bc.ConvertFrom("#2a2e32");
        }

        // On click of about button
        private void D2SEAbout_Click(object sender, RoutedEventArgs e)
        {
            // For now it just refers the user over to a question of the FAQ
            System.Diagnostics.Process.Start("https://github.com/DrNoLife/Destiny-2-Solo-Enabler#what-guarantee-do-you-have-this-wont-ban-anyone-source");
        }
    }
}
