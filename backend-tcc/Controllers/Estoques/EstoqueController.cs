using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using backend_tcc.bs.estoque.Class;
using backend_tcc.lib.repository.Class;
using backend_tcc.lib.repository.Repository;

namespace backend_tcc.ms.administrador.Controllers
{
    /// <summary>
    /// Api de Estoque dos produtos
    /// </summary>
    [RoutePrefix("api/estoque")]
    [Authorize]
    public class EstoqueController : ApiController
    {
        private UnitOfWork unitOfWork = new UnitOfWork(new EFDbContext());
        
        /// <summary>
        /// Responsável por buscar o estoque do produto
        /// </summary>
        /// <param name="id">Código produto</param>
        /// <returns></returns>
        [ResponseType(typeof(Estoque))]
        [HttpGet]
        public Estoque Get(int id)
        {

            var obj = unitOfWork.Repository<Estoque>().GetById(id);

            return obj;
        }

        /// <summary>
        /// Responsável por buscar lista de produtos e estoque
        /// </summary>        
        /// <returns></returns>
        [Route("list")]
        [ResponseType(typeof(IList<Estoque>))]
        [HttpGet]
        public IList<Estoque> List()
        {
            var itens = unitOfWork.Repository<Estoque>().Table.ToList();

            return itens;
        }

        /// <summary>
        /// Responsável por atualizar o estoque
        /// </summary>
        /// <param name="obj">Estoque</param>
        /// <returns></returns>        
        [HttpPut]
        public string Put(Estoque obj)
        {
            var rep = unitOfWork.Repository<Estoque>();
            rep.Update(obj);
            unitOfWork.Save();
            return "Estoque Atualizado com sucesso";
        }

        /// <summary>
        /// Responsável por gravar estoque
        /// </summary>
        /// <param name="obj">Estoque</param>
        /// <returns></returns>        
        [HttpPost]
        public string Post(Estoque obj)
        {
            var rep = unitOfWork.Repository<Estoque>();

            var objAux = rep.GetById(obj.ProdutoID);
            if (objAux != null)
            {
                objAux.Quantidade = obj.Quantidade;
                objAux.Bloqueado = obj.Bloqueado;                

                rep.Update(objAux);
            }
            else
            {
                rep.Insert(obj);
            }

            unitOfWork.Save();
            return "Estoque gravado com sucesso";
        }
    }
}