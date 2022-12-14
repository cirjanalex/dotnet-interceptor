using GenericInterceptor;
using GenericInterceptor.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

bool interceptorOn = true;

var hostBuilder = Host.CreateDefaultBuilder (args)
    .ConfigureServices ((hostContext, services) => {            
        services.AddInterceptor();
        if(interceptorOn) {
            services.AddInterceptedSingleton<IDoStuff, DoStuff>();
        }
        else {
            services.AddSingleton<IDoStuff, DoStuff>();
        }
        
        services.AddSingleton<IHostedService, MyHostedService>();
    });

var application = hostBuilder.Build();
application.Run();