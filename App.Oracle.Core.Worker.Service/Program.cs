IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
    {
        options.ServiceName = "App Background Worker";
    })
    .ConfigureServices(services =>
    {
        services.AddHostedService<BackgroundTaskWorker>();
        services.AddSingleton<IBackgroundTaskEngine, BackgroundTaskEngine>();
        services.AddHttpClient<IBackgroundTasks, BackgroundTasks>();
    })
    .Build();

await host.RunAsync();
