using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace GenericInterceptor
{
    internal interface IInterceptorFactory
    {
        public IInterceptor CreateRecordingInterceptor<TInterface, TImplementation>(InterceptorType interceptorType, TImplementation interceptedInstance)
            where TImplementation : TInterface;
    }

    internal class InterceptorFactory : IInterceptorFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public InterceptorFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IInterceptor CreateRecordingInterceptor<TInterface, TImplementation>(InterceptorType interceptorType, TImplementation interceptedInstance)
            where TImplementation : TInterface
        {
            var interceptorContextProvider = _serviceProvider.GetService<IInterceptorContextProvider>();
            var logger = _serviceProvider.GetService<ILogger<RecordingInterceptor>>();
            var options = _serviceProvider.GetService<IOptions<InterceptorConfiguration>>();

            var interceptor = BuildRecordingInterceptor<TInterface, TImplementation>(interceptedInstance);
            interceptor.InjectDependencies(interceptorContextProvider, interceptorType, options, logger);
            return interceptor;
        }

        private RecordingInterceptor BuildRecordingInterceptor<TInterface, TImplementation>(TImplementation decorated)
            where TImplementation : TInterface
        {
            var proxy = typeof(DispatchProxy)
                .GetMethod("Create")
                .MakeGenericMethod(typeof(TInterface), typeof(RecordingInterceptor))
                .Invoke(null, Array.Empty<object>())
            as RecordingInterceptor;

            proxy._decorated = decorated;

            return proxy;
        }
    }
}
