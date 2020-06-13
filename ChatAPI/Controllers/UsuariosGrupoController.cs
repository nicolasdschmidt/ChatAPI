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
    /// Controlador de relação entre usuários e grupos.
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UsuariosGrupoController : ApiController
    {
        /// <summary>
        /// Pedido GET de todas as relações entre usuários e grupos.
        /// </summary>
        /// <returns>Lista de objetos da classe UsuarioGrupo</returns>
        public IEnumerable<UsuariosGrupo> Get()
        {
            using (UsuariosGrupoDBContext dbContext = new UsuariosGrupoDBContext())
            {
                List<UsuariosGrupo> get = dbContext.UsuariosGrupo.ToList();

                return get;
            }
        }

        /// <summary>
        /// Pedido GET de todos os grupos de um usuário.
        /// </summary>
        /// <param name="id">RA do usuário</param>
        /// <returns>Lista de IDs dos grupos aos quais o usuário pertence</returns>
        public IEnumerable<int> GetFromUser(int id)
        {
            using (UsuariosGrupoDBContext dbContext = new UsuariosGrupoDBContext())
            {
                List<UsuariosGrupo> get = dbContext.UsuariosGrupo.Where(g => g.Usuario == id).ToList();

                List<int> ret = new List<int>();

                foreach (UsuariosGrupo u in get)
                {
                    ret.Add((int)u.Grupo);
                }

                return ret;
            }
        }

        /// <summary>
        /// Método para adicionar usuários a um grupo
        /// </summary>
        /// <param name="u">Associação usuário e grupo</param>
        /// <returns>Status da tentativa de associação</returns>
        public HttpResponseMessage Post([FromBody] UsuariosGrupo u)
        {
            using (UsuariosGrupoDBContext dbContext = new UsuariosGrupoDBContext())
            {
                try
                {
                    UsuariosGrupo get = dbContext.UsuariosGrupo.FirstOrDefault(g => g.Usuario == u.Usuario && g.Grupo == u.Grupo);

                    if (get != null)
                        return Request.CreateResponse(HttpStatusCode.Conflict, "Assossiação já existe");

                    dbContext.UsuariosGrupo.Add(u);
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
