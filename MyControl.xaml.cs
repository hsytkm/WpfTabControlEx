using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfTabControlEx;

public partial class MyControl : UserControl
{
    public static readonly DependencyProperty MessageProperty =
        DependencyProperty.Register(nameof(Message), typeof(string), typeof(MyControl),
            new PropertyMetadata(""));

    public string Message
    {
        get => (string)GetValue(MessageProperty);
        set => SetValue(MessageProperty, value);
    }

    public MyControl()
    {
        InitializeComponent();

        Loaded += (_, _) => Debug.WriteLine($"Loaded : {Message}");
        Unloaded += (_, _) => Debug.WriteLine($"Unloaded : {Message}");

        MyListBox.ItemsSource = Enumerable.Range(0, 100).Select(i => $"Data{i}").ToArray();
        Debug.WriteLine($"ctor : {Message}");
    }
}
