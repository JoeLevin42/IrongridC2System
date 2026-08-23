

using IronGridConsumer.Data;
using IronGridConsumer.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

var bootstrapServers = configuration["Kafka:BootstrapServers"] ?? "localhost:9092";

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
