using Rhino.Etl.Core.ConventionOperations;

namespace RhinoETL_POC.Migrar.CategoriasComAtualizacao
{
    public class RecuperarCategoriasDoDestino : ConventionInputCommandOperation
    {
        public RecuperarCategoriasDoDestino()
            : base("Destino")
        {
            Timeout = 0;
            Command = @" SELECT Id, Nome, Descricao FROM categorias ";
        }
    }
}
