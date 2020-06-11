using ChatAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChatAPI.Controllers
{
    public class AvaliacoesController : ApiController
    {
        /// <summary>
        /// Pedido GET de todas as avaliações.
        /// </summary>
        /// <returns>Lista de objetos da classe Avaliacao</returns>
        [HttpGet]
        [Route("api/Avaliacoes")]
        public HttpResponseMessage Get()
        {
            using (AvaliacoesDBContext dbContext = new AvaliacoesDBContext())
            {
                List<Avaliacoes> ret = dbContext.Avaliacoes.ToList();

                return Request.CreateResponse(HttpStatusCode.OK, ret);
            }
        }

        /// <summary>
        /// Pedido POST para adicionar avaliacao
        /// </summary>
        /// <param name="a">Avaliacao a ser adicionada</param>
        /// <returns>O status da tentativa de inserção</returns>
        [HttpPost]
        [Route("api/Avaliacoes")]
        public HttpResponseMessage Post([FromBody]Avaliacoes a)
        {
            // return Request.CreateResponse(HttpStatusCode.OK, a);
            using (AvaliacoesDBContext dbContext = new AvaliacoesDBContext())
            {
                try
                {
                    a.Data = DateTime.Now;

                    dbContext.Avaliacoes.Add(a);
                    dbContext.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, true);
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
