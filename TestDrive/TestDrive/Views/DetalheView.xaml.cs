using TestDrive.Models;
using TestDrive.ViewModels;
using Xamarin.Forms;

namespace TestDrive.Views
{
    public partial class DetalheView : ContentPage
    {
        public Veiculo Veiculo { get; private set; }
        public Usuario Usuario { get; private set; }

        public DetalheView(Veiculo veiculo, Usuario usuario)
        {
            InitializeComponent();
            Veiculo = veiculo;
            Usuario = usuario;
            BindingContext = new DetalheViewModel(veiculo);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<Veiculo>(this, "Proximo", (veiculo) =>
            {
                Navigation.PushAsync(new AgendamentoView(veiculo, Usuario));
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<Veiculo>(this, "Proximo");
        }
    }
}
