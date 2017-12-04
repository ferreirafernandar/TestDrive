using TestDrive.Models;
using TestDrive.ViewModels;
using Xamarin.Forms;

namespace TestDrive.Views
{
    public partial class MasterView : TabbedPage
    {
        public MasterView(Usuario usuario)
        {
            InitializeComponent();
            BindingContext = new MasterViewModel(usuario);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            AssinarMensagens();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            CencelarMensagens();
        }
        private void AssinarMensagens()
        {
            MessagingCenter.Subscribe<Usuario>(this, "EditarPerfilCommand", (usuario) => { CurrentPage = Children[1]; });

            MessagingCenter.Subscribe<Usuario>(this, "SucessoSalvarPerfil", (usuario) => { CurrentPage = Children[0]; });
        }

        private void CencelarMensagens()
        {
            MessagingCenter.Unsubscribe<Usuario>(this, "EditarPerfilCommand");
            MessagingCenter.Unsubscribe<Usuario>(this, "SucessoSalvarPerfil");
        }
    }
}