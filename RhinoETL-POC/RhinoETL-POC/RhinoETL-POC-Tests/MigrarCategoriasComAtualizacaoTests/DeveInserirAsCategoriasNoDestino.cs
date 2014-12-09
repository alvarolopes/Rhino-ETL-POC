using FluentData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Etl.Core;
using Rhino.Etl.Core.Operations;
using System.Collections.Generic;
using RhinoETL_POC.Migrar.CategoriasComAtualizacao;

namespace RhinoETL_POC_Tests.MigrarCategoriasComAtualizacaoTests
{
    [TestClass]
    public class DeveInserirAsCategoriasNoDestino
    {
        private List<dynamic> returnRows = new List<dynamic>();
        private readonly IDbContext contexto = new DbContext().ConnectionStringName("Destino", DbProviderTypes.SqlServerCompact40);

        public class TestarInserirAsCategoriasNoDestino : EtlProcess
        {
            protected override void Initialize()
            {
                Register(new GenerateTestData());
                Register(new GravarCategoriasNoDestinoComAtualizacao());
            }
        }

        public class GenerateTestData : AbstractOperation
        {
            public override IEnumerable<Row> Execute(IEnumerable<Row> rows)
            {
                var linha1 = new Row
                    {
                        {"ACAO", "INSERIR"},
                        {"Category ID", 1},
                        {"Category Name", "Teste1"},
                        {"Description", "Categoria Teste 1"}
                    };

                var linha2 = new Row
                    {
                        {"ACAO", "INSERIR"},
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

            var testarGravarAsCategoriasNoDestino = new TestarInserirAsCategoriasNoDestino();
            testarGravarAsCategoriasNoDestino.Execute();   
        }

        [TestMethod]
        public void DeveInserirDuasCategorias()
        {            
            returnRows = contexto.Sql("Select * from categorias").Query();

            Assert.IsNotNull(returnRows);
            Assert.AreEqual(2, returnRows.Count);
        }
    }
}
