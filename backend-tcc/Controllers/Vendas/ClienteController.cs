using backend_tcc.bs.common.Class;
using backend_tcc.lib.repository.Class;
using backend_tcc.lib.repository.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace backend_tcc.ms.administrador.Controllers
{
    /// <summary>
    /// API responsável por Clientes
    /// </summary>
    [RoutePrefix("api/cliente")]
    [Authorize]
    public class ClienteController : ApiController
    {
        private UnitOfWork unitOfWork = new UnitOfWork(new EFDbContext());
        /// <summary>
        /// Responsável por criar um novo cliente
        /// </summary>
        /// <returns>Nova instância de cliente</returns>
        [Route("create")]
        [ResponseType(typeof(Cliente))]
        [HttpGet]
        public Cliente Create()
        {
            return new Cliente();
        }

        /// <summary>
        /// Responsável por buscar o cliente
        /// </summary>
        /// <param name="id">Código identificado do cliente</param>
        /// <returns></returns>
        [ResponseType(typeof(Cliente))]
        [HttpGet]
        public Cliente Get(int id)
        {

            var obj = unitOfWork.Repository<Cliente>().GetById(id);

            return obj;
        }

        /// <summary>
        /// Responsável por buscar lista de clientes
        /// </summary>        
        /// <returns></returns>
        [Route("list")]
        [ResponseType(typeof(IList<Cliente>))]
        [HttpGet]
        public IList<Cliente> List()
        {
            var itens = unitOfWork.Repository<Cliente>().Table.ToList();

            return itens;
        }

        /// <summary>
        /// Responsável por atualizar o cliente
        /// </summary>
        /// <param name="obj">Cliente</param>
        /// <returns></returns>        
        [HttpPut]
        public string Put(Cliente obj)
        {
            var rep = unitOfWork.Repository<Cliente>();
            rep.Update(obj);
            unitOfWork.Save();
            return "Cliente Atualizado com sucesso";
        }

        /// <summary>
        /// Responsável por excluir o cliente
        /// </summary>
        /// <param name="id">Cliente</param>
        /// <returns></returns>
        [HttpDelete]
        public string Delete(int id)
        {
            var rep = unitOfWork.Repository<Cliente>();
            var user = rep.GetById(id);
            if (user != null)
            {
                rep.Delete(user);
                unitOfWork.Save();
            }
            else
                return "Cliente não encontrado";

            return "Cliente removido com sucesso";
        }

        /// <summary>
        /// Responsável por gravar cliente
        /// </summary>
        /// <param name="obj">Cliente</param>
        /// <returns></returns>        
        [HttpPost]
        public string Post(Cliente obj)
        {
            var rep = unitOfWork.Repository<Cliente>();

            var objAux = rep.GetById(obj.ClienteID);
            if (objAux != null)
            {
                objAux = obj;
                rep.Update(objAux);
            }
            else
            {
                rep.Insert(obj);
            }

            unitOfWork.Save();
            return "Cliente gravado com sucesso";
        }
    }
}