using backend_tcc.ms.administrador.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;

namespace backend_tcc.ms.administrador.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {

        /// <summary>
        /// Responsável por criar um novo usuário
        /// </summary>
        /// <returns>Nova instância de usuário</returns>
        [Route("Create")]
        [ResponseType(typeof(UserModel))]
        [HttpPost]
        public async Task<JsonResult<UserModel>> Create()
        {                        
            return Json(new UserModel());
        }

        /// <summary>
        /// Responsável por buscar o usuário
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Route("Get")]
        [ResponseType(typeof(UserModel))]
        [HttpGet]
        public UserModel Get(string user)
        {
            return new UserModel
            {
                UserName = user,
                Email = $"{user}@email.com",
                Name = $"Name of {user}"
            };
        }

        /// <summary>
        /// Responsável por atualizar o usuário
        /// </summary>
        /// <param name="user">Usuário</param>
        /// <returns></returns>
        [Route("Put")]
        [ResponseType(typeof(UserModel))]
        public UserModel Put(UserModel user)
        {
            throw new NotImplementedException();
        }
                        
        /// <summary>
        /// Responsável por excluir o usuário
        /// </summary>
        /// <param name="user">Usuário</param>
        /// <returns></returns>
        public string Delete(string user)
        {
            return "User deleted";
        }

        /// <summary>
        /// Responsável por gravar usuário
        /// </summary>
        /// <param name="user">Usuário</param>
        /// <returns></returns>
        [Route("Post")]
        [ResponseType(typeof(UserModel))]
        [HttpPost]
        public async Task<JsonResult<UserModel>> Create(UserModel user)
        {
            return Json(user);
        }
    }
}
