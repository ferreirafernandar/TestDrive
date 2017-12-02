using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TestDrive.Models;
using Xamarin.Forms;

namespace TestDrive
{
    public class LoginService
    {
        public async Task FazerLogin(Login login)
        {
            try
            {
                using (var cliente = new HttpClient())
                {
                    var camposFormulario = new FormUrlEncodedContent(new[]
                    {
                    new KeyValuePair<string, string>("email", login.Usuario),
                    new KeyValuePair<string, string>("senha", login.Senha)
                });

                    cliente.BaseAddress = new Uri("https://aluracar.herokuapp.com");
                    var resultado = await cliente.PostAsync("/login", camposFormulario);
                    if (resultado.IsSuccessStatusCode)
                        MessagingCenter.Send(new Usuario(), "SucessoLogin");
                    else
                        MessagingCenter.Send(new LoginException("Usuário ou senha incorretos"), "FalhaLogin");
                }
            }
            catch (Exception)
            {
                MessagingCenter.Send(new LoginException("Ocorreu um erro de comunicação com o servidor"), "FalhaLogin");
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
