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
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UsuariosController : ApiController
    {
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

        public Usuario Get(int id)
        {
            using (UsuarioDBContext dbContext = new UsuarioDBContext())
            {
                Usuario get = dbContext.Usuario.FirstOrDefault(u => u.RA == id);

                get.Senha = "";

                return get;
            }
        }

        public void Post([FromBody]Usuario u)
        {
            using (UsuarioDBContext dbContext = new UsuarioDBContext())
            {
                dbContext.Usuario.Add(u);
                dbContext.SaveChanges();
            }
        }
    }
}
