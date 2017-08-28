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
    /// API responsável por veículos
    /// </summary>
    [RoutePrefix("api/veiculo")]
    [Authorize]
    public class VeiculoController : ApiController
    {
        private UnitOfWork unitOfWork = new UnitOfWork(new EFDbContext());
        /// <summary>
        /// Responsável por criar um novo veículo
        /// </summary>
        /// <returns>Nova instância de veículo</returns>
        [Route("create")]
        [ResponseType(typeof(Veiculo))]
        [HttpGet]
        public Veiculo Create()
        {
            return new Veiculo();
        }

        /// <summary>
        /// Responsável por buscar o veículo
        /// </summary>
        /// <param name="id">Código identificado do veículo</param>
        /// <returns></returns>
        [ResponseType(typeof(Veiculo))]
        [HttpGet]
        public Veiculo Get(int id)
        {

            var obj = unitOfWork.Repository<Veiculo>().GetById(id);

            return obj;
        }

        /// <summary>
        /// Responsável por buscar lista de veículos
        /// </summary>        
        /// <returns></returns>
        [Route("list")]
        [ResponseType(typeof(IList<Veiculo>))]
        [HttpGet]
        public IList<Veiculo> List()
        {
            var itens = unitOfWork.Repository<Veiculo>().Table.ToList();

            return itens;
        }

        /// <summary>
        /// Responsável por atualizar o veículo
        /// </summary>
        /// <param name="obj">Veiculo</param>
        /// <returns></returns>        
        [HttpPut]
        public string Put(Veiculo obj)
        {
            var rep = unitOfWork.Repository<Veiculo>();
            rep.Update(obj);
            unitOfWork.Save();
            return "Veículo Atualizado com sucesso";
        }

        /// <summary>
        /// Responsável por excluir o veículo
        /// </summary>
        /// <param name="id">Veiculo</param>
        /// <returns></returns>
        [HttpDelete]
        public string Delete(int id)
        {
            var rep = unitOfWork.Repository<Veiculo>();
            var user = rep.GetById(id);
            if (user != null)
            {
                rep.Delete(user);
                unitOfWork.Save();
            }
            else
                return "Veículo não encontrado";

            return "Veículo removido com sucesso";
        }

        /// <summary>
        /// Responsável por gravar veículo
        /// </summary>
        /// <param name="obj">Veiculo</param>
        /// <returns></returns>        
        [HttpPost]
        public string Post(Veiculo obj)
        {
            var rep = unitOfWork.Repository<Veiculo>();

            var objAux = rep.GetById(obj.VeiculoID);
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
            return "Veículo gravado com sucesso";
        }
    }
}