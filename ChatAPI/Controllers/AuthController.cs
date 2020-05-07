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

namespace ChatAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AuthController : ApiController
    {
        public bool Post([FromBody]Usuario auth)
        {
            using (UsuarioDBContext dbContext = new UsuarioDBContext())
            {
                Usuario get = dbContext.Usuario.FirstOrDefault(u => u.RA == auth.RA);

                bool ret = get.Senha == auth.Senha;

                Random rand = new Random();

                int millis = rand.Next(3000, 3500);

                if (!ret)
                    Thread.Sleep(millis);

                return ret;
            }
        }
    }
}
