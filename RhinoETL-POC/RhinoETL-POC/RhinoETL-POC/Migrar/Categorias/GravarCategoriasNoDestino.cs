using System.Data;
using Rhino.Etl.Core;
using Rhino.Etl.Core.ConventionOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhinoETL_POC.Migrar.Categorias
{
    public class GravarCategoriasNoDestino : ConventionOutputCommandOperation
    {
        public GravarCategoriasNoDestino():base("Destino")
        {            
        }

        protected override void PrepareCommand(IDbCommand cmd, Row row)
        {
            cmd.CommandText = "INSERT INTO categorias (Id, Nome, Descricao) VALUES(@Id, @Nome, @Descricao)";

            var Id = cmd.CreateParameter();
            Id.Value = row["Category ID"];
            Id.ParameterName = "Id";
            Id.DbType = DbType.Decimal;
            cmd.Parameters.Add(Id);           

            var Name = cmd.CreateParameter();
            Name.Value = row["Category Name"];
            Name.ParameterName = "Nome";
            Name.DbType = DbType.String;
            cmd.Parameters.Add(Name);           
 
            var Description = cmd.CreateParameter();
            Description.Value = row["Description"];
            Description.ParameterName = "Descricao";
            Description.DbType = DbType.String;
            cmd.Parameters.Add(Description);
        }
    }
}
