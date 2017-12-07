using System;
using TestDrive.Models;
using TestDrive.ViewModels;
using Xamarin.Forms;

namespace TestDrive.Views
{
    public partial class ListagemView : ContentPage
    {
        public ListagemViewModel ViewModel { get; set; }

        private readonly Usuario _usuario;
        public ListagemView(Usuario usuario)
        {
            InitializeComponent();
            _usuario = usuario;
            ViewModel = new ListagemViewModel();
            BindingContext = ViewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            AssinarMensagens();

            await ViewModel.GetVeiculos();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            CancelarMensagens();
        }

        private void AssinarMensagens()
        {
            MessagingCenter.Subscribe<Veiculo>(this, "VeiculoSelecionado",
                (veiculo) =>
                {
                    Navigation.PushAsync(new DetalheView(veiculo, _usuario));
                });

            MessagingCenter.Subscribe<Exception>(this, "FalhaListagem",
                (msg) =>
                {
                    DisplayAlert("Erro", "Ocorreu um erro ao obter a listagem de veículos. Por favor tente novamente mais tarde.", "Ok");
                });
        }

        private void CancelarMensagens()
        {
            MessagingCenter.Unsubscribe<Veiculo>(this, "VeiculoSelecionado");
            MessagingCenter.Unsubscribe<Exception>(this, "FalhaListagem");
        }
    }
}
