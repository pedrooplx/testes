using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonLogic
{
    public static class IteratorProduto
    {
        static List<string> produtos = new List<string>
        {
            "FXSPOT",
            "NDFONSHORE",
            "PTAX"
        };

        public static string ObterProdutoAleatorio()
        {
            // Inicialize o gerador de números aleatórios
            Random rand = new Random();

            // Gere um índice aleatório dentro do intervalo da lista
            int indiceAleatorio = rand.Next(0, produtos.Count);

            // Retorne a string correspondente ao índice aleatório gerado
            return produtos[indiceAleatorio];
        }
    }
}
