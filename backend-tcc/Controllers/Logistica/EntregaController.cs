using backend_tcc.bs.logistica.Class;
using backend_tcc.lib.repository.Class;
using backend_tcc.lib.repository.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace backend_tcc.ms.administrador.Controllers
{
    /// <summary>
    /// API responsável por entregas
    /// </summary>
    [RoutePrefix("api/entrega")]
    [Authorize]
    public class EntregaController : ApiController
    {
        private UnitOfWork unitOfWork = new UnitOfWork(new EFDbContext());
        /// <summary>
        /// Responsável por criar um nova entrega
        /// </summary>
        /// <returns>Nova instância de entrega</returns>
        [Route("create")]
        [ResponseType(typeof(Entrega))]
        [HttpGet]
        public Entrega Create()
        {
            return new Entrega();
        }

        /// <summary>
        /// Responsável por buscar entrega
        /// </summary>
        /// <param name="id">Código identificado da entrega</param>
        /// <returns></returns>
        [ResponseType(typeof(Entrega))]
        [HttpGet]
        public Entrega Get(int id)
        {

            var obj = unitOfWork.Repository<Entrega>().GetById(id);

            return obj;
        }

        /// <summary>
        /// Responsável por buscar lista de entrega
        /// </summary>        
        /// <returns></returns>
        [Route("list")]
        [ResponseType(typeof(IList<Entrega>))]
        [HttpGet]
        public IList<Entrega> List()
        {
            var itens = unitOfWork.Repository<Entrega>().Table.ToList();

            return itens;
        }

        /// <summary>
        /// Responsável por atualizar entrega
        /// </summary>
        /// <param name="obj">Entrega</param>
        /// <returns></returns>        
        [HttpPut]
        public string Put(Entrega obj)
        {
            var rep = unitOfWork.Repository<Entrega>();
            rep.Update(obj);
            unitOfWork.Save();
            return "Entrega Atualizada com sucesso";
        }

        /// <summary>
        /// Responsável por excluir entrega
        /// </summary>
        /// <param name="id">Entrega</param>
        /// <returns></returns>
        [HttpDelete]
        public string Delete(int id)
        {
            var rep = unitOfWork.Repository<Entrega>();
            var user = rep.GetById(id);
            if (user != null)
            {
                rep.Delete(user);
                unitOfWork.Save();
            }
            else
                return "Entrega não encontrada";

            return "Entrega removida com sucesso";
        }

        /// <summary>
        /// Responsável por gravar entrega
        /// </summary>
        /// <param name="obj">Entrega</param>
        /// <returns></returns>        
        [HttpPost]
        public string Post(Entrega obj)
        {
            var rep = unitOfWork.Repository<Entrega>();

            var objAux = rep.GetById(obj.EntregaID);
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
            return "Entrega gravada com sucesso";
        }
    }
}