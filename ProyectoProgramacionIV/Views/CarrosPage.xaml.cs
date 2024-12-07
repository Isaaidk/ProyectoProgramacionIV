using ProyectoProgramacionIV.Models;
using ProyectoProgramacionIV.Services;
using System.Collections.ObjectModel;

namespace ProyectoProgramacionIV.Views;

public partial class CarrosPage : ContentPage
{
    private FileService _fileService;
    private List<Carro> _carros;

    public CarrosPage()
    {
        InitializeComponent();
        _fileService = new FileService("carros.json");
        _carros = new List<Carro>();
        LoadCarros();
    }

    private async void LoadCarros()
    {
        _carros = await _fileService.LoadDataAsync<Carro>();
        CarrosListView.ItemsSource = _carros;
    }

    private void AddCarroButton_Clicked(object sender, EventArgs e)
    {
        var newCarro = new Carro
        {
            IdCarro = _carros.Count + 1,  
            Marca = "Marca del carro",    
            Modelo = "Modelo del carro"
        };

        _carros.Add(newCarro);
        CarrosListView.ItemsSource = null;  
        CarrosListView.ItemsSource = _carros;

        SaveCarros();
    }

    private async void SaveCarros()
    {
        await _fileService.SaveDataAsync(_carros);
    }

    private void CarrosListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            var selectedCarro = (Carro)e.SelectedItem;
            DisplayAlert("Carro Seleccionado", $"{selectedCarro.Marca} {selectedCarro.Modelo}", "OK");
        }
    }
}