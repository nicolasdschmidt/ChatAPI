using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using ChatAPI.Models;

namespace ChatAPI.Controllers
{
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

        [Route("api/usuarios/{RA}/{senha}")]
        public bool Get(int RA, string senha)
        {
            using (UsuarioDBContext dbContext = new UsuarioDBContext())
            {
                Usuario get = dbContext.Usuario.FirstOrDefault(u => u.RA == RA);

                bool ret = get.Senha == senha;

                Random rand = new Random();

                int millis = rand.Next(3000, 3500);

                if (!ret)
                    Thread.Sleep(millis);

                return ret;
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
