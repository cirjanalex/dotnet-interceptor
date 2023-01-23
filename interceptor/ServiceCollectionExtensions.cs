using Microsoft.Extensions.DependencyInjection;

namespace GenericInterceptor
{
    public static class ServiceCollectionExtensions
    {

        public static void AddRecordingInterceptor(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IInterceptorFactory, InterceptorFactory>();
            serviceCollection.AddSingleton<IInterceptorContextProvider, InterceptorContextProvider>();
        }

        public static void AddInboundInterceptedScoped<TInterface, TImplementation>(this IServiceCollection serviceCollection) 
            where TImplementation : class, TInterface
            where TInterface : class
                => AddInterceptedScoped<TInterface, TImplementation>(serviceCollection, InterceptorType.Inbound);

        public static void AddOutboundInterceptedScoped<TInterface, TImplementation>(this IServiceCollection serviceCollection) 
            where TImplementation : class, TInterface
            where TInterface : class
                => AddInterceptedScoped<TInterface, TImplementation>(serviceCollection, InterceptorType.Outbound);

        private static void AddInterceptedScoped<TInterface, TImplementation>(
            this IServiceCollection serviceCollection,
            InterceptorType interceptorType)
        where TImplementation : class, TInterface
        where TInterface : class
        {
            serviceCollection.AddScoped<TImplementation>();
            serviceCollection.AddScoped<TInterface>((serviceProvider) =>
            {
                var underlyingInstance = serviceProvider.GetService<TImplementation>();
                var interceptorFactory = serviceProvider.GetRequiredService<IInterceptorFactory>();
                var interceptor = interceptorFactory.CreateRecordingInterceptor<TInterface, TImplementation>(interceptorType, underlyingInstance);
                return (TInterface)(object)interceptor;
            });
        }
    }
}