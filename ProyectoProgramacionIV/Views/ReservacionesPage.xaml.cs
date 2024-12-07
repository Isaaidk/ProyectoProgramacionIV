using ProyectoProgramacionIV.Models;
using ProyectoProgramacionIV.Services;
using System.Collections.ObjectModel;

namespace ProyectoProgramacionIV.Views;

public partial class ReservacionesPage : ContentPage
{
    private FileService _fileService;
    private List<Reservacion> _reservaciones;
    private List<Cliente> _clientes;
    private List<Carro> _carros;

    public ReservacionesPage()
    {
        InitializeComponent();
        _fileService = new FileService("reservaciones.json");
        _reservaciones = new List<Reservacion>();
        _clientes = new List<Cliente>();
        _carros = new List<Carro>();

        LoadClientes();
        LoadCarros();
        LoadReservaciones();
    }

    // Cargar clientes
    private async void LoadClientes()
    {
        var clienteService = new FileService("clientes.json");
        _clientes = await clienteService.LoadDataAsync<Cliente>();
        ClientePicker.ItemsSource = _clientes;
        ClientePicker.ItemDisplayBinding = new Binding("Nombre");
    }

    // Cargar carros
    private async void LoadCarros()
    {
        var carroService = new FileService("carros.json");
        _carros = await carroService.LoadDataAsync<Carro>();
        CarroPicker.ItemsSource = _carros;
        CarroPicker.ItemDisplayBinding = new Binding("Marca");
    }

    // Cargar reservaciones
    private async void LoadReservaciones()
    {
        _reservaciones = await _fileService.LoadDataAsync<Reservacion>();
        ReservacionesListView.ItemsSource = _reservaciones;
    }

    // Guardar reservaciones
    private async void SaveReservaciones()
    {
        await _fileService.SaveDataAsync(_reservaciones);
    }

    // Agregar nueva reservación
    private void AddReservacionButton_Clicked(object sender, EventArgs e)
    {
        // Verificar que los campos estén completos
        if (ClientePicker.SelectedItem == null || CarroPicker.SelectedItem == null)
        {
            DisplayAlert("Error", "Debe seleccionar un cliente y un carro", "OK");
            return;
        }

        var newReservacion = new Reservacion
        {
            IdReservacion = _reservaciones.Count + 1,
            IdCliente = ((Cliente)ClientePicker.SelectedItem).IdCliente,  
            IdCarro = ((Carro)CarroPicker.SelectedItem).IdCarro, 
            FechaReserva = FechaReservaPicker.Date
        };

        _reservaciones.Add(newReservacion);
        ReservacionesListView.ItemsSource = null;
        ReservacionesListView.ItemsSource = _reservaciones;

        SaveReservaciones();
    }
}