using FluentData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Etl.Core;
using System.Linq;
using RhinoETL_POC.Migrar.Categorias;
using System.Collections.Generic;
using RhinoETL_POC.Migrar.CategoriasComAtualizacao;

namespace RhinoETL_POC_Tests.MigrarCategoriasComAtualizacaoTests
{
    [TestClass]
    public class DeveRecuperarAsCategoriasDaOrigem 
    {
        private static List<Row> returnRows = new List<Row>();
        private readonly IDbContext contexto = new DbContext().ConnectionStringName("Destino", DbProviderTypes.SqlServerCompact40);

        public class TestarRecuperarAsCategoriasDaOrigem : EtlProcess
        {
            protected override void Initialize()
            {
                Register(new RecuperarCategoriasDoDestino());
                Register(new ResultsOperation(returnRows));
            }
        }

        [TestInitialize]
        public void Inicializar()
        {
            returnRows.Clear();

            contexto.Sql("delete from categorias").Execute();

            contexto.Sql("INSERT INTO categorias (Id, Nome, Descricao) VALUES(@0, @1, @2)")
               .Parameters(1, "TesteAlterado 1", "Categoria Teste 1 ALterada")
               .Execute();

            contexto.Sql("INSERT INTO categorias (Id, Nome, Descricao) VALUES(@0, @1, @2)")
                .Parameters(2, "TesteAlterado 2", "Categoria Teste 2 ALterada")
                .Execute();

            var testarRecuperarCategoriasDaOrigem = new TestarRecuperarAsCategoriasDaOrigem();
            testarRecuperarCategoriasDaOrigem.Execute();
        }

        [TestMethod]
        public void DeveRetornarDuasCategorias()
        {
            Assert.AreEqual(2, returnRows.Count);
        }

        [TestMethod]
        public void DeveRetornarOsCamposDasCategorias()
        {
            Assert.IsTrue(returnRows.All(o => o["Id"] != null));
            Assert.IsTrue(returnRows.All(o => o["Nome"] != null));
            Assert.IsTrue(returnRows.All(o => o["Descricao"] != null));
        }
    }
}
