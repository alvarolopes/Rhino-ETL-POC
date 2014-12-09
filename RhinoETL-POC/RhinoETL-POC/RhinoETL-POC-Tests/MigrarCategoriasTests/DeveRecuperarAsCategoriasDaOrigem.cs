using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Etl.Core;
using System.Linq;
using RhinoETL_POC.Migrar.Categorias;
using Rhino.Etl.Core.Operations;
using System.Collections.Generic;
using FluentData;

namespace RhinoETL_POC_Tests.MigrarCategoriasTests
{
    [TestClass]
    public class DeveRecuperarAsCategoriasDaOrigem 
    {
        private static List<Row> returnRows = new List<Row>();

        public class TestarRecuperarCategoriasDaOrigem : EtlProcess
        {
            protected override void Initialize()
            {
                Register(new RecuperarCategoriasDaOrigem());
                Register(new ResultsOperation(returnRows));
            }
        }

        [TestInitialize]
        public void Inicializar()
        {
            returnRows.Clear();
            var testarRecuperarCategoriasDaOrigem = new TestarRecuperarCategoriasDaOrigem();
            testarRecuperarCategoriasDaOrigem.Execute();
        }

        [TestMethod]
        public void DeveRetornarOitoCategorias()
        {
            Assert.AreEqual(8, returnRows.Count);
        }

        [TestMethod]
        public void DeveRetornarOsCamposDasCategorias()
        {
            Assert.IsTrue(returnRows.All(o => o["Category ID"] != null));
            Assert.IsTrue(returnRows.All(o => o["Category Name"] != null));
            Assert.IsTrue(returnRows.All(o => o["Description"] != null));
        }
    }
}
