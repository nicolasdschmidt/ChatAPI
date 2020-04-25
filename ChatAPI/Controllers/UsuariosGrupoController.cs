using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ChatAPI.Models;

namespace ChatAPI.Controllers
{
    public class UsuariosGrupoController : ApiController
    {
        // GET api/usuariosgrupo
        public IEnumerable<UsuariosGrupo> Get()
        {
            using (UsuariosGrupoDBContext dbContext = new UsuariosGrupoDBContext())
            {
                List<UsuariosGrupo> get = dbContext.UsuariosGrupo.ToList();

                return get;
            }
        }

        // GET api/usuariosgrupo
        public UsuariosGrupo Get(string search)
        {
            using (UsuariosGrupoDBContext dbContext = new UsuariosGrupoDBContext())
            {
                UsuariosGrupo get = null;

                char type = search[0];
                int id = int.Parse(search.Substring(1));

                if (search[0] == 'u')
                {
                    get = dbContext.UsuariosGrupo.FirstOrDefault(u => u.Usuario == id);
                }

                return get;
            }
        }
    }
}
