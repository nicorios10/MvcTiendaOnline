using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TiendaOnlineMvc.Models;
using TiendaOnlineMvc.Utilities;

namespace TiendaOnlineMvc.Managers
{
    public class UsuarioManager : BaseManager
    {
        /// <summary>
        /// Este metodo retorna el usuario (clase Entity) que coincide con el email y password. Caso contrario retorna null.
        /// </summary>
        /// <param name="email">email del usuario</param>
        /// <param name="password">password del usuario</param>
        /// <returns>Usuario o null</returns>
        public static Customer Login(string email, string password)
        {
            Customer usuario = GetByEmail(email);
            string passwordEncriptada = Strings.Encriptar(password);
            if (usuario != null && usuario.Password == passwordEncriptada)
            {
                return usuario;
            }
            return null;
        }

        public static Customer ResetearPassword(string email) {
            /*
             pseudo-codigo del olvidoContraseña

            - recibo el email.
            - busco el usuario en la BD con ese email y lo guardo en una variable
            - SINO encuentra retorno NULL
            - SI lo encuentra
                 - generamos nueva pass (de forma aleatoria)
                 - seteamos la nueva pass al usuario .> user.Password = passEncriptada;
                 - actualizamos la contraseña del usuario en la BD. 
                 - retorna el usuario.
             */
            return null;
        }

        /// <summary>
        /// Retorna todos los clientes ordenados por el nombre.
        /// </summary>
        /// <returns></returns>
        public static List<Customer> GetClientes()
        {
            var usuarios = new List<Customer>();

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings[Strings.KeyConnectionStringComIT].ToString()))
            {
                con.Open();

                var query = new SqlCommand("SELECT * FROM Usuario WHERE TipoDeUsuario = 1", con);
                using (var dr = query.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var usuario = MapearUsuario(dr);
                        // Agregamos el usuario a la lista
                        usuarios.Add(usuario);
                    }
                }
            }

            return usuarios;
        }


        /// <summary>
        /// Retorna un usuario a partir de un E-mail.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private static Customer GetByEmail(string email)
        {
            Customer usuario = null;

            //creo una conexion con la base de datos.
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings[Strings.KeyConnectionStringComIT].ToString()))
            {
                //abro la conexion
                con.Open();

                //Preparo la consulta a realizar.
                var query = new SqlCommand("SELECT * FROM Usuario WHERE Email = @email", con);
                //Seteo los parametros
                query.Parameters.AddWithValue("@email", email);

                //creo un lector
                using (var dr = query.ExecuteReader())
                {
                    //le digo al lector que lea la 1er fila
                    dr.Read();
                    if (dr.HasRows) //Si hay fila..
                    {
                        //Mapeo la fila con la entidad.
                        usuario = MapearUsuario(dr);
                    }
                }
            }

            return usuario;
        }

        /// <summary>
        /// Retorna un usuario a partir de un id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Customer GetById(int id)
        {
            Customer usuario = null;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings[Strings.KeyConnectionStringComIT].ToString()))
            {
                con.Open();

                var query = new SqlCommand("SELECT * FROM Usuario WHERE Id = @id", con);
                query.Parameters.AddWithValue("@id", id);

                using (var dr = query.ExecuteReader())
                {
                    dr.Read();
                    if (dr.HasRows)
                    {
                        usuario = MapearUsuario(dr);
                    }
                }
            }

            return usuario;
        }


        private static Customer MapearUsuario(SqlDataReader dr)
        {
            var usuario = new Customer
            {
                Id = Convert.ToInt32(dr["Id"]),
                FirstName = dr["FirstName"].ToString(),
                LastName = dr["LastName"].ToString(),
                Email = dr["Email"].ToString(),
                Password = dr["Password"].ToString(),
                UserType = (EUserType)Convert.ToInt32(dr["UserType"])
            };
            return usuario;
        }
    }
}