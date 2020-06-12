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
    /// Controlador de objetos da classe Amizade
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AmizadesController : ApiController
    {
        /// <summary>
        /// Pedido GET da lista de amizades.
        /// </summary>
        /// <returns>Lista de objetos da classe Amizade</returns>
        public IEnumerable<Amizade> Get()
        {
            using (AmizadeDBContext dbContext = new AmizadeDBContext())
            {
                List<Amizade> get = dbContext.Amizade.ToList();

                return get;
            }
        }

        /// <summary>
        /// Método para retornar a Amizade entre dois usuários
        /// </summary>
        /// <param name="raUser1">RA do primeiro Usuario</param>
        /// <param name="raUser2">RA do segundo Usuario</param>
        /// <returns>Objeto Amizade que representa a amizade entre os dois usuários, ou null se não encontrada</returns>
        [HttpGet]
        [Route("api/Amizades/{raUser1}/{raUser2}")]
        public Amizade Get(int raUser1, int raUser2)
        {
            using (AmizadeDBContext dbContext = new AmizadeDBContext())
            {
                Amizade get = dbContext.Amizade.FirstOrDefault(a => (a.User1 == raUser1 && a.User2 == raUser2) || (a.User1 == raUser2 && a.User2 == raUser1));

                return get;
            }
        }

        /// <summary>
        /// Pedido GET de todos os amigos de um usuário.
        /// </summary>
        /// <param name="id">RA do usuário</param>
        /// <returns>Lista de RAs dos amigos do usuário</returns>
        public IEnumerable<int> Get(int id)
        {
            using (AmizadeDBContext dbContext = new AmizadeDBContext())
            {
                List<Amizade> get = dbContext.Amizade.Where(a => a.User1 == id || a.User2 == id).ToList();

                List<int> ret = new List<int>();

                foreach (Amizade a in get)
                {
                    if (a.User1 != id)
                        ret.Add((int) a.User1);
                    else
                        ret.Add((int)a.User2);
                }

                return ret;
            }
        }

        /// <summary>
        /// Método para criar amizade
        /// </summary>
        /// <param name="a">Amizade</param>
        /// <returns>O status da tentativa de inserção</returns>
        public HttpResponseMessage Post([FromBody] Amizade a)
        {
            using (AmizadeDBContext dbContext = new AmizadeDBContext())
            {
                try
                {
                    if (a == null) return Request.CreateResponse(HttpStatusCode.BadRequest);
                    if (a.User1 == null || a.User2 == null) return Request.CreateResponse(HttpStatusCode.BadRequest);

                    a.Data = DateTime.Now;

                    dbContext.Amizade.Add(a);
                    dbContext.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                catch (Exception e)
                {
                    HttpError err = new HttpError(e.Message);
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, err);
                }
            }
        }
    }
}
