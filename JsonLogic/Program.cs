using JsonLogic;
using JsonLogic.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Concurrent;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        var i = 0;
        while (i <= 5)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            var operation = new Operacao().RetornaOperacao();
            string mapeamento = JsonLogicExtensions.MapearCoreografia(operation);

            stopwatch.Stop();

            Console.WriteLine(mapeamento);
            Console.WriteLine(stopwatch.ElapsedMilliseconds + " ms");
            i++;
        }
        Console.ReadKey();
    }
}

public static class JsonLogicExtensions
{
    public static string MapearCoreografia(Operacao jsonObject)
    {
        var mapResult = new ConcurrentDictionary<string, dynamic>();
        Parallel.Invoke(
            () => MapAndAddToResponse(JObject.FromObject(jsonObject.Header), mapResult),
            () => MapAndAddToResponse(JObject.FromObject(jsonObject.Payload), mapResult)
        );

        return JsonConvert.SerializeObject(mapResult, Formatting.Indented);
    }

    public static void MapAndAddToResponse(JObject obj, ConcurrentDictionary<string, dynamic> response)
    {
        foreach (var kvp in Map(obj))
            response[kvp.Key] = kvp.Value;
    }

    private static IDictionary<string, dynamic> Map(JObject obj)
    {
        var mappedObject = new Dictionary<string, dynamic>();
        var engine = new JsonLogicEvaluator(EvaluateOperators.Default);

        var jsonLogicRuleObject = JObject.Parse("{ \"var\": \"value\" }");

        if (obj != null)
        {
            foreach (var property in obj)
            {
                jsonLogicRuleObject["var"] = property.Key;
                var value = engine.Apply(jsonLogicRuleObject, obj);
                mappedObject[property.Key] = value;
            }
        }

        return mappedObject;
    }

    //string jsonLogicRule = @"
    //{
    //    ""id"": { ""var"": ""Header.Id"" },
    //    ""produto"": { ""var"": ""Header.Produto"" }
    //}";
}