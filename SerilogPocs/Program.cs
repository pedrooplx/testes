using Serilog;
using Serilog.Templates;
using System.Reflection;

class Program
{
    static void Main()
    {
        List<string> listaDeNomes = new List<string>();

        Type tipoNomes = typeof(Nomes);
        FieldInfo[] campos = tipoNomes.GetFields(BindingFlags.Public | BindingFlags.Static);

        foreach (FieldInfo campo in campos)
        {
            if (campo.FieldType == typeof(string))
            {
                string valor = (string)campo.GetValue(null);
                listaDeNomes.Add(valor);
            }
        }
    }
}

public static class Nomes
{
    public const string nome1 = "pedro";
    public const string nome2 = "joao";
}