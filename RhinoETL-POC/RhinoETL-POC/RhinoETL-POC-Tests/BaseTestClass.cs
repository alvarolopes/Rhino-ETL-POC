using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.Etl.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Etl.Core.Operations;
using Rhino.Etl.Core.Pipelines;
using System.Data.SqlClient;
using System.Configuration;

namespace RhinoETL_POC_Tests
{
    public class BaseTestClass
    {
        protected void AreSameRows(IList<Row> expected, IList<Row> actual)
        {
            if (expected.Count != actual.Count)
                throw new AssertFailedException("Expected count of " + expected.Count + " not same as actual: " + actual.Count);

            for (int i = 0; i < expected.Count; i++)
                Assert.IsTrue(expected[i].Equals(actual[i]), expected[i].ToString() + " not equal to " + actual[i].ToString());
        }

        protected List<Row> TestOperation(params IOperation[] operations)
        {
            TestProcess process = new TestProcess(operations);
            List<Row> results = process.ExecuteWithResults();

            //re-throw first error if any
            var errors = new List<Exception>(process.GetAllErrors());
            if (errors.Count > 0)
                throw errors[0];

            return results;
        }

        protected class TestProcess : EtlProcess
        {
            List<Row> returnRows = new List<Row>();

            private class ResultsOperation : AbstractOperation
            {
                public ResultsOperation(List<Row> returnRows)
                {
                    this.returnRows = returnRows;
                }

                List<Row> returnRows = null;

                public override IEnumerable<Row> Execute(IEnumerable<Row> rows)
                {
                    returnRows.AddRange(rows);

                    return rows;
                }
            }

            public TestProcess(params IOperation[] testOperations)
            {
                this.testOperations = testOperations;
            }

            IEnumerable<IOperation> testOperations = null;

            protected override void Initialize()
            {
                foreach(var testOperation in testOperations)
                    Register(testOperation);

                Register(new ResultsOperation(returnRows));
            }

            public List<Row> ExecuteWithResults()
            {
                Execute();
                return returnRows;
            }
        }


    }
}
