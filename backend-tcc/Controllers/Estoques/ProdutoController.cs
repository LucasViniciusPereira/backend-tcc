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
    /// Api responsável pelos produtos
    /// </summary>
    [RoutePrefix("api/produto")]
    [Authorize]
    public class ProdutoController : ApiController
    {
        private UnitOfWork unitOfWork = new UnitOfWork(new EFDbContext());
        /// <summary>
        /// Responsável por criar um novo produto
        /// </summary>
        /// <returns>Nova instância de produto</returns>
        [Route("create")]
        [ResponseType(typeof(Produto))]
        [HttpGet]
        public Produto Create()
        {
            return new Produto();
        }

        /// <summary>
        /// Responsável por buscar o produto
        /// </summary>
        /// <param name="id">Código identificado do produto</param>
        /// <returns></returns>
        [ResponseType(typeof(Produto))]
        [HttpGet]
        public Produto Get(int id)
        {

            var obj = unitOfWork.Repository<Produto>().GetById(id);

            return obj;
        }

        /// <summary>
        /// Responsável por buscar lista de produtos
        /// </summary>        
        /// <returns></returns>
        [Route("produtos")]
        [ResponseType(typeof(IList<Produto>))]
        [HttpGet]
        public IList<Produto> Produtos()
        {
            var itens = unitOfWork.Repository<Produto>().Table.ToList();

            return itens;
        }

        /// <summary>
        /// Responsável por atualizar o produto
        /// </summary>
        /// <param name="obj">Produto</param>
        /// <returns></returns>        
        [HttpPut]
        public string Put(Produto obj)
        {
            var rep = unitOfWork.Repository<Produto>();
            rep.Update(obj);
            unitOfWork.Save();
            return "Produto Atualizado com sucesso";
        }

        /// <summary>
        /// Responsável por excluir o produto
        /// </summary>
        /// <param name="id">Produto</param>
        /// <returns></returns>
        [HttpDelete]
        public string Delete(int id)
        {
            var rep = unitOfWork.Repository<Produto>();
            var user = rep.GetById(id);
            if (user != null)
            {
                rep.Delete(user);
                unitOfWork.Save();
            }
            else
                return "Produto não encontrado";

            return "Produto removido com sucesso";
        }

        /// <summary>
        /// Responsável por gravar produto
        /// </summary>
        /// <param name="obj">Produto</param>
        /// <returns></returns>        
        [HttpPost]
        public string Post(Produto obj)
        {
            var rep = unitOfWork.Repository<Produto>();

            var objAux = rep.GetById(obj.ProdutoID);
            if (objAux != null)
            {
                objAux.FabricanteID = obj.FabricanteID;
                objAux.Nome = obj.Nome;
                objAux.FornecedorID = obj.FornecedorID;
                objAux.Preco = obj.Preco;
                objAux.UN = obj.UN;
                rep.Update(objAux);
            }
            else
            {
                rep.Insert(obj);
            }

            unitOfWork.Save();
            return "Produto gravado com sucesso";
        }
    }
}