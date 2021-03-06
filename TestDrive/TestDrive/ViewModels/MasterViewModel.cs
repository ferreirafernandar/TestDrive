﻿using System.IO;
using System.Windows.Input;
using TestDrive.Media;
using TestDrive.Models;
using Xamarin.Forms;

namespace TestDrive.ViewModels
{
    public class MasterViewModel : BaseViewModel
    {
        private readonly Usuario _usuario;
        public MasterViewModel(Usuario usuario)
        {
            _usuario = usuario;
            DefinirComandos(usuario);
            AssinarMensagens();
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

        public ICommand EditarPerfilCommand { get; private set; }
        public ICommand SalvarCommand { get; private set; }
        public ICommand EditarCommand { get; private set; }
        public ICommand TirarFotoCommand { get; private set; }
        public ICommand MeusAgendamentosCommand { get; private set; }
        public ICommand NovoAgendamentoCommand { get; private set; }

        private void DefinirComandos(Usuario usuario)
        {
            EditarPerfilCommand = new Command(() => MessagingCenter.Send(usuario, "EditarPerfilCommand"));

            MeusAgendamentosCommand = new Command(() => MessagingCenter.Send(usuario, "MeusAgendamentosCommand"));

            NovoAgendamentoCommand = new Command(() => MessagingCenter.Send(usuario, "NovoAgendamentoCommand"));

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
        }

        private void AssinarMensagens()
        {
            MessagingCenter.Subscribe<byte[]>(this, "FotoTirada",
            bytes =>
            {
                FotoPerfil = ImageSource.FromStream(
                    () => new MemoryStream(bytes));
            });
        }
    }
}
