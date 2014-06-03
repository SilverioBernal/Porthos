using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orkidea.Porthos.Entities;

namespace Orkidea.Porthos.Utilities
{
    public static class StringHelper
    {
        public static string NombreUsuario(People usuario)
        {
            string res = "";

            res = usuario.nombre1 + " " + (string.IsNullOrEmpty(usuario.nombre2) ? "" : usuario.nombre2 + " ") +
                    usuario.apellido1 + (string.IsNullOrEmpty(usuario.apellido2) ? "" : " " + usuario.apellido2);

            return res;
        }
    }
}
