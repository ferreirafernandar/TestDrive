using System;

namespace TestDrive.Models
{
    public class Login
    {
        public Login(string usuario, string senha)
        {
            if (string.IsNullOrEmpty(usuario))
                throw new ArgumentException(nameof(usuario));

            if (string.IsNullOrEmpty(senha))
                throw new ArgumentException(nameof(senha));

            Usuario = usuario;
            Senha = senha;
        }

        public string Usuario { get; private set; }
        public string Senha { get; private set; }
    }
}
