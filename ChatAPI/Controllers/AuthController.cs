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
using System.Data.Entity;

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
        [HttpPost]
        [Route("api/Auth")]
        public bool Autenticacao([FromBody]Usuario auth)
        {
            using (UsuarioDBContext dbContext = new UsuarioDBContext())
            {
                Usuario get = dbContext.Usuario.FirstOrDefault(u => u.RA == auth.RA);

                bool ret = get.Senha == auth.Senha;

                Random rand = new Random();

                int millis = rand.Next(1000, 1500);

                if (!ret)
                    Thread.Sleep(millis);

                return ret;
            }
        }

        /// <summary>
        /// Método para alterar a senha de um usuário (POST)
        /// </summary>
        /// <param name="RA">RA do usuario a se alterar a senha</param>
        /// <param name="antiga">Senha atual do usuário</param>
        /// <param name="nova">Senha nova, que se tornará a senha do usuário</param>
        /// <returns>Se a troca de senha foi efetuada com sucesso</returns>
        [HttpPost]
        [Route("api/Auth/trocarsenha")]
        public bool TrocarSenha([FromBody]int RA, string antiga, string nova)
        {
            using (UsuarioDBContext dbContext = new UsuarioDBContext())
            {
                Usuario get = dbContext.Usuario.FirstOrDefault(u => u.RA == RA);

                bool auth = get.Senha == antiga;

                Random rand = new Random();

                int millis = rand.Next(1000, 1500);

                if (!auth)
                {
                    Thread.Sleep(millis);
                    return false;
                }
                else
                {
                    try
                    {
                        get.Senha = nova;
                        dbContext.Entry(get).State = EntityState.Modified;
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
}
