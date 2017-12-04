using TestDrive.Models;
using Xamarin.Forms;

namespace TestDrive.Views
{
    public partial class MasterDetailView : MasterDetailPage
    {
        public MasterDetailView(Usuario usuario)
        {
           
            InitializeComponent();
            _usuario = usuario;
            Master = new MasterView(usuario);
        }

        private readonly Usuario _usuario;
    }
}