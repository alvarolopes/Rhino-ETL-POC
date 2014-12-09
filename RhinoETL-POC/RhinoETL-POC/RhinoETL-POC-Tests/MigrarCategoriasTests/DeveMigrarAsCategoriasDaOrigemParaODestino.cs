using FluentData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Etl.Core;
using Rhino.Etl.Core.Operations;
using RhinoETL_POC.Migrar.Categorias;
using System.Collections.Generic;


namespace RhinoETL_POC_Tests.MigrarCategoriasTests
{
    [TestClass]
    public class DeveMigrarAsCategoriasDaOrigemParaODestino
    {
        private List<dynamic> returnRows = new List<dynamic>();
        private IDbContext contexto = new DbContext().ConnectionStringName("Destino", DbProviderTypes.SqlServerCompact40);

        [TestInitialize]
        public void Inicializar()
        {
            contexto.Sql("delete from categorias").Execute();

            var migrarCategorias = new MigrarCategorias();
            migrarCategorias.Execute();   
        }

        [TestMethod]
        public void DeveMigrarOitoCategorias()
        {
            returnRows = contexto.Sql("Select * from categorias").Query();

            Assert.IsNotNull(returnRows);
            Assert.AreEqual(8, returnRows.Count);
        }
    }
}
