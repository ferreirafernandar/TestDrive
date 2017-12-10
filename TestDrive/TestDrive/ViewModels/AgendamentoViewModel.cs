using System;
using System.Windows.Input;
using TestDrive.Models;
using TestDrive.Service;
using Xamarin.Forms;

namespace TestDrive.ViewModels
{
    public class AgendamentoViewModel : BaseViewModel
    {
        

        public AgendamentoViewModel(Veiculo veiculo, Usuario usuario)
        {
            Agendamento = new Agendamento(usuario.nome, usuario.telefone,
                usuario.email, veiculo.Nome, veiculo.Preco);

            AgendarCommand = new Command(() =>
            {
                MessagingCenter.Send(Agendamento
                    , "Agendamento");
            }, () => !string.IsNullOrEmpty(Nome)
                     && !string.IsNullOrEmpty(Fone)
                     && !string.IsNullOrEmpty(Email));
        }

        public Agendamento Agendamento { get; set; }

        public string Modelo
        {
            get => Agendamento.Modelo;
            set => Agendamento.Modelo = value;
        }

        public decimal Preco
        {
            get => Agendamento.Preco;
            set => Agendamento.Preco = value;
        }

        public string Nome
        {
            get => Agendamento.Nome;

            set
            {
                Agendamento.Nome = value;
                OnPropertyChanged();
                ((Command)AgendarCommand).ChangeCanExecute();
            }
        }

        public string Fone
        {
            get => Agendamento.Fone;

            set
            {
                Agendamento.Fone = value;
                OnPropertyChanged();
                ((Command)AgendarCommand).ChangeCanExecute();
            }

        }

        public string Email
        {
            get => Agendamento.Email;

            set
            {
                Agendamento.Email = value;
                OnPropertyChanged();
                ((Command)AgendarCommand).ChangeCanExecute();
            }
        }

        public DateTime DataAgendamento
        {
            get => Agendamento.DataAgendamento;
            set => Agendamento.DataAgendamento = value;
        }

        public TimeSpan HoraAgendamento
        {
            get => Agendamento.HoraAgendamento;
            set => Agendamento.HoraAgendamento = value;
        }

        public ICommand AgendarCommand { get; set; }

        public async void SalvarAgendamento()
        {
            var agendamentoService = new AgendamentoService();
            await agendamentoService.EnviarAgendamento(Agendamento);
        }
    }
}

