using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDNJR.DW.TCC.Domain.Entidades;

namespace WDNJR.DW.TCC.Domain.Repository
{
    public class AvaliacaoRepository : BaseRepository, IDisposable
    {
        public AvaliacaoRepository(string cnnString) : base(cnnString)
        {

        }

        public void Dispose()
        {
            GC.Collect();
        }

        public IQueryable<Avaliacao> Avaliacoes
        {
            get { return Contexto.Avaliacao; }
        }

        public IQueryable<Questionario> Questionarios
        {
            get { return Contexto.Questionario; }
        }

        public IQueryable<QuestionarioResposta> QuestionarioRespostas
        {
            get { return Contexto.QuestionarioResposta; }
        }

        public void Salvar(Avaliacao avaliacao, int userID)
        {
            try
            {
                Avaliacao aux;
                if (avaliacao is AvaliacaoSAD360)
                {
                    aux = Contexto.Avaliacao.OfType<AvaliacaoSAD360>().Where(p => p.AvaliacaoID == avaliacao.AvaliacaoID).SingleOrDefault();

                    if (aux != null)
                    {
                        //((AvaliacaoSAD360)avaliacao).Nome = "Nome Alterado";
                    }
                }
                else
                {
                    aux = Contexto.Avaliacao.Where(p => p.AvaliacaoID == avaliacao.AvaliacaoID).SingleOrDefault();
                }

                if (aux == null)
                {

                    avaliacao.DataCriacao = DateTime.Now;
                    avaliacao.UsuarioCriacaoID = userID;

                    Contexto.Avaliacao.Add(avaliacao);
                }
                else
                {
                    aux.Descricao = avaliacao.Descricao;
                    aux.DataAvaliacao = avaliacao.DataAvaliacao;

                    aux.DataAlteracao = DateTime.Now;
                    aux.UsuarioAlteracaoID = userID;
                }

                Contexto.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao gravar Avaliação : " + ex.Message);
            }
        }

        public void SalvarQuestionario(Questionario questionario, int userID)
        {
            try
            {
                Questionario aux = Contexto.Questionario.Where(p => p.QuestionarioID == questionario.QuestionarioID).SingleOrDefault();
                

                if (aux == null)
                {

                    questionario.DataCriacao = DateTime.Now;
                    questionario.UsuarioCriacaoID = userID;

                    Contexto.Questionario.Add(questionario);
                }
                else
                {
                    aux.ModeloQuestionarioID = questionario.ModeloQuestionarioID;
                    aux.AvaliadorID = questionario.AvaliadorID;
                    aux.AvaliadoID = questionario.AvaliadoID;
                    aux.AvaliacaoID = questionario.AvaliacaoID;

                    aux.DataAlteracao = DateTime.Now;
                    aux.UsuarioAlteracaoID = userID;
                }

                Contexto.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao gravar Questionário : " + ex.Message);
            }
        }

        public void SalvarRespostaQuestionario(QuestionarioResposta resposta, int userID)
        {
            try
            {
                QuestionarioResposta aux = Contexto.QuestionarioResposta.Where(p => p.QuestionarioRespostaID == resposta.QuestionarioRespostaID).SingleOrDefault();


                if (aux == null)
                {

                    resposta.DataCriacao = DateTime.Now;
                    resposta.UsuarioCriacaoID = userID;

                    Contexto.QuestionarioResposta.Add(resposta);
                }
                else
                {
                    aux.PerguntaID = resposta.PerguntaID;
                    aux.QuestionarioID = resposta.QuestionarioID;
                    aux.RespostaAberta = resposta.RespostaAberta;
                    aux.RespostaID = resposta.RespostaID;

                    aux.DataAlteracao = DateTime.Now;
                    aux.UsuarioAlteracaoID = userID;
                }

                Contexto.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao gravar Resposta do Questionário : " + ex.Message);
            }
        }

        public void Excluir(int avaliacaoID)
        {
            try
            {
                Avaliacao aux = Contexto.Avaliacao.Where(p => p.AvaliacaoID == avaliacaoID).SingleOrDefault();
                

                if (aux != null)
                {
                    Contexto.Avaliacao.Remove(aux);
                }                

                Contexto.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir Avaliação : " + ex.Message);
            }
        }

        public void ExcluirQuestionario(int questionarioID)
        {
            try
            {
                Questionario aux = Contexto.Questionario.Where(p => p.QuestionarioID == questionarioID).SingleOrDefault();


                if (aux != null)
                {
                    Contexto.Questionario.Remove(aux);
                }                

                Contexto.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir Questionário : " + ex.Message);
            }
        }

        public void ExcluirRespostaQuestionario(int respostaID)
        {
            try
            {
                QuestionarioResposta aux = Contexto.QuestionarioResposta.Where(p => p.QuestionarioRespostaID == respostaID).SingleOrDefault();


                if (aux != null)
                {
                    Contexto.QuestionarioResposta.Remove(aux);
                }

                Contexto.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir Resposta do Questionário : " + ex.Message);
            }
        }
    }
}
