using IrodgridProducer.Services;

using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

var bootstrapServers = configuration["Kafka:BootstrapServers"] ?? "localhost:9092";

var dataPath = Path.Combine(AppContext.BaseDirectory,"Data", "field_reports.json");

var uavTopic = configuration["Kafka:uav"] ?? "uav";
var perimeterSensorTopic = configuration["Kafka:perimeterSensor"] ?? "perimeterSensor";

var jsonLoader = new JsonLoaderService();
var typeSorter = new TypeSorterService();
var validateGenerator = new ValidateGenerator();
var producer = new ProducerService(bootstrapServers);

var rawData = jsonLoader.LoadFromJson(dataPath);

var sortedUav = typeSorter.FilterToUav(rawData);
var sortedPerimeter = typeSorter.FilterToPerimeterSensor(rawData);

var finalUavList = validateGenerator.UAVHandler(sortedUav);
var finalsortedPerimeterList = validateGenerator.PermiterSensorHandler(sortedPerimeter);


foreach (var u in finalUavList)
{
    await producer.ProduceGeneric(u, uavTopic);
}
   
Console.WriteLine("END Produce uav");

foreach (var p in finalsortedPerimeterList)
{
    await producer.ProduceGeneric(p, perimeterSensorTopic);
}

Console.WriteLine("END Produce PerimeterSensor");

producer.Dispose(); //clean 