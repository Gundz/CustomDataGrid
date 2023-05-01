using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace CustomDataGrid.Views;

public partial class DataGridItemView : UserControl
{
    public DataGridItemView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load( this );
    }
}
