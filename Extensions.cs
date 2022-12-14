using Microsoft.Extensions.DependencyInjection;

namespace GenericInterceptor {
    public static class ServiceCollectionExtensions {
        public static void AddInterceptor (this IServiceCollection serviceCollection) {
            serviceCollection.AddSingleton<IInterceptor, Interceptor> ();
        }

        public static void AddInterceptedSingleton<TInterface, TImplementation> (this IServiceCollection serviceCollection)
        where TImplementation : class, TInterface
        where TInterface : class {
            serviceCollection.AddSingleton<TImplementation> ();
            serviceCollection.AddSingleton<TInterface> ((serviceProvider) => {
                var underlyingInstance = serviceProvider.GetService<TImplementation> ();
                var interceptor = serviceProvider.GetRequiredService<IInterceptor> ();
                return interceptor.BuildAndDecorate<TInterface, TImplementation> (underlyingInstance);
            });
        }
    }
}