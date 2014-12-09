using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Etl.Core;
using Rhino.Etl.Core.Operations;

namespace RhinoETL_POC.Migrar.CategoriasComAtualizacao
{
    public class MesclarCategorias : JoinOperation
    {
        protected override void SetupJoinConditions()
        {
            LeftJoin
                .Left("Category ID")
                .Right("Id");
        }

        protected override Row MergeRows(Row leftRow, Row rightRow)
        {
            var row = new Row();

            row.Copy(leftRow);

            row["ACAO"] = rightRow.Columns.Any() ? "ALTERAR" : "INSERIR";

            return row;
        }
    }
}
