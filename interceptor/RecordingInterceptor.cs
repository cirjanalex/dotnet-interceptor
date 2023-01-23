using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GenericInterceptor
{
    public class RecordingInterceptor : Interceptor
    {
        private IInterceptorContextProvider _interceptorContextProvider;
        private InterceptorType _interceptorType;
        private InterceptorConfiguration _configuration;
        private ILogger<RecordingInterceptor> _logger;

        public RecordingInterceptor() { }

        public void InjectDependencies(
            IInterceptorContextProvider interceptorContextProvider, 
            InterceptorType interceptorType,
            IOptions<InterceptorConfiguration> options,
            ILogger<RecordingInterceptor> logger)
        {
            _interceptorContextProvider = interceptorContextProvider;
            _interceptorType = interceptorType;
            _configuration = options.Value;
            _logger = logger;
        }

        protected override void OnInvoking(MethodInfo methodInfo, object[] args)
        {
            if (_interceptorType == InterceptorType.Inbound)
            {
                _interceptorContextProvider.CreateContext();
            }
        }

        protected override void OnInvoked(MethodInfo methodInfo, object[] args, object result)
        {
            _logger.LogInformation($"Invoked {methodInfo.Name} on {_decorated.GetType().Name} with params [{string.Join(",", args)}], type {_interceptorType}, mode: {_configuration.Mode}");
        }

        protected override void OnException(MethodInfo methodInfo, object[] args, Exception exception)
        {
            _logger.LogInformation($"Invoked {methodInfo.Name} with exception {exception} {_configuration.Mode}");
        }
    }
    public enum InterceptorType
    {
        Inbound,
        Outbound
    }

    public interface IInterceptorContextProvider
    {
        public Guid ContextId { get; }

        public void CreateContext();
    }

    public class InterceptorContextProvider : IInterceptorContextProvider 
    {        
        private AsyncLocal<Guid> _guid;
        
        public Guid ContextId => _guid.Value;

        public void CreateContext()
        {            
            _guid = new AsyncLocal<Guid>();
            _guid.Value = Guid.NewGuid();        
        }
    }
}
