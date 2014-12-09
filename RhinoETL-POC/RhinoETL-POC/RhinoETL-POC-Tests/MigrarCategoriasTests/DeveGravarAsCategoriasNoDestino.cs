using FluentData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Etl.Core;
using Rhino.Etl.Core.Operations;
using RhinoETL_POC.Migrar.Categorias;
using System.Collections.Generic;


namespace RhinoETL_POC_Tests.MigrarCategoriasTests
{
    [TestClass]
    public class DeveGravarAsCategoriasNoDestino
    {
        private List<dynamic> returnRows = new List<dynamic>();
        private IDbContext contexto = new DbContext().ConnectionStringName("Destino", DbProviderTypes.SqlServerCompact40);

        public class TestarGravarAsCategoriasNoDestino : EtlProcess
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
                var linha1 = new Row();
                linha1.Add("Category ID", 1);
                linha1.Add("Category Name", "Teste1");
                linha1.Add("Description","Categoria Teste 1");

                var linha2 = new Row();
                linha2.Add("Category ID", 2);
                linha2.Add("Category Name", "Teste2");
                linha2.Add("Description", "Categoria Teste 2");

                yield return linha1;
                yield return linha2;
            }
        }

        [TestInitialize]
        public void Inicializar()
        {
            contexto.Sql("delete from categorias").Execute();

            var testarGravarAsCategoriasNoDestino = new TestarGravarAsCategoriasNoDestino();
            testarGravarAsCategoriasNoDestino.Execute();   
        }

        [TestMethod]
        public void DeveGravarDuasCategorias()
        {
            
            returnRows = contexto.Sql("Select * from categorias").Query();

            Assert.IsNotNull(returnRows);
            Assert.AreEqual(2, returnRows.Count);
        }


    }
}
