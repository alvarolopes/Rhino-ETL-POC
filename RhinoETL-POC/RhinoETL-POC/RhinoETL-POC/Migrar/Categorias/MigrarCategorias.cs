using Rhino.Etl.Core;

namespace RhinoETL_POC.Migrar.Categorias
{
    public class MigrarCategorias : EtlProcess    
    {
        protected override void Initialize()
        {
            Register(new RecuperarCategoriasDaOrigem());
            Register(new GravarCategoriasNoDestino());
        }        
    }
}
