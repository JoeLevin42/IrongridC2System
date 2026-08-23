
using System.Text.Json;

namespace IrodgridProducer.Services;


public class JsonLoaderService
{
    public List<object>? LoadFromJson(string filePath)
    {
        try
        {
            var rawList = File.ReadAllText(filePath);

            var objList = JsonSerializer.Deserialize<List<object>>(rawList);

            return objList;
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine(ex.ToString());
            return null;
        }
        catch (JsonException ex)
        {
            Console.WriteLine(ex.ToString());
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return null;
        }
    }
}