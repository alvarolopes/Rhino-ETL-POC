using System.Linq;
using FluentData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Etl.Core;
using Rhino.Etl.Core.Operations;
using RhinoETL_POC.Migrar.Categorias;
using System.Collections.Generic;

namespace RhinoETL_POC_Tests.MigrarCategoriasComAtualizacaoTests
{
    [TestClass]
    public class DeveAlterarAsCategoriasNoDestino
    {
        private List<dynamic> returnRows = new List<dynamic>();
        private readonly IDbContext contexto = new DbContext().ConnectionStringName("Destino", DbProviderTypes.SqlServerCompact40);

        public class TestarAlterarAsCategoriasNoDestino : EtlProcess
        {
            protected override void Initialize()
            {
                Register(new GenerateTestData());
                Register(new GravarCategoriasNoDestino());
            }
        }

        public class GenerateTestData : AbstractOperation
        {
            public override IEnumerable<Row> Execute(IEnumerable<Row> rows)
            {
                var linha1 = new Row
                    {
                        {"ACAO", "ALTERAR"},
                        {"Category ID", 1},
                        {"Category Name", "Teste1"},
                        {"Description", "Categoria Teste 1"}
                    };

                var linha2 = new Row
                    {
                        {"ACAO", "ALTERAR"},
                        {"Category ID", 2},
                        {"Category Name", "Teste2"},
                        {"Description", "Categoria Teste 2"}
                    };

                yield return linha1;
                yield return linha2;
            }
        }

        [TestInitialize]
        public void Inicializar()
        {
            contexto.Sql("delete from categorias").Execute();            

            contexto.Sql("INSERT INTO categorias (Id, Nome, Descricao) VALUES(@0, @1, @2)")
                .Parameters(1,"TesteAlterado 1","Categoria Teste 1 ALterada") 
                .Execute();

            contexto.Sql("INSERT INTO categorias (Id, Nome, Descricao) VALUES(@0, @1, @2)")
                .Parameters(2, "TesteAlterado 2", "Categoria Teste 2 ALterada")
                .Execute();

            var testarGravarAsCategoriasNoDestino = new TestarAlterarAsCategoriasNoDestino();
            testarGravarAsCategoriasNoDestino.Execute();   
        }

        [TestMethod]
        public void DeveAlterarAsCategorias()
        {
            returnRows = contexto.Sql("Select * from categorias").Query();

            Assert.IsNotNull(returnRows);
            Assert.AreEqual(2, returnRows.Count);

            var linha1 = returnRows.Single(o => o.Id == 1);
            Assert.AreEqual("TesteAlterado 1", linha1.Nome);

            var linha2 = returnRows.Single(o => o.Id == 2);
            Assert.AreEqual("TesteAlterado 2", linha2.Nome);            
        }        
    }
}
