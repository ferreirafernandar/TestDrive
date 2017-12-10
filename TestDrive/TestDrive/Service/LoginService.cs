using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TestDrive.Models;
using Xamarin.Forms;

namespace TestDrive.Service
{
    public class LoginService
    {
        public async Task FazerLogin(Login login)
        {

            using (var cliente = new HttpClient())
            {
                var camposFormulario = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("email", login.Usuario),
                    new KeyValuePair<string, string>("senha", login.Senha)
                });

                cliente.BaseAddress = new Uri("https://aluracar.herokuapp.com");
                var resultado = new HttpResponseMessage();
                try
                {
                    resultado = await cliente.PostAsync("/login", camposFormulario);
                }
                catch (Exception)
                {
                    MessagingCenter.Send(new LoginException("Ocorreu um erro de comunicação com o servidor"), "FalhaLogin");
                }
                if (resultado.IsSuccessStatusCode)
                {
                    var conteudoResultado = await resultado.Content.ReadAsStringAsync();
                    var resultadoLogin = JsonConvert.DeserializeObject<ResultadoLogin>(conteudoResultado);
                    MessagingCenter.Send(resultadoLogin.usuario, "SucessoLogin");
                }
                else
                    MessagingCenter.Send(new LoginException("Usuário ou senha incorretos"), "FalhaLogin");
            }

        }
    }

    public class LoginException : Exception
    {
        public LoginException(string mensagem) : base(mensagem)
        {

        }
    }
}
