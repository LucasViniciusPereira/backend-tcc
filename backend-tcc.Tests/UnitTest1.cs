using System;
using backend_tcc.bs.administrador.Class;
using backend_tcc.lib.repository.Class;
using backend_tcc.lib.repository.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace backend_tcc.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestarEntityFramework()
        {
            try
            {
                UnitOfWork unitOfWork = new UnitOfWork(new EFDbContext());
                var usuario = unitOfWork.Repository<Usuario>().Table.Where(c => c.UsuarioID == "usuario" && c.Senha == "senha").SingleOrDefault();

                if (usuario != null)
                {

                }
            }
            catch (Exception)
            {
                throw;
            }            
        }
    }
}
