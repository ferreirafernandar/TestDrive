using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestDrive.Data;
using TestDrive.Models;
using Xamarin.Forms;

namespace TestDrive.Service
{
    public class AgendamentoService
    {
        public async Task EnviarAgendamento(Agendamento agendamento)
        {
            const string URL_POST_AGENDAMENTO = "https://aluracar.herokuapp.com/salvaragendamento";
            var cliente = new HttpClient();

            var dataHoraAgendamento = new DateTime(
                agendamento.DataAgendamento.Year, agendamento.DataAgendamento.Month, agendamento.DataAgendamento.Day,
                agendamento.HoraAgendamento.Hours, agendamento.HoraAgendamento.Minutes, agendamento.HoraAgendamento.Seconds);

            var json = JsonConvert.SerializeObject(new
            {
                nome = agendamento.Nome,
                fone = agendamento.Fone,
                email = agendamento.Email,
                carro = agendamento.Modelo,
                preco = agendamento.Preco,
                dataAgendamento = dataHoraAgendamento
            });

            var conteudo = new StringContent(json, Encoding.UTF8, "application/json");

            var resposta = await cliente.PostAsync(URL_POST_AGENDAMENTO, conteudo);
            agendamento.Confirmado = resposta.IsSuccessStatusCode;
            SalvaAgendamentoDb(agendamento);

            if (agendamento.Confirmado)
                MessagingCenter.Send(agendamento, "SucessoAgendamento");
            else
                MessagingCenter.Send(new ArgumentException(), "FalhaAgendamento");
        }

        public void SalvaAgendamentoDb(Agendamento agendamento)
        {
            using (var conexao = DependencyService.Get<ISQLite>().PegarConexao())
            {
                var dao = new AgendamentoDao(conexao);
                dao.Salvar(agendamento);
            }
        }
    }
}
