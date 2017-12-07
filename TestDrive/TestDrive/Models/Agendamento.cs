using System;
using SQLite;

namespace TestDrive.Models
{
    public class Agendamento
    {
        public Agendamento(string nome, string fone, string email, string modelo, decimal preco)
        {
            Nome = nome;
            Fone = fone;
            Email = email;
            Modelo = modelo;
            Preco = preco;
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Fone { get; set; }
        public string Email { get; set; }
        public string Modelo { get; set; }
        public decimal Preco { get; set; }
        public DateTime DataAgendamento { get; set; } = DateTime.Today;
        public TimeSpan HoraAgendamento { get; set; }
    }
}
