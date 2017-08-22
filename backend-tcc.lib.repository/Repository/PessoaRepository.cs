using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDNJR.DW.TCC.Domain.Entidades;
using WDNJR.DW.TCC.Domain.Interfaces;

namespace WDNJR.DW.TCC.Domain.Repository
{
    public class PessoaRepository : BaseRepository, IDisposable
    {
        public PessoaRepository(string cnnString): base(cnnString)
        {

        }

        public void Dispose()
        {
            GC.Collect();
        }

        public IQueryable<Pessoa> Pessoas
        {
            get { return Contexto.Pessoa; }
        }

        public void Salvar(Pessoa pessoa, int userID)
        {
            try
            {
                Pessoa aux;
                if (pessoa is Funcionario)
                {
                    aux = Contexto.Pessoa.OfType<Funcionario>().Where(p => p.PessoaID == pessoa.PessoaID).SingleOrDefault();

                    if (aux != null)
                    {                        
                        ((Funcionario)aux).GestorID = ((Funcionario)pessoa).GestorID;
                        ((Funcionario)aux).DepartamentoID = ((Funcionario)pessoa).DepartamentoID;
                        ((Funcionario)aux).FuncaoID = ((Funcionario)pessoa).FuncaoID;
                    }
                }
                else
                {
                    aux = Contexto.Pessoa.Where(p => p.PessoaID == pessoa.PessoaID).SingleOrDefault();                  
                }

                if (aux == null)
                {
                    pessoa.DataCriacao = DateTime.Now;
                    pessoa.UsuarioCriacaoID = userID;
                    Contexto.Pessoa.Add(pessoa);
                }
                else
                {
                    aux.Nome = pessoa.Nome;
                    aux.CPF = pessoa.CPF;
                    aux.Sexo = pessoa.Sexo;
                    aux.Email = pessoa.Email;
                    aux.Endereco = pessoa.Endereco;

                    aux.DataAlteracao = DateTime.Now;
                    aux.UsuarioAlteracaoID = userID;
                }

                Contexto.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao gravar Pessoa : " + ex.Message);
            }
        }

        public void Excluir(int pessoaID)
        {
            try
            {
                Pessoa aux = Contexto.Pessoa.Where(p => p.PessoaID == pessoaID).SingleOrDefault();

                if (aux != null)
                {
                    Contexto.Pessoa.Remove(aux);
                }

                Contexto.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir Pessoa : " + ex.Message);
            }
        }
    }
}
