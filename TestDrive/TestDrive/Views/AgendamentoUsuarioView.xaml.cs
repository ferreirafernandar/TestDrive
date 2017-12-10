using TestDrive.Models;
using TestDrive.Service;
using TestDrive.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestDrive.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgendamentoUsuarioView : ContentPage
    {
        readonly AgendamentoUsuarioViewModel _viewModel;
        public AgendamentoUsuarioView()
        {
            InitializeComponent();
            _viewModel = new AgendamentoUsuarioViewModel();
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<Agendamento>(this, "AgendamentoSelecionado", async (agendamento) =>
            {
                if (!agendamento.Confirmado)
                {
                    var reenviar = await DisplayAlert("Reenviar", "Deseja reenviar o agendamento?", "Sim", "Não");
                    if (reenviar)
                    {
                        AgendamentoService agendamentoService = new AgendamentoService();
                        await agendamentoService.EnviarAgendamento(agendamento);
                        _viewModel.AtualizarLista();
                    }
                }
            });

            MessagingCenter.Subscribe<Agendamento>(this, "SucessoAgendamento",
                async (agendamento) =>
                {
                    await DisplayAlert("Reenviar", "Reenvio com sucesso!", "ok");
                });

            MessagingCenter.Subscribe<Agendamento>(this, "FalhaAgendamento",
                async (agendamento) =>
                {
                    await DisplayAlert("Reenviar", "Falha ao reenviar!", "ok");
                });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<Agendamento>(this, "SucessoAgendamento");
            MessagingCenter.Unsubscribe<Agendamento>(this, "FalhaAgendamento");
        }
    }
}