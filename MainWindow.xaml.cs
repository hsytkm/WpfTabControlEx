using System.Windows;

namespace WpfTabControlEx;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        TabControlNameTextBlock.Text = @$"TabControl type is ""{MyTabControl.GetType().Name}"".";
    }
}
