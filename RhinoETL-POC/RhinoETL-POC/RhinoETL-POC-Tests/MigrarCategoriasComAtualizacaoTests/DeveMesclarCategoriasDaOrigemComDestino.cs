using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Etl.Core;
using Rhino.Etl.Core.Operations;
using RhinoETL_POC.Migrar.Categorias;
using RhinoETL_POC.Migrar.CategoriasComAtualizacao;

namespace RhinoETL_POC_Tests.MigrarCategoriasComAtualizacaoTests
{
    [TestClass]
    public class DeveMesclarCategoriasDaOrigemComDestino
    {
        public class GenerateTestDataOrigem : AbstractOperation
        {
            public override IEnumerable<Row> Execute(IEnumerable<Row> rows)
            {
                var linha1 = new Row();
                linha1.Add("Category ID", 1);
                linha1.Add("Category Name", "Teste1");
                linha1.Add("Description", "Categoria Teste 1");

                var linha2 = new Row();
                linha2.Add("Category ID", 2);
                linha2.Add("Category Name", "Teste2");
                linha2.Add("Description", "Categoria Teste 2");

                yield return linha1;
                yield return linha2;
            }
        }

        public class GenerateTestDataDestino : AbstractOperation
        {
            public override IEnumerable<Row> Execute(IEnumerable<Row> rows)
            {
                var linha1 = new Row();
                linha1.Add("Id", 1);
                linha1.Add("Nome", "Teste1");
                linha1.Add("Descricao", "Categoria Teste 1");

                yield return linha1;
            }
        }

        private static List<Row> returnRows = new List<Row>();

        public class TestarMesclarCategoriasDaOrigemComDestino : EtlProcess
        {
            protected override void Initialize()
            {
                Register(new MesclarCategorias()
                    .Left(new GenerateTestDataOrigem())
                    .Right(new GenerateTestDataDestino()));

                Register(new ResultsOperation(returnRows));
            }
        }

        [TestInitialize]
        public void Inicializar()
        {
            returnRows.Clear();

            var testarRecuperarCategoriasDaOrigem = new TestarMesclarCategoriasDaOrigemComDestino();

            testarRecuperarCategoriasDaOrigem.Execute();
        }

        [TestMethod]
        public void DeveMesclarDuasCategorias()
        {
            Assert.AreEqual(2, returnRows.Count);
        }

        [TestMethod]
        public void AcaoDeveSerAlterar()
        {
            var linha1 = returnRows.Single(o => (int)o["Category ID"] == 1);
            Assert.AreEqual("ALTERAR", linha1["ACAO"]);            
        }

        [TestMethod]
        public void AcaoDeveSerInserir()
        {
            var linha1 = returnRows.Single(o => (int)o["Category ID"] == 2);
            Assert.AreEqual("INSERIR", linha1["ACAO"]);            
        }
    }
}
