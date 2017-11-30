using System.Windows.Input;
using TestDrive.Models;
using Xamarin.Forms;

namespace TestDrive.ViewModels
{
    public class DetalheViewModel : BaseViewModel
    {
        public DetalheViewModel(Veiculo veiculo)
        {
            Veiculo = veiculo;
            ProximoCommand = new Command(() =>
            {
                MessagingCenter.Send(veiculo, "Proximo");
            });
        }

        public Veiculo Veiculo { get; set; }

        public string TextoFreioAbs => $"Freio ABS - R$ {Veiculo.FREIO_ABS}";

        public string TextoArCondicionado => $"Ar Condicionado - R$ {Veiculo.AR_CONDICIONADO}";

        public string TextoMp3Player => $"MP3 Player - R$ {Veiculo.MP3_PLAYER}";

        public bool TemFreioAbs
        {
            get => Veiculo.TemFreioAbs;
            set
            {
                Veiculo.TemFreioAbs = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ValorTotal));
            }
        }

        public bool TemArCondicionado
        {
            get => Veiculo.TemArCondicionado;
            set
            {
                Veiculo.TemArCondicionado = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ValorTotal));
            }
        }

        public bool TemMp3Player
        {
            get => Veiculo.TemMp3Player;
            set
            {
                Veiculo.TemMp3Player = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ValorTotal));
            }
        }

        public string ValorTotal => Veiculo.PrecoTotalFormatado;

        public ICommand ProximoCommand { get; set; }
    }
}
