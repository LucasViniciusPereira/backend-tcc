using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDNJR.DW.TCC.Domain.Entidades;

namespace WDNJR.DW.TCC.Domain.Repository
{
    public class ModeloQuestionarioRepository : BaseRepository, IDisposable
    {
        public ModeloQuestionarioRepository(string cnnString) : base(cnnString)
        {

        }

        public void Dispose()
        {
            GC.Collect();
        }

        public IQueryable<ModeloQuestionario> ModelosQuestionario
        {
            get { return Contexto.ModeloQuestionario; }
        }

        public IQueryable<Pergunta> Perguntas
        {
            get { return Contexto.Pergunta; }
        }

        public IQueryable<Resposta> Respostas
        {
            get { return Contexto.Resposta; }
        }

        public void Salvar(ModeloQuestionario modeloquestionario, int userID)
        {
            try
            {
                ModeloQuestionario aux = Contexto.ModeloQuestionario.Where(p => p.ModeloQuestionarioID == modeloquestionario.ModeloQuestionarioID).SingleOrDefault();

                if (aux != null)
                {
                    aux.Descricao = modeloquestionario.Descricao;
                    aux.Ativo = modeloquestionario.Ativo;

                    aux.DataAlteracao = DateTime.Now;
                    aux.UsuarioAlteracaoID = userID;
                }
                else
                {
                    modeloquestionario.DataCriacao = DateTime.Now;
                    modeloquestionario.UsuarioCriacaoID = userID;
                    Contexto.ModeloQuestionario.Add(modeloquestionario);
                }

                Contexto.SaveChanges();                

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao gravar Modelo de Questionário : " + ex.Message);
            }
        }

        public void SalvarPergunta(Pergunta pergunta, int userID)
        {
            try
            {
                Pergunta aux = Contexto.Pergunta.Where(p => p.PerguntaID == pergunta.PerguntaID).SingleOrDefault();

                if (aux != null)
                {
                    aux.Descricao = pergunta.Descricao;
                    aux.ModeloQuestionarioID = pergunta.ModeloQuestionarioID;
                    aux.Obrigatoria = pergunta.Obrigatoria;
                    aux.Ordem = pergunta.Ordem;
                    aux.TipoPergunta = pergunta.TipoPergunta;

                    aux.DataAlteracao = DateTime.Now;
                    aux.UsuarioAlteracaoID = userID;
                }
                else
                {
                    pergunta.DataCriacao = DateTime.Now;
                    pergunta.UsuarioCriacaoID = userID;
                    Contexto.Pergunta.Add(pergunta);
                }

                Contexto.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao gravar Pergunta : " + ex.Message);
            }
        }

        public void SalvarResposta(Resposta resposta, int userID)
        {
            try
            {
                Resposta aux = Contexto.Resposta.Where(p => p.RespostaID == resposta.RespostaID).SingleOrDefault();

                if (aux != null)
                {
                    aux.Descricao = resposta.Descricao;
                    aux.PerguntaID = resposta.PerguntaID;
                    aux.Peso = resposta.Peso;

                    aux.DataAlteracao = DateTime.Now;
                    aux.UsuarioAlteracaoID = userID;
                }
                else
                {
                    resposta.DataCriacao = DateTime.Now;
                    resposta.UsuarioCriacaoID = userID;
                    Contexto.Resposta.Add(resposta);
                }

                Contexto.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao gravar Resposta : " + ex.Message);
            }
        }

        public void Excluir(int modeloQuestionarioID)
        {
            try
            {
                ModeloQuestionario aux = Contexto.ModeloQuestionario.Where(p => p.ModeloQuestionarioID == modeloQuestionarioID).SingleOrDefault();

                if (aux != null)
                {
                    Contexto.ModeloQuestionario.Remove(aux);
                }

                Contexto.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir Modelo Questionário : " + ex.Message);
            }
        }

        public void ExcluirPergunta(int perguntaID)
        {
            try
            {
                Pergunta aux = Contexto.Pergunta.Where(p => p.PerguntaID == perguntaID).SingleOrDefault();

                if (aux != null)
                {
                    Contexto.Pergunta.Remove(aux);
                }

                Contexto.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir Pergunta : " + ex.Message);
            }
        }

        public void ExcluirResposta(int respostaID)
        {
            try
            {
                Resposta aux = Contexto.Resposta.Where(p => p.RespostaID == respostaID).SingleOrDefault();

                if (aux != null)
                {
                    Contexto.Resposta.Remove(aux);
                }

                Contexto.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir Resposta : " + ex.Message);
            }
        }
    }
}
