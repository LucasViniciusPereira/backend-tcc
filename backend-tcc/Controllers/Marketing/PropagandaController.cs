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
    /// API responsável por propaganda
    /// </summary>
    [RoutePrefix("api/propaganda")]
    [Authorize]
    public class PropagandaController : ApiController
    {
        private UnitOfWork unitOfWork = new UnitOfWork(new EFDbContext());
        /// <summary>
        /// Responsável por criar um nova propaganda
        /// </summary>
        /// <returns>Nova instância de propaganda</returns>
        [Route("create")]
        [ResponseType(typeof(Propaganda))]
        [HttpGet]
        public Propaganda Create()
        {
            return new Propaganda();
        }

        /// <summary>
        /// Responsável por buscar propaganda
        /// </summary>
        /// <param name="id">Código identificado da propaganda</param>
        /// <returns></returns>
        [ResponseType(typeof(Propaganda))]
        [HttpGet]
        public Propaganda Get(int id)
        {

            var obj = unitOfWork.Repository<Propaganda>().GetById(id);

            return obj;
        }

        /// <summary>
        /// Responsável por buscar lista de propagandas
        /// </summary>        
        /// <returns></returns>
        [Route("list")]
        [ResponseType(typeof(IList<Propaganda>))]
        [HttpGet]
        public IList<Propaganda> List()
        {
            var itens = unitOfWork.Repository<Propaganda>().Table.ToList();

            return itens;
        }

        /// <summary>
        /// Responsável por atualizar propaganda
        /// </summary>
        /// <param name="obj">Propaganda</param>
        /// <returns></returns>        
        [HttpPut]
        public string Put(Propaganda obj)
        {
            var rep = unitOfWork.Repository<Propaganda>();
            rep.Update(obj);
            unitOfWork.Save();
            return "Propaganda Atualizado com sucesso";
        }

        /// <summary>
        /// Responsável por excluir propaganda
        /// </summary>
        /// <param name="id">Propaganda</param>
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
                return "Propaganda não encontrada";

            return "Propaganda removida com sucesso";
        }

        /// <summary>
        /// Responsável por gravar propaganda
        /// </summary>
        /// <param name="obj">Propaganda</param>
        /// <returns></returns>        
        [HttpPost]
        public string Post(Propaganda obj)
        {
            var rep = unitOfWork.Repository<Propaganda>();

            var objAux = rep.GetById(obj.PropagandaID);
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
            return "Propaganda gravada com sucesso";
        }
    }
}