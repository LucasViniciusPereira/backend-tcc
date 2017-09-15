using System;
using backend_tcc.bs.administrador.Class;
using backend_tcc.lib.repository.Class;
using backend_tcc.lib.repository.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using backend_tcc.bs.estoque.Class;
using backend_tcc.bs.common.Class;

namespace backend_tcc.Tests
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void EntityFramework()
        {
            try
            {
                #region Arrange

                UnitOfWork unitOfWork = new UnitOfWork(new EFDbContext());
                
                #endregion

                #region Act

                var usuario = unitOfWork.Repository<Usuario>().Table.Where(c => c.UsuarioID == "usuario" && c.Senha == "senha").SingleOrDefault();

                #endregion

                #region Assert

                Assert.IsNotNull(usuario);

                #endregion                
            }
            catch (Exception)
            {
                throw;
            }            
        }

        [TestMethod]
        public void BuscaDeProdutos()
        {
            try
            {
                #region Arrange

                UnitOfWork unitOfWork = new UnitOfWork(new EFDbContext());

                #endregion

                #region Act

                var count = unitOfWork.Repository<Produto>().Table.Count();

                #endregion

                #region Assert

                Assert.IsTrue(count > 0);

                #endregion                
            }
            catch (Exception)
            {
                throw;
            }
        }

        [TestMethod]
        public void CadastrarProduto()
        {
            try
            {
                #region Arrange

                UnitOfWork unitOfWork = new UnitOfWork(new EFDbContext());

                #endregion

                #region Act

                var produto = new Produto
                {
                    Fabricante = unitOfWork.Repository<Fabricante>().Table.FirstOrDefault(),
                    Fornecedor = unitOfWork.Repository<Fornecedor>().Table.FirstOrDefault(),
                    Nome = "Produto de Teste",
                    Preco = 0.01M,
                    UN = "UN"
                };

                unitOfWork.Repository<Produto>().Insert(produto);

                unitOfWork.Save();

                #endregion

                #region Assert

                Assert.IsTrue(produto.ProdutoID > 0);

                #endregion                
            }
            catch (Exception)
            {
                throw;
            }
        }

        [TestMethod]
        public void AlterarProduto()
        {
            try
            {
                #region Arrange

                UnitOfWork unitOfWork = new UnitOfWork(new EFDbContext());

                #endregion

                #region Act

                var produtoTable = unitOfWork.Repository<Produto>().Table;

                var produtos = produtoTable.ToList();
                var produto = produtos.LastOrDefault();

                produto.Nome = produto.Nome + " Atualizado";

                unitOfWork.Repository<Produto>().Update(produto);
                unitOfWork.Save();

                var produto2 = unitOfWork.Repository<Produto>().Table.Single(c=> c.ProdutoID == produto.ProdutoID);

                #endregion

                #region Assert

                Assert.AreEqual(produto.Nome, produto2.Nome);

                #endregion                
            }
            catch (Exception)
            {
                throw;
            }
        }

        [TestMethod]
        public void ExcluirProduto()
        {
            try
            {
                #region Arrange

                UnitOfWork unitOfWork = new UnitOfWork(new EFDbContext());

                #endregion

                #region Act

                var produto = unitOfWork.Repository<Produto>().Table.ToList().LastOrDefault();
                var idProd = produto.ProdutoID;

                unitOfWork.Repository<Produto>().Delete(produto);
                unitOfWork.Save();

                var produto2 = unitOfWork.Repository<Produto>().Table.SingleOrDefault(c => c.ProdutoID == idProd);

                #endregion

                #region Assert

                Assert.IsNull(produto2);

                #endregion                
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
