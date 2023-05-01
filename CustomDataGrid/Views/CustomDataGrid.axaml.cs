using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Styling;

namespace CustomDataGrid.Views;

public class CustomDataGrid : DataGrid
{
    public static readonly StyledProperty<bool> ShowGridProperty = AvaloniaProperty.Register<CustomDataGrid, bool>( nameof(ShowGrid), defaultValue: false );
    public bool ShowGrid
    {
        get => GetValue( ShowGridProperty );
        set => SetValue( ShowGridProperty, value );
    }
    
    protected override void OnApplyTemplate( TemplateAppliedEventArgs e )
    {
        base.OnApplyTemplate( e );

        if ( e.NameScope.Find<DataGridColumnHeadersPresenter>( "PART_ColumnHeadersPresenter" )?.Parent is Grid dataGridBorderGrid )
        {
            SetRightControls( dataGridBorderGrid );
        }

        SetStyle();
        SetRowDetailsVisibility();
    }

    protected override void OnPropertyChanged( AvaloniaPropertyChangedEventArgs change )
    {
        base.OnPropertyChanged( change );

        if ( change.Property == ShowGridProperty )
        {
            SetRowDetailsVisibility();
        }
    }

    // Adding the Checkbox to Toggle the GridView/CustomView
    private void SetRightControls( Panel grid )
    {
        var showGridCheckBox = new CheckBox
        {
            Content = "Grid",
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Center,
            [!ToggleButton.IsCheckedProperty] = this[!ShowGridProperty]
        };

        showGridCheckBox.SetValue( Grid.ColumnProperty, 1 );
        grid.Children.Add( showGridCheckBox );
    }

    private void SetStyle()
    {
        var style = new Style( x => x.OfType<DataGridCellsPresenter>() )
        {
            Setters =
            {
                new Setter( IsVisibleProperty, this.GetObservable( ShowGridProperty ).ToBinding()  )
            }
        };
        Styles.Add( style );
    }

    private void SetRowDetailsVisibility()
    {
        RowDetailsVisibilityMode = ShowGrid ? DataGridRowDetailsVisibilityMode.Collapsed : DataGridRowDetailsVisibilityMode.Visible;
    }
}
