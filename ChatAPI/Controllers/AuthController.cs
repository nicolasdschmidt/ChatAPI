using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http.Cors;
using System.Web.Http;
using ChatAPI.Models;

namespace ChatAPI.Controllers
{
    /// <summary>
    /// Controlador de autenticação de usuários.
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AuthController : ApiController
    {
        /// <summary>
        /// Método de autenticação (POST)
        /// </summary>
        /// <param name="auth">Objeto da classe Usuário (apenas RA e senha)</param>
        /// <returns>Se o usuário deve ser autenticado.</returns>
        public bool Post([FromBody]Usuario auth)
        {
            using (UsuarioDBContext dbContext = new UsuarioDBContext())
            {
                Usuario get = dbContext.Usuario.FirstOrDefault(u => u.RA == auth.RA);

                bool ret = get.Senha == auth.Senha;

                Random rand = new Random();

                int millis = rand.Next(3000, 3500);

                if (!ret)
                    Thread.Sleep(millis);

                return ret;
            }
        }
    }
}
