using backend_tcc.bs.marketing.Class;
using backend_tcc.lib.repository.Class;
using backend_tcc.lib.repository.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace backend_tcc.ms.administrador.Controllers
{
    /// <summary>
    /// API responsável por promoção
    /// </summary>
    [RoutePrefix("api/promocao")]
    [Authorize]
    public class PromocaoController : ApiController
    {
        private UnitOfWork unitOfWork = new UnitOfWork(new EFDbContext());
        /// <summary>
        /// Responsável por criar um nova promoção
        /// </summary>
        /// <returns>Nova instância de promoção</returns>
        [Route("create")]
        [ResponseType(typeof(Promocao))]
        [HttpGet]
        public Promocao Create()
        {
            return new Promocao();
        }

        /// <summary>
        /// Responsável por buscar promoção
        /// </summary>
        /// <param name="id">Código identificado da promoção</param>
        /// <returns></returns>
        [ResponseType(typeof(Promocao))]
        [HttpGet]
        public Promocao Get(int id)
        {

            var obj = unitOfWork.Repository<Promocao>().GetById(id);

            return obj;
        }

        /// <summary>
        /// Responsável por buscar lista de promoção
        /// </summary>        
        /// <returns></returns>
        [Route("list")]
        [ResponseType(typeof(IList<Promocao>))]
        [HttpGet]
        public IList<Promocao> List()
        {
            var itens = unitOfWork.Repository<Promocao>().Table.ToList();

            return itens;
        }

        /// <summary>
        /// Responsável por atualizar promoção
        /// </summary>
        /// <param name="obj">Promocao</param>
        /// <returns></returns>        
        [HttpPut]
        public string Put(Promocao obj)
        {
            var rep = unitOfWork.Repository<Promocao>();
            rep.Update(obj);
            unitOfWork.Save();
            return "Promoção Atualizada com sucesso";
        }

        /// <summary>
        /// Responsável por excluir promoção
        /// </summary>
        /// <param name="id">Promocao</param>
        /// <returns></returns>
        [HttpDelete]
        public string Delete(int id)
        {
            var rep = unitOfWork.Repository<Propaganda>();
            var user = rep.GetById(id);
            if (user != null)
            {
                rep.Delete(user);
                unitOfWork.Save();
            }
            else
                return "Promoção não encontrada";

            return "Promoção removida com sucesso";
        }

        /// <summary>
        /// Responsável por gravar promoção
        /// </summary>
        /// <param name="obj">Promocao</param>
        /// <returns></returns>        
        [HttpPost]
        public string Post(Promocao obj)
        {
            var rep = unitOfWork.Repository<Promocao>();

            var objAux = rep.GetById(obj.PromocaoID);
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
            return "Promoção gravada com sucesso";
        }
    }
}