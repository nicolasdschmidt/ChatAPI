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
    public class AmizadesController : ApiController
    {
        // GET api/amizades
        public IEnumerable<Amizade> Get()
        {
            using (AmizadeDBContext dbContext = new AmizadeDBContext())
            {
                List<Amizade> get = dbContext.Amizade.ToList();

                return get;
            }
        }

        // GET api/values/5
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
    }
}
