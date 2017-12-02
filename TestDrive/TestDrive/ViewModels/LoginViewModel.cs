using System.Windows.Input;
using TestDrive.Models;
using Xamarin.Forms;

namespace TestDrive.ViewModels
{
    public class LoginViewModel
    {
        public LoginViewModel()
        {
            EntrarCommand = new Command(async () =>
            {
                var loginService = new LoginService();
                await loginService.FazerLogin(new Login(_usuario, _senha));
            }, () => !string.IsNullOrEmpty(_usuario) && !string.IsNullOrEmpty(_senha));
        }

       

        private string _usuario;
        public string Usuario
        {
            get => _usuario;
            set
            {
                _usuario = value;
                ((Command)EntrarCommand).ChangeCanExecute();
            }
        }

        private string _senha;
        public string Senha
        {
            get => _senha;
            set
            {
                _senha = value;
                ((Command)EntrarCommand).ChangeCanExecute();
            }
        }

        public ICommand EntrarCommand { get; private set; }
    }
}
