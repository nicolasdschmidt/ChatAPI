using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using ChatAPI.Models;

namespace ChatAPI.Controllers
{
    /// <summary>
    /// Controlador de objetos da classe Conversa.
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ConversasController : ApiController
    {
        /// <summary>
        /// Pedido GET da lista de conversas.
        /// </summary>
        /// <returns>Lista de objetos da classe Conversa.</returns>
        public IEnumerable<Conversa> Get()
        {
            using (BD19191Entities dbContext = new BD19191Entities())
            {
                List<Conversa> get = dbContext.Conversa.ToList();

                return get;
            }
        }

        /// <summary>
        /// Pedido GET de todas as conversas de um usuário.
        /// </summary>
        /// <param name="id">RA do usuário</param>
        /// <returns>Lista de objetos da classe Conversa</returns>
        public IEnumerable<Conversa> Get(int id)
        {
            using (BD19191Entities dbContext = new BD19191Entities())
            {
                List<Conversa> get = dbContext.Conversa.Where(c => c.User1 == id || c.User2 == id).ToList();

                List<Conversa> ret = new List<Conversa>();

                foreach (Conversa c in get)
                {
                    if (c.User1 != id)
                        ret.Add(c);
                    else
                        ret.Add(c);
                }

                return ret;
            }
        }
    }
}
