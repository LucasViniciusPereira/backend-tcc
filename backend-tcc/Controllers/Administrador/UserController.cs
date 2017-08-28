using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using backend_tcc.bs.administrador.Class;
using backend_tcc.lib.repository.Class;
using backend_tcc.lib.repository.Repository;

namespace backend_tcc.ms.administrador.Controllers
{
    /// <summary>
    /// Api de Usuários
    /// </summary>
    [RoutePrefix("api/user")]
    [Authorize]
    public class UserController : ApiController
    {        
        private UnitOfWork unitOfWork = new UnitOfWork(new EFDbContext());
        /// <summary>
        /// Responsável por criar um novo usuário
        /// </summary>
        /// <returns>Nova instância de usuário</returns>
        [Route("create")]
        [ResponseType(typeof(Usuario))]
        [HttpGet]
        public Usuario Create()
        {                        
            return new Usuario();
        }

        /// <summary>
        /// Responsável por buscar o usuário
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [ResponseType(typeof(Usuario))]
        [HttpGet]
        public Usuario Get(string user)
        {
            
            var usuario = unitOfWork.Repository<Usuario>().GetById(user);

            return usuario;
        }

        /// <summary>
        /// Responsável por buscar lista de usuários
        /// </summary>        
        /// <returns></returns>
        [Route("list")]
        [ResponseType(typeof(IList<Usuario>))]
        [HttpGet]
        public IList<Usuario> List()
        {            
            var usuarios = unitOfWork.Repository<Usuario>().Table.ToList();

            return usuarios;
        }

        /// <summary>
        /// Responsável por atualizar o usuário
        /// </summary>
        /// <param name="user">Usuário</param>
        /// <returns></returns>        
        [HttpPut]
        public string Put(Usuario user)
        {            
            var rep = unitOfWork.Repository<Usuario>();
            rep.Update(user);
            unitOfWork.Save();
            return "Usuário Atualizado com sucesso";
        }

        /// <summary>
        /// Responsável por excluir o usuário
        /// </summary>
        /// <param name="userName">Usuário</param>
        /// <returns></returns>
        [HttpDelete]
        public string Delete(string userName)
        {
            var rep = unitOfWork.Repository<Usuario>();
            var user = rep.GetById(userName);
            if (user != null)
            {
                rep.Delete(user);
                unitOfWork.Save();
            }
            else
                return "Usuário não encontrado";

            return "User deleted";
        }

        /// <summary>
        /// Responsável por gravar usuário
        /// </summary>
        /// <param name="user">Usuário</param>
        /// <returns></returns>        
        [HttpPost]
        public string Post(Usuario user)
        {
            var rep = unitOfWork.Repository<Usuario>();

            var userAux = rep.GetById(user.UsuarioID);
            if(userAux != null)
            {
                userAux.Ativo = user.Ativo;
                userAux.Email = user.Email;
                userAux.Nome = user.Nome;
                userAux.Senha = user.Senha;
                rep.Update(userAux);
            }
            else
            {
                rep.Insert(user);
            }

            unitOfWork.Save();
            return "Usuário gravado com sucesso";
        }
    }
}
