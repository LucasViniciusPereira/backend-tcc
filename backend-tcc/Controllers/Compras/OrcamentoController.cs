using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using backend_tcc.bs.compras.Class;
using backend_tcc.lib.repository.Class;
using backend_tcc.lib.repository.Repository;

namespace backend_tcc.ms.administrador.Controllers
{
    /// <summary>
    /// Orçamentos de Fornecedores
    /// </summary>
    [RoutePrefix("api/orcamento")]
    [Authorize]
    public class OrcamentoController : ApiController
    {
        private UnitOfWork unitOfWork = new UnitOfWork(new EFDbContext());
        /// <summary>
        /// Responsável por criar um novo orçamento
        /// </summary>
        /// <returns>Nova instância de orçamento</returns>
        [Route("create")]
        [ResponseType(typeof(Orcamento))]
        [HttpGet]
        public Orcamento Create()
        {
            return new Orcamento();
        }

        /// <summary>
        /// Responsável por buscar o orçamento
        /// </summary>
        /// <param name="id">Código identificado do orçamento</param>
        /// <returns></returns>
        [ResponseType(typeof(Orcamento))]
        [HttpGet]
        public Orcamento Get(int id)
        {

            var obj = unitOfWork.Repository<Orcamento>().GetById(id);

            return obj;
        }

        /// <summary>
        /// Responsável por buscar lista de orçamentos
        /// </summary>        
        /// <returns></returns>
        [Route("orcamentos")]
        [ResponseType(typeof(IList<Orcamento>))]
        [HttpGet]
        public IList<Orcamento> Orcamentos()
        {
            var itens = unitOfWork.Repository<Orcamento>().Table.ToList();

            return itens;
        }

        /// <summary>
        /// Responsável por atualizar o orçamento
        /// </summary>
        /// <param name="obj">Orcamento</param>
        /// <returns></returns>        
        [HttpPut]
        public string Put(Orcamento obj)
        {
            var rep = unitOfWork.Repository<Orcamento>();
            rep.Update(obj);
            unitOfWork.Save();
            return "Orçamento Atualizado com sucesso";
        }

        /// <summary>
        /// Responsável por excluir o orçamento
        /// </summary>
        /// <param name="id">Orcamento</param>
        /// <returns></returns>
        [HttpDelete]
        public string Delete(int id)
        {
            var rep = unitOfWork.Repository<Orcamento>();
            var user = rep.GetById(id);
            if (user != null)
            {
                rep.Delete(user);
                unitOfWork.Save();
            }
            else
                return "Orçamento não encontrado";

            return "Orçamento removido com sucesso";
        }

        /// <summary>
        /// Responsável por gravar orçamento
        /// </summary>
        /// <param name="obj">Orcamento</param>
        /// <returns></returns>        
        [HttpPost]
        public string Post(Orcamento obj)
        {
            var rep = unitOfWork.Repository<Orcamento>();

            var objAux = rep.GetById(obj.OrcamentoID);
            if (objAux != null)
            {
                objAux.DataPedido = obj.DataPedido;
                objAux.FornecedorID = obj.FornecedorID;
                objAux.SituacaoOrcamentoID = obj.SituacaoOrcamentoID;
                objAux.Valor = obj.Valor;

                foreach (var itemOrcamento in obj.Itens)
                {
                    if (itemOrcamento.ItemOrcamentoID <= 0)
                        objAux.Itens.Add(itemOrcamento);
                    else
                    {
                        var itemAux = objAux.Itens.SingleOrDefault(c => c.ItemOrcamentoID == itemOrcamento.ItemOrcamentoID);

                        itemAux.ProdutoID = itemOrcamento.ProdutoID;
                        itemAux.Quantidade = itemOrcamento.Quantidade;
                        itemAux.Valor = itemOrcamento.Valor;
                    }
                }

                rep.Update(objAux);
            }
            else
            {
                rep.Insert(obj);
            }

            unitOfWork.Save();
            return "Orçamento gravado com sucesso";
        }
    }
}