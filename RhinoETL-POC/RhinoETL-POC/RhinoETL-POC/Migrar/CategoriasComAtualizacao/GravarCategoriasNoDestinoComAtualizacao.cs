using System.Data;
using Rhino.Etl.Core;
using Rhino.Etl.Core.ConventionOperations;

namespace RhinoETL_POC.Migrar.CategoriasComAtualizacao
{
    public class GravarCategoriasNoDestinoComAtualizacao : ConventionOutputCommandOperation
    {
        public GravarCategoriasNoDestinoComAtualizacao()
            : base("Destino")
        {            
        }

        protected override void PrepareCommand(IDbCommand cmd, Row row)
        {
            if (row["ACAO"].ToString() == "INSERIR")
            {
                cmd.CommandText = "INSERT INTO categorias (Id, Nome, Descricao) VALUES(@Id, @Nome, @Descricao)";    
            }

            if (row["ACAO"].ToString() == "ALTERAR")
            {
                cmd.CommandText = "UPDATE categorias SET Nome = @Nome, Descricao=@Descricao where Id=@Id";    
            }

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
