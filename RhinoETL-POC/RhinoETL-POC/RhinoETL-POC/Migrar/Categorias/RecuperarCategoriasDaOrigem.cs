using Rhino.Etl.Core.ConventionOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhinoETL_POC.Migrar.Categorias
{
    public class RecuperarCategoriasDaOrigem : ConventionInputCommandOperation
    {
        public RecuperarCategoriasDaOrigem():base("Origem")
        {
            Timeout = 0;
            Command = @" SELECT [Category ID], [Category Name], Description FROM Categories ";
        }
    }
}
