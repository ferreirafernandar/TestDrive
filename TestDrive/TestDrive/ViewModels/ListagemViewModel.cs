using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using TestDrive.Models;
using Xamarin.Forms;

namespace TestDrive.ViewModels
{
    public class ListagemViewModel : BaseViewModel
    {
        const string URL_GET_VEICULOS = "http://aluracar.herokuapp.com/";

        public ListagemViewModel()
        {
            Veiculos = new ObservableCollection<Veiculo>();
        }

        public ObservableCollection<Veiculo> Veiculos { get; set; }
        
        Veiculo _veiculoSelecionado;
        public Veiculo VeiculoSelecionado
        {
            get => _veiculoSelecionado;
            set
            {
                _veiculoSelecionado = value;
                if (value != null)
                    MessagingCenter.Send(_veiculoSelecionado, "VeiculoSelecionado");
            }
        }

        private bool _aguarde;
        public bool Aguarde
        {
            get => _aguarde;
            set
            {
                _aguarde = value;
                OnPropertyChanged();
            }
        }

        public async Task GetVeiculos()
        {
            Aguarde = true;
            try
            {
                var cliente = new HttpClient();

                var resultado = await cliente.GetStringAsync(URL_GET_VEICULOS);

                var veiculosJson = JsonConvert.DeserializeObject<VeiculoJson[]>(resultado);

                foreach (var veiculoJson in veiculosJson)
                {
                    Veiculos.Add(new Veiculo
                    {
                        Nome = veiculoJson.Nome,
                        Preco = veiculoJson.Preco
                    });
                }
            }
            catch (Exception exc)
            {
                MessagingCenter.Send(exc, "FalhaListagem");
            }

            Aguarde = false;
        }
    }

    public class VeiculoJson
    {
        public string Nome { get; set; }
        public int Preco { get; set; }
    }
}
