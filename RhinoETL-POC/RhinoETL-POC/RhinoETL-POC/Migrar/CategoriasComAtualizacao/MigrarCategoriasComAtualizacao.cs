using Rhino.Etl.Core;
using RhinoETL_POC.Migrar.Categorias;

namespace RhinoETL_POC.Migrar.CategoriasComAtualizacao
{
    public class MigrarCategoriasComAtualizacao : EtlProcess    
    {
        protected override void Initialize()
        {
            Register(new MesclarCategorias()
                            .Left(new RecuperarCategoriasDaOrigem())
                            .Right(new RecuperarCategoriasDoDestino()));

            Register(new GravarCategoriasNoDestinoComAtualizacao());
        }        
    }
}
