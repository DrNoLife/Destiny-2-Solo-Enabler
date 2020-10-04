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
            currentStatus.Text = isActive ? "Enabled." : "Disabled.";
            soloplayButton.Content = isActive ? "Disable soloplay" : "Enable soloplay";
        }
    }
}
