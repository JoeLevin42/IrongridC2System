

using Confluent.Kafka;
using IronGridConsumer.Data;
using IronGridConsumer.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

var bootstrapServers = configuration["Kafka:BootstrapServers"] ?? "localhost:9092";
var groupId = configuration["Kafka:GroupId"] ?? "some-group22";
var services = new ServiceCollection(); //create the collection

//register here!!
var conString = configuration.GetConnectionString("DefaultConnection"); //need to check this if works

services.AddDbContext<ApplicationDbContext>(
            dbContextOptions => dbContextOptions
                .UseMySql(conString, ServerVersion.AutoDetect(conString)));

services.AddScoped<Proccessor>();

var serviceProvider = services.BuildServiceProvider(); // this is the creation

//using (var scoped = serviceProvider.CreateScope())
//{
//    var db = scoped.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//    db.Database.EnsureCreated();
//} // this is needed??????


//====== until here the DI

//now kafka

var config = new ConsumerConfig
{
    BootstrapServers = bootstrapServers,
    GroupId = groupId,
    AutoOffsetReset = AutoOffsetReset.Earliest 
    //we will do auto-commit
};

using var consumer = new ConsumerBuilder<Ignore, string>(config).Build() ;
//this is the consumer config and build
//now start the while loop to recive data

//string[] topics = configuration["Kafka:Topics"] ?? new["uav","perimeterSensor";
string[] topics = { "uav", "perimeterSensor" };
consumer.Subscribe(topics);

while (true)
{
    var result = consumer.Consume();

    if (result?.Message?.Value == null)
    {
        continue;
    }

    //mybe more checks??? TODO!

    using (var scope = serviceProvider.CreateScope())
    {
        var proccessor = scope.ServiceProvider.GetRequiredService<Proccessor>();
        var res = await proccessor.ProccessAssetLiveStatus(result.Message.Value);
        if (res) { Console.WriteLine($"Proccessed to DB {result.Message.Value}"); }
        //TODO !!//we will want late to do the enablueautocommit = false , and commit only if true!
        else { Console.WriteLine("Something failed"); }
    }

    consumer.Commit(result);
}
    

