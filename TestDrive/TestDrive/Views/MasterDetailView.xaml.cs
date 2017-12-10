using TestDrive.Models;
using Xamarin.Forms;

namespace TestDrive.Views
{
    public partial class MasterDetailView : MasterDetailPage
    {
        private readonly Usuario _usuario;
        public MasterDetailView(Usuario usuario)
        {
           
            InitializeComponent();
            _usuario = usuario;
            Master = new MasterView(usuario);
            Detail = new NavigationPage(new ListagemView(usuario));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            AssinarMensagens();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            CancelarMensagens();
        }

        private void AssinarMensagens()
        {
            MessagingCenter.Subscribe<Usuario>(this, "MeusAgendamentosCommand", (_usuario) =>
            {
                Detail = new NavigationPage(new AgendamentoUsuarioView());
                IsPresented = false;
            });

            MessagingCenter.Subscribe<Usuario>(this, "NovoAgendamentoCommand", (_usuario) =>
            {
                Detail = new NavigationPage(new ListagemView(_usuario));
                IsPresented = false;
            });
        }

        private void CancelarMensagens()
        {
            MessagingCenter.Unsubscribe<Usuario>(this, "MeusAgendamentosCommand");
            MessagingCenter.Unsubscribe<Usuario>(this, "NovoAgendamentoCommand");
        }
    }
}