using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDNJR.DW.TCC.Domain.Entidades;

namespace WDNJR.DW.TCC.Domain.Repository
{
    public class DepartamentoRepository : BaseRepository, IDisposable
    {
        public DepartamentoRepository(string cnnString) : base(cnnString)
        {

        }

        public void Dispose()
        {
            GC.Collect();
        }

        public IQueryable<Departamento> Departamentos
        {
            get { return Contexto.Departamento; }
        }

        public void Salvar(Departamento departamento, int userID)
        {
            try
            {
                Departamento aux = Contexto.Departamento.Where(p => p.DepartamentoID == departamento.DepartamentoID).SingleOrDefault();

                if (aux != null)
                {
                    aux.Descricao = departamento.Descricao;

                    aux.DataAlteracao = DateTime.Now;
                    aux.UsuarioAlteracaoID = userID;
                }
                else
                {
                    departamento.DataCriacao = DateTime.Now;
                    departamento.UsuarioCriacaoID = userID;
                    Contexto.Departamento.Add(departamento);
                }

                Contexto.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao gravar Departamento : " + ex.Message);
            }
        }

        public void Excluir(int departamentoID)
        {
            try
            {
                Departamento aux = Contexto.Departamento.Where(p => p.DepartamentoID == departamentoID).SingleOrDefault();

                if (aux != null)
                {
                    Contexto.Departamento.Remove(aux);
                }
                
                Contexto.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir Departamento : " + ex.Message);
            }
        }
    }
}
