﻿namespace CentroPastoralSF.Core.Requests.Usuario
{
    public class LoginUsuarioRequest
    {
        public string Email { get; set; } = null!;
        public string Senha { get; set; } = null!;
    }
}