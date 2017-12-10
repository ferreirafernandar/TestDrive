using System.Collections.ObjectModel;
using System.Linq;
using TestDrive.Data;
using TestDrive.Models;
using Xamarin.Forms;

namespace TestDrive.ViewModels
{
    public class AgendamentoUsuarioViewModel : BaseViewModel
    {
        public AgendamentoUsuarioViewModel()
        {
            AtualizarLista();
        }

        private ObservableCollection<Agendamento> _lista = new ObservableCollection<Agendamento>();
        public ObservableCollection<Agendamento> ListaAgendamentos
        {
            get => _lista;
            set
            {
                _lista = value;
                OnPropertyChanged();
            }
        }

        private Agendamento _agendamentoSelecionado;

        public Agendamento AgendamentoSelecionado
        {
            get => _agendamentoSelecionado;
            set
            {
                if (value == null) return;
                _agendamentoSelecionado = value;
                MessagingCenter.Send(_agendamentoSelecionado, "AgendamentoSelecionado");
            }
        }

        public void AtualizarLista()
        {
            using (var conexao = DependencyService.Get<ISQLite>().PegarConexao())
            {
                var dao = new AgendamentoDao(conexao);
                var listaDb = dao.ListaAgendamentos;

                var query =
                    listaDb
                        .OrderBy(l => l.DataAgendamento)
                        .ThenBy(l => l.HoraAgendamento);

                ListaAgendamentos.Clear();
                foreach (var itemDb in query)
                {
                    ListaAgendamentos.Add(itemDb);
                }
            }
        }
    }
}
