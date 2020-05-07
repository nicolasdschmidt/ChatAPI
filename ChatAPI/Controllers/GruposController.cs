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
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GruposController : ApiController
    {
        public IEnumerable<Grupo> Get()
        {
            using (GrupoDBContext dbContext = new GrupoDBContext())
            {
                List<Grupo> get = dbContext.Grupo.ToList();

                return get;
            }
        }

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
