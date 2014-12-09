using Rhino.Etl.Core;
using Rhino.Etl.Core.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhinoETL_POC_Tests
{
    public class ResultsOperation : AbstractOperation
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
}
