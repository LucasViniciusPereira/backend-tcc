using backend_tcc.bs.vendas.Class;
using backend_tcc.lib.repository.Class;
using backend_tcc.lib.repository.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace backend_tcc.ms.administrador.Controllers
{
    /// <summary>
    /// API responsável por pedidos
    /// </summary>
    [RoutePrefix("api/pedido")]
    [Authorize]
    public class PedidoController : ApiController
    {
        private UnitOfWork unitOfWork = new UnitOfWork(new EFDbContext());
        /// <summary>
        /// Responsável por criar um novo pedido
        /// </summary>
        /// <returns>Nova instância de pedido</returns>
        [Route("create")]
        [ResponseType(typeof(Pedido))]
        [HttpGet]
        public Pedido Create()
        {
            return new Pedido();
        }

        /// <summary>
        /// Responsável por buscar o pedido
        /// </summary>
        /// <param name="id">Código identificado do pedido</param>
        /// <returns></returns>
        [ResponseType(typeof(Pedido))]
        [HttpGet]
        public Pedido Get(int id)
        {

            var obj = unitOfWork.Repository<Pedido>().GetById(id);

            return obj;
        }

        /// <summary>
        /// Responsável por buscar lista de pedidos
        /// </summary>        
        /// <returns></returns>
        [Route("list")]
        [ResponseType(typeof(IList<Pedido>))]
        [HttpGet]
        public IList<Pedido> List()
        {
            var itens = unitOfWork.Repository<Pedido>().Table.ToList();

            return itens;
        }

        /// <summary>
        /// Responsável por atualizar o pedido
        /// </summary>
        /// <param name="obj">Pedido</param>
        /// <returns></returns>        
        [HttpPut]
        public string Put(Pedido obj)
        {
            var rep = unitOfWork.Repository<Pedido>();
            rep.Update(obj);
            unitOfWork.Save();
            return "Pedido Atualizado com sucesso";
        }

        /// <summary>
        /// Responsável por excluir o pedido
        /// </summary>
        /// <param name="id">Pedido</param>
        /// <returns></returns>
        [HttpDelete]
        public string Delete(int id)
        {
            var rep = unitOfWork.Repository<Pedido>();
            var user = rep.GetById(id);
            if (user != null)
            {
                rep.Delete(user);
                unitOfWork.Save();
            }
            else
                return "Pedido não encontrado";

            return "Pedido removido com sucesso";
        }

        /// <summary>
        /// Responsável por gravar pedido
        /// </summary>
        /// <param name="obj">Pedido</param>
        /// <returns></returns>        
        [HttpPost]
        public string Post(Pedido obj)
        {
            var rep = unitOfWork.Repository<Pedido>();

            var objAux = rep.GetById(obj.PedidoID);
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
            return "Pedido gravado com sucesso";
        }
    }
}