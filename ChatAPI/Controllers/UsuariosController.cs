using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using ChatAPI.Models;

namespace ChatAPI.Controllers
{
    /// <summary>
    /// Controlador de objetos da classe Usuario.
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UsuariosController : ApiController
    {
        /// <summary>
        /// Pedido GET da lista de usuários.
        /// </summary>
        /// <returns>Lista de objetos da classe Usuario</returns>
        [HttpGet]
        [Route("api/Usuario")]
        public HttpResponseMessage Get()
        {
            using (UsuarioDBContext dbContext = new UsuarioDBContext())
            {
                List<Usuario> get = dbContext.Usuario.ToList();
                List<UsuarioRetorno> ret = new List<UsuarioRetorno>();

                foreach (Usuario u in get)
                {
                    ret.Add(new UsuarioRetorno(u.RA, u.Nome, u.Status, u.Twitter, u.Instagram, u.LinkedIn, u.Foto));
                }

                return Request.CreateResponse(HttpStatusCode.OK, ret);
            }
        }

        /// <summary>
        /// Pedido GET de um usuário.
        /// </summary>
        /// <param name="id">RA do usuário</param>
        /// <returns>Um objeto da classe Usuario</returns>
        [HttpGet]
        [Route("api/Usuario/{id}")]
        public HttpResponseMessage Get(int id)
        {
            using (UsuarioDBContext dbContext = new UsuarioDBContext())
            {
                Usuario get = dbContext.Usuario.FirstOrDefault(u => u.RA == id);

                if (get != null)
                {
                    UsuarioRetorno ret = new UsuarioRetorno(get.RA, get.Nome, get.Status, get.Twitter, get.Instagram, get.LinkedIn, get.Foto);

                    return Request.CreateResponse(HttpStatusCode.OK, ret);
                }
                else
                {
                    var message = string.Format("User com id = {0} não encontrado", id);
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.NotFound, err);
                }
            }
        }

        /// <summary>
        /// POST de cadastro de um usuário.
        /// </summary>
        /// <param name="u">Objeto da classe Usuário</param>
        /// <returns>Se o cadastro foi bem-sucedido</returns>
        [HttpPost]
        [Route("api/Usuario")]
        public bool Post([FromBody]Usuario u)
        {
            using (UsuarioDBContext dbContext = new UsuarioDBContext())
            {
                try
                {
                    dbContext.Usuario.Add(u);
                    dbContext.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// POST de edição do cadastro de um usuário.
        /// </summary>
        /// <param name="u">Objeto da classe Usuário</param>
        /// <returns>O resultado da tentativa de edição.</returns>
        [HttpPost]
        [Route("api/Usuario/edit")]
        public HttpResponseMessage Edit([FromBody]Usuario user)
        {
            using (UsuarioDBContext dbContext = new UsuarioDBContext())
            {
                Usuario get = dbContext.Usuario.FirstOrDefault(u => u.RA == user.RA);
                
                try
                {
                    if (user.Senha == get.Senha)
                    {
                        get.Nome = user.Nome;
                        get.Twitter = user.Twitter;
                        get.Instagram = user.Instagram;
                        get.LinkedIn = user.LinkedIn;
                        if (user.Foto != "")
                            get.Foto = user.Foto;

                        dbContext.Entry(get).State = EntityState.Modified;
                        dbContext.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, true);
                    } else
                    {
                        var message = "Senha incorreta";
                        HttpError err = new HttpError(message);
                        return Request.CreateResponse(HttpStatusCode.Forbidden, err);
                    }
                }
                catch (Exception e)
                {
                    HttpError err = new HttpError(e.Message);
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, err);
                }
            }
        }

        /// <summary>
        /// Classe de retorno de Usuario. Omite informações como a senha do usuario.
        /// </summary>
        class UsuarioRetorno
        {
            public int RA;
            public string Nome;
            public int Status;
            public string Twitter;
            public string Instagram;
            public string LinkedIn;
            public string Foto;

            public UsuarioRetorno(int RA, string Nome, int Status, string Twitter, string Instagram, string LinkedIn, string Foto)
            {
                this.RA = RA;
                this.Nome = Nome;
                this.Status = Status;
                this.Twitter = Twitter;
                this.Instagram = Instagram;
                this.LinkedIn = LinkedIn;
                this.Foto = Foto;
            }
        }
    }
}
