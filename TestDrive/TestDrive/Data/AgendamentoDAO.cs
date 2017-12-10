using System.Collections.Generic;
using System.Linq;
using SQLite;
using TestDrive.Models;

namespace TestDrive.Data
{
    public class AgendamentoDao
    {
        private readonly SQLiteConnection _conexao;
        public AgendamentoDao(SQLiteConnection conexao)
        {
            _conexao = conexao;
            _conexao.CreateTable<Agendamento>();
        }

        private List<Agendamento> _listaAgendamentos;
        public List<Agendamento> ListaAgendamentos
        {
            get => _conexao.Table<Agendamento>().ToList();
            set => _listaAgendamentos = value;
        }

        public void Salvar(Agendamento agendamento)
        {
            _conexao.Insert(agendamento);
        }
    }
}
