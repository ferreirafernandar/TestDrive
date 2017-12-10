using System;
using SQLite;

namespace TestDrive.Models
{
    public class Agendamento
    {
        public Agendamento()
        {

        }

        public Agendamento(string nome, string fone, string email, string modelo, decimal preco)

        {
            Nome = nome;
            Fone = fone;
            Email = email;
            Modelo = modelo;
            Preco = preco;
        }

        public Agendamento(string nome, string fone, string email, string modelo, decimal preco, DateTime dataAgendamento, TimeSpan horaAgendamento)
            : this(nome, fone, email, modelo, preco)
        {
            DataAgendamento = dataAgendamento;
            HoraAgendamento = horaAgendamento;
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Fone { get; set; }
        public string Email { get; set; }
        public string Modelo { get; set; }
        public decimal Preco { get; set; }
        private DateTime _dataAgendamento = DateTime.Today;
        public DateTime DataAgendamento
        {
            get => _dataAgendamento;
            set => _dataAgendamento = value;
        }
        public TimeSpan HoraAgendamento { get; set; }
        public string DataFormatada => DataAgendamento.Add(HoraAgendamento).ToString("dd/MM/yyyy HH:mm");
        public bool Confirmado { get; set; }
    }
}
