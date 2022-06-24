using BudiBu85.PizzaGame;
using BudiBu85.PizzaGame.Service;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<IPlayService, PlayService>();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
