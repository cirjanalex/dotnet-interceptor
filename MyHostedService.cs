using System.Diagnostics;
using GenericInterceptor.Services;
using Microsoft.Extensions.Hosting;

namespace GenericInterceptor {
    public class MyHostedService : IHostedService {
        private readonly IDoStuff iDoStuff;

        public MyHostedService (IDoStuff iDoStuff) {
            this.iDoStuff = iDoStuff;
        }
        Stopwatch stopwatch = new Stopwatch ();
        public async Task StartAsync (CancellationToken cancellationToken) {
            while (true) {
                if (cancellationToken.IsCancellationRequested) break;

                Console.WriteLine (iDoStuff.DoWork ("abcd"));                
                await Task.Delay (500);
            }
        }

        public Task StopAsync (CancellationToken cancellationToken) {
            return Task.CompletedTask;
        }
    }
}