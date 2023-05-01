using System;
using System.Collections.ObjectModel;
using DynamicData;
using ReactiveUI;

namespace CustomDataGrid.ViewModels;

public class DataGridItemViewModel : ViewModelBase
{
    public long Id { get; }

    private string _name;
    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged( ref _name, value );
    }

    private string _lastName;
    public string LastName
    {
        get => _lastName;
        set => this.RaiseAndSetIfChanged( ref _lastName, value );
    }

    private DateTime _dateOfBirth;
    public DateTime DateOfBirth
    {
        get => _dateOfBirth;
        set => this.RaiseAndSetIfChanged( ref _dateOfBirth, value );
    }

    public DataGridItemViewModel( long id )
    {
        Id = id;
        _name = Faker.Name.First();
        _lastName = Faker.Name.Last();
        _dateOfBirth = Faker.Identification.DateOfBirth();
    }
}

public class MainWindowViewModel : ViewModelBase
{
    private ReadOnlyObservableCollection<DataGridItemViewModel> _items;
    public ReadOnlyObservableCollection<DataGridItemViewModel> Items
    {
        get => _items;
        set => this.RaiseAndSetIfChanged( ref _items, value );
    }

    private readonly SourceCache<DataGridItemViewModel, long> _sourceCache = new( model => model.Id );

    public MainWindowViewModel()
    {
        _sourceCache
            .Connect()
            .Bind( out _items )
            .DisposeMany()
            .Subscribe();

        for ( int ii = 0; ii < 100; ii++ )
        {
            _sourceCache.AddOrUpdate( new DataGridItemViewModel( ii ) );
        }
    }
}
