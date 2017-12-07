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
            MessagingCenter.Subscribe<Usuario>(this, "MeusAgendamentosCommand", (_usuario) =>
            {
                Detail = new AgendamentoUsuarioView();
                IsPresented = false;
            });
        }
    }
}