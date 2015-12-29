namespace Michmela44.StyleRepair.Views
{
    using EnvDTE;
    using Window = System.Windows.Window;
    using System.Windows;

    /// <summary>
    /// Interaction logic for UpdateSettings.xaml
    /// </summary>
    public partial class UpdateSettings : Window
    {
        public UpdateSettings()
        {
            InitializeComponent();
            this.companyName.Text = StyleRepair.Properties.StyleRepair.Default.CompanyName;
            this.copyrightMessage.Text = StyleRepair.Properties.StyleRepair.Default.CopyrightMessage;
            this.RegionsCheck.IsChecked = StyleRepair.Properties.StyleRepair.Default.NArrangeUseRegions;
        }

        private void saveSettings_Click(object sender, RoutedEventArgs e)
        {
            StyleRepair.Properties.StyleRepair.Default.CompanyName = this.companyName.Text;
            StyleRepair.Properties.StyleRepair.Default.CopyrightMessage = this.copyrightMessage.Text;
            StyleRepair.Properties.StyleRepair.Default.NArrangeUseRegions = this.RegionsCheck.IsChecked == true;
            StyleRepair.Properties.StyleRepair.Default.Save();
            Close();
        }
    }
}