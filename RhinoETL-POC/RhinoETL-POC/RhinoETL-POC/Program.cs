using System;
using RhinoETL_POC.Migrar.Categorias;
using RhinoETL_POC.Migrar.CategoriasComAtualizacao;

namespace RhinoETL_POC
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Running ETL...");

            try
            {
                new MigrarCategorias().Execute();
                
                new MigrarCategoriasComAtualizacao().Execute();                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.WriteLine("Complete");
            Console.ReadKey();
        }
    }
}
