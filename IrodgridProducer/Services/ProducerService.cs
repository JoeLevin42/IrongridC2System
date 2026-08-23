
using Confluent.Kafka;
using System.Text.Json;
using static Confluent.Kafka.ConfigPropertyNames;

namespace IrodgridProducer.Services;

public class ProducerService
{
    private readonly IProducer<string, string> _producer; //?string, string or Null, string?

    public ProducerService(string bootstrapServers)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = bootstrapServers
        };

        _producer = new ProducerBuilder<string, string>(config).Build();
    }


    public async Task<bool> ProduceGeneric<T>(T obj , string topicName) 
    {
        try
        {
            var json = JsonSerializer.Serialize(obj);

            if (json == null)
            {
                return false;
            }

            var msg = new Message<string, string>
            {
                Value = json
            };

           await _producer.ProduceAsync(topicName, msg);
            return true;
        }
        catch (JsonException ex)
        {
            Console.WriteLine(ex.ToString());
            return false;
        }
    }
}