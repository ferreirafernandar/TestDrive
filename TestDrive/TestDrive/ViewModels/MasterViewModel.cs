using System.IO;
using System.Windows.Input;
using TestDrive.Media;
using TestDrive.Models;
using Xamarin.Forms;

namespace TestDrive.ViewModels
{
    public class MasterViewModel : BaseViewModel
    {
        public MasterViewModel(Usuario usuario)
        {
            _usuario = usuario;
            DefinirComandos(usuario);
        }

        public string Nome
        {
            get => _usuario.nome;
            set => _usuario.nome = value;
        }

        public string DataNascimento
        {
            get => _usuario.dataNascimento;
            set => _usuario.dataNascimento = value;
        }

        public string Telefone
        {
            get => _usuario.telefone;
            set => _usuario.telefone = value;
        }

        public string Email
        {
            get => _usuario.email;
            set => _usuario.email = value;
        }

        private bool _editando = false;

        public bool Editando
        {
            get => _editando;
            private set
            {
                _editando = value;
                OnPropertyChanged();
            }
        }

        private ImageSource _fotoPerfil = "perfil.png";

        public ImageSource FotoPerfil
        {
            get => _fotoPerfil;
            private set
            {
                _fotoPerfil = value;
                OnPropertyChanged();
            }
        }

        private readonly Usuario _usuario;

        public ICommand EditarPerfilCommand { get; private set; }
        public ICommand SalvarCommand { get; private set; }
        public ICommand EditarCommand { get; private set; }
        public ICommand TirarFotoCommand { get; private set; }

        private void DefinirComandos(Usuario usuario)
        {
            EditarPerfilCommand = new Command(() =>
            {
                MessagingCenter.Send(usuario, "EditarPerfilCommand");
            });

            SalvarCommand = new Command(() =>
            {
                Editando = false;
                MessagingCenter.Send(usuario, "SucessoSalvarPerfil");
            });

            EditarCommand = new Command(() =>
            {
               Editando = true;
            });

            TirarFotoCommand = new Command(() =>
            {
                DependencyService.Get<ICamera>().TirarFoto();
            });

            MessagingCenter.Subscribe<byte[]>(this, "FotoTirada",
            (bytes) =>
            {
                FotoPerfil = ImageSource.FromStream(
                    () => new MemoryStream(bytes));
            });
        }
    }
}
