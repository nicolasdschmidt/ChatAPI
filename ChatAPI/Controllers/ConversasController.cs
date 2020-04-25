using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ChatAPI.Models;

namespace ChatAPI.Controllers
{
    public class ConversasController : ApiController
    {
        // GET api/conversas
        public IEnumerable<Conversa> Get()
        {
            using (BD19191Entities dbContext = new BD19191Entities())
            {
                List<Conversa> get = dbContext.Conversa.ToList();

                return get;
            }
        }

        // GET api/conversas/{id}
        public Conversa Get(int id)
        {
            using (BD19191Entities dbContext = new BD19191Entities())
            {
                Conversa get = dbContext.Conversa.FirstOrDefault(c => c.Id == id);

                return get;
            }
        }
    }
}
