using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using TestDrive.Models;
using Xamarin.Forms;

namespace TestDrive.ViewModels
{
    public class AgendamentoViewModel : BaseViewModel
    {
        const string URL_POST_AGENDAMENTO = "https://aluracar.herokuapp.com/salvaragendamento";

        public AgendamentoViewModel(Veiculo veiculo)
        {
            Agendamento = new Agendamento { Veiculo = veiculo };

            AgendarCommand = new Command(() =>
            {
                MessagingCenter.Send(Agendamento
                    , "Agendamento");
            }, () => !string.IsNullOrEmpty(Nome)
                     && !string.IsNullOrEmpty(Fone)
                     && !string.IsNullOrEmpty(Email));
        }

        public Agendamento Agendamento { get; set; }

        public Veiculo Veiculo
        {
            get => Agendamento.Veiculo;
            set => Agendamento.Veiculo = value;
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
            var cliente = new HttpClient();

            var dataHoraAgendamento = new DateTime(
                DataAgendamento.Year, DataAgendamento.Month, DataAgendamento.Day,
                HoraAgendamento.Hours, HoraAgendamento.Minutes, HoraAgendamento.Seconds);

            var json = JsonConvert.SerializeObject(new
            {
                nome = Nome,
                fone = Fone,
                email = Email,
                carro = Veiculo.Nome,
                preco = Veiculo.Preco,
                dataAgendamento = dataHoraAgendamento
            });

            var conteudo = new StringContent(json, Encoding.UTF8, "application/json");

            var resposta = await cliente.PostAsync(URL_POST_AGENDAMENTO, conteudo);
            if (resposta.IsSuccessStatusCode)
                MessagingCenter.Send(this.Agendamento, "SucessoAgendamento");
            else
                MessagingCenter.Send(new ArgumentException(), "FalhaAgendamento");
        }
    }
}
