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
    /// Controlador de objetos da classe Grupo.
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GruposController : ApiController
    {
        /// <summary>
        /// Pedido GET da lista de grupos.
        /// </summary>
        /// <returns>Lista de objetos da classe Grupo.</returns>
        public IEnumerable<Grupo> Get()
        {
            using (GrupoDBContext dbContext = new GrupoDBContext())
            {
                List<Grupo> get = dbContext.Grupo.ToList();

                return get;
            }
        }

        /// <summary>
        /// Pedido GET de um grupo específico.
        /// </summary>
        /// <param name="id">Id do grupo</param>
        /// <returns>Objeto da classe Grupo</returns>
        public Grupo Get(int id)
        {
            using (GrupoDBContext dbContext = new GrupoDBContext())
            {
                Grupo get = dbContext.Grupo.FirstOrDefault(g => g.Id == id);

                return get;
            }
        }

        [HttpPost]
        public HttpResponseMessage Criar(Grupo grupo)
        {
            using (GrupoDBContext dbContext = new GrupoDBContext())
            {
                try
                {
                    if (grupo == null)
                    {
                        var message = "Grupo não pode ser null";
                        HttpError err = new HttpError(message);
                        return Request.CreateResponse(HttpStatusCode.BadRequest, err);
                    }

                    if (grupo.Nome == null ||
                        grupo.Criador == null)
                    {
                        var message = "Nome ou criador ausentes";
                        HttpError err = new HttpError(message);
                        return Request.CreateResponse(HttpStatusCode.BadRequest, err);
                    }

                    /*Grupo get = dbContext.Grupo.FirstOrDefault(g => g.Id == grupo.Id);
                    if (get != null)
                    {
                        var message = "Grupo já existe";
                        HttpError err = new HttpError(message);
                        return Request.CreateResponse(HttpStatusCode.Conflict, err);
                    }*/

                    grupo.Criacao = DateTime.Now;

                    dbContext.Grupo.Add(grupo);
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
