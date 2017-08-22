using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDNJR.DW.TCC.Domain.Entidades;

namespace WDNJR.DW.TCC.Domain.Repository
{
    public class UsuarioRepository : BaseRepository, IDisposable
    {
        public UsuarioRepository(string cnnString) : base(cnnString)
        {

        }

        public void Dispose()
        {
            GC.Collect();
        }

        public IQueryable<Usuario> Usuarios
        {
            get { return Contexto.Usuario; }
        }

        public void Salvar(Usuario usuario, int userID)
        {
            try
            {
                Usuario aux = Contexto.Usuario.Where(p => p.UsuarioID == usuario.UsuarioID).SingleOrDefault();

                if (aux != null)
                {
                    aux.PessoaID = usuario.PessoaID;
                    aux.TipoUsuario = usuario.TipoUsuario;
                    aux.UserName = usuario.UserName;

                    aux.DataAlteracao = DateTime.Now;
                    aux.UsuarioAlteracaoID = userID;
                }
                else
                {
                    usuario.DataCriacao = DateTime.Now;
                    usuario.UsuarioCriacaoID = userID;
                    Contexto.Usuario.Add(usuario);
                }

                Contexto.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao gravar Usuário : " + ex.Message);
            }
        }

        public void AlterarSenha(int UsuarioID, string password, int userID)
        {
            try
            {
                Usuario aux = Contexto.Usuario.Where(p => p.UsuarioID == UsuarioID).SingleOrDefault();
                if (aux != null)
                {

                    aux.Password = Util.CriptografarSenha(password);
                    aux.UsuarioAlteracaoID = userID;
                    aux.DataAlteracao = DateTime.Now;
                    Contexto.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao alterar Senha : " + ex.Message);
            }
        }

        public void AlterarSenha(string userName, string password)
        {
            try
            {
                Usuario aux = Contexto.Usuario.Where(p => p.UserName == userName).SingleOrDefault();
                if (aux != null)
                {
                    aux.Password = Util.CriptografarSenha(password);
                    aux.UsuarioAlteracaoID = aux.UsuarioID;
                    aux.DataAlteracao = DateTime.Now;
                    Contexto.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao alterar Senha : " + ex.Message);
            }
        }

        public void Excluir(int usuarioID)
        {
            try
            {
                Usuario aux = Contexto.Usuario.Where(p => p.UsuarioID == usuarioID).SingleOrDefault();

                if (aux != null)
                {
                    Contexto.Usuario.Remove(aux);
                }

                Contexto.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir Usuário : " + ex.Message);
            }
        }
    }
}
