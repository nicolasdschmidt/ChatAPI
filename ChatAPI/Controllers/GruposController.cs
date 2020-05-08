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
    }
}
