using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Cors;
using ChatAPI.Models;

namespace ChatAPI.Controllers
{
    /// <summary>
    /// Controlador de objetos da classe Usuario.
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UsuariosController : ApiController
    {
        /// <summary>
        /// Pedido GET da lista de usuários.
        /// </summary>
        /// <returns>Lista de objetos da classe Usuario</returns>
        public IEnumerable<Usuario> Get()
        {
            using (UsuarioDBContext dbContext = new UsuarioDBContext())
            {
                List<Usuario> get = dbContext.Usuario.ToList();

                foreach (Usuario u in get)
                {
                    u.Senha = "";
                }

                return get;
            }
        }

        /// <summary>
        /// Pedido GET de um usuário.
        /// </summary>
        /// <param name="id">RA do usuário</param>
        /// <returns>Um objeto da classe Usuario</returns>
        public Usuario Get(int id)
        {
            using (UsuarioDBContext dbContext = new UsuarioDBContext())
            {
                Usuario get = dbContext.Usuario.FirstOrDefault(u => u.RA == id);

                get.Senha = "";

                return get;
            }
        }

        /// <summary>
        /// POST de cadastro de um usuário.
        /// </summary>
        /// <param name="u">Objeto da classe Usuário</param>
        /// <returns>Se o cadastro foi bem-sucedido</returns>
        public bool Post([FromBody]Usuario u)
        {
            using (UsuarioDBContext dbContext = new UsuarioDBContext())
            {
                try
                {
                    dbContext.Usuario.Add(u);
                    dbContext.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }
    }
}
