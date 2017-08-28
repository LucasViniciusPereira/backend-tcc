using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using backend_tcc.bs.common.Class;
using backend_tcc.lib.repository.Class;
using backend_tcc.lib.repository.Repository;

namespace backend_tcc.ms.administrador.Controllers
{
    /// <summary>
    /// Api de Fornecedores
    /// </summary>
    [RoutePrefix("api/fornecedor")]
    [Authorize]
    public class FornecedorController : ApiController
    {
        private UnitOfWork unitOfWork = new UnitOfWork(new EFDbContext());
        /// <summary>
        /// Responsável por criar um novo fornecedor
        /// </summary>
        /// <returns>Nova instância de fornecedor</returns>
        [Route("create")]
        [ResponseType(typeof(Fornecedor))]
        [HttpGet]
        public Fornecedor Create()
        {
            return new Fornecedor();
        }

        /// <summary>
        /// Responsável por buscar o fornecedor
        /// </summary>
        /// <param name="id">Código identificado do fornecedor</param>
        /// <returns></returns>
        [ResponseType(typeof(Fornecedor))]
        [HttpGet]
        public Fornecedor Get(int id)
        {

            var obj = unitOfWork.Repository<Fornecedor>().GetById(id);

            return obj;
        }

        /// <summary>
        /// Responsável por buscar lista de fornecedores
        /// </summary>        
        /// <returns></returns>
        [Route("list")]
        [ResponseType(typeof(IList<Fornecedor>))]
        [HttpGet]
        public IList<Fornecedor> List()
        {
            var itens = unitOfWork.Repository<Fornecedor>().Table.ToList();

            return itens;
        }

        /// <summary>
        /// Responsável por atualizar o fornecedor
        /// </summary>
        /// <param name="obj">Fornecedor</param>
        /// <returns></returns>        
        [HttpPut]
        public string Put(Fornecedor obj)
        {
            var rep = unitOfWork.Repository<Fornecedor>();
            rep.Update(obj);
            unitOfWork.Save();
            return "Fornecedor Atualizado com sucesso";
        }

        /// <summary>
        /// Responsável por excluir o fornecedor
        /// </summary>
        /// <param name="id">Fornecedor</param>
        /// <returns></returns>
        [HttpDelete]
        public string Delete(int id)
        {
            var rep = unitOfWork.Repository<Fornecedor>();
            var user = rep.GetById(id);
            if (user != null)
            {
                rep.Delete(user);
                unitOfWork.Save();
            }
            else
                return "Fornecedor não encontrado";

            return "Fornecedor removido com sucesso";
        }

        /// <summary>
        /// Responsável por gravar fornecedor
        /// </summary>
        /// <param name="obj">Fornecedor</param>
        /// <returns></returns>        
        [HttpPost]
        public string Post(Fornecedor obj)
        {
            var rep = unitOfWork.Repository<Fornecedor>();

            var objAux = rep.GetById(obj.FornecedorID);
            if (objAux != null)
            {                
                objAux.Email = obj.Email;
                objAux.Nome = obj.Nome;                
                rep.Update(objAux);
            }
            else
            {
                rep.Insert(obj);
            }

            unitOfWork.Save();
            return "Fornecedor gravado com sucesso";
        }
    }
}