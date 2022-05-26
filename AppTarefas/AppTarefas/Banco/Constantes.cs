using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AppTarefas.Banco
{
    public static class Constantes
    {
        public const string NomeDoArquivo = "TodoSQLite.db3";
        public static string CaminhoDoBanco
        {
            get
            {

                // Pasta onde o projeto está instalado
                var caminhoBase = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(caminhoBase, NomeDoArquivo);
            }
        }

    }
}
