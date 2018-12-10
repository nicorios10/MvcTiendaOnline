using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TiendaOnlineMvc.Utilities
{
    public class Strings
    {
        #region Messagges
        public const string UIMessageUnauthorized = "Al parecer ha caducado su sesión. Será redireccionado al login..";
        public const string UIMessageMethodNotAllowed = "No tiene los permisos necesarios para el recurso que intenta acceder.";
        #endregion


        #region Keys
        public const string KeyCurrentUser = "KeyCurrentUser";
        public const string KeyCarritoDeCompra = "KeyCarritoDeCompra";
        public const string KeyMensajeDeAccion = "KeyMensajeDeAccion";
        public const string KeyConnectionStringComIT = "ComITGrupal";

        #endregion

        public static string Encriptar(string texto)
        {
            System.Security.Cryptography.SHA1 sha1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();

            byte[] inputBytes = (new System.Text.UnicodeEncoding()).GetBytes(texto);
            byte[] hash = sha1.ComputeHash(inputBytes);

            return Convert.ToBase64String(hash);
        }
    }
}