using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDNJR.DW.TCC.Domain.Entidades;

namespace WDNJR.DW.TCC.Domain.Repository
{
    public class FuncaoRepository : BaseRepository, IDisposable
    {
        public FuncaoRepository(string cnnString) : base(cnnString)
        {

        }

        public void Dispose()
        {
            GC.Collect();
        }

        public IQueryable<Funcao> Funcoes
        {
            get { return Contexto.Funcao; }
        }

        public void Salvar(Funcao funcao, int userID)
        {
            try
            {
                Funcao aux = Contexto.Funcao.Where(p => p.FuncaoID == funcao.FuncaoID).SingleOrDefault();

                if (aux != null)
                {
                    aux.Descricao = funcao.Descricao;
                    aux.DataAlteracao = DateTime.Now;
                    aux.UsuarioAlteracaoID = userID;
                }
                else
                {
                    funcao.DataCriacao = DateTime.Now;
                    funcao.UsuarioCriacaoID = userID;
                    Contexto.Funcao.Add(funcao);
                }

                Contexto.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao gravar Função : " + ex.Message);
            }
        }

        public void Excluir(int funcaoID)
        {
            try
            {
                Funcao aux = Contexto.Funcao.Where(p => p.FuncaoID == funcaoID).SingleOrDefault();

                if (aux != null)
                {
                    Contexto.Funcao.Remove(aux);
                }

                Contexto.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir Função : " + ex.Message);
            }
        }
    }
}
