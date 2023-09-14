using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// agregar la siguiente libreria
using SysInventarioFacturacion.EntidadesDeNegocio;
//************************************

namespace SysInventarioFacturacion.WebAPI.Auth
{
    public interface IJwtAuthenticationService
    {
        string Authenticate(Usuario pUsuario);
    }
}
