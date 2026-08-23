

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

var bootstrapServers = configuration["Kafka:BootstrapServers"] ?? "localhost:9092";

var services = new ServiceCollection(); //create the collection

//register here!!


var serviceProvider = services.BuildServiceProvider();