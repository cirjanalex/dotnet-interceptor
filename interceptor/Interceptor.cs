using System.Reflection;

namespace GenericInterceptor {

    public interface IInterceptor {
    }

    public abstract class Interceptor : DispatchProxy, IInterceptor {
        internal object _decorated;

        public Interceptor () : base () { }

        protected override object Invoke (MethodInfo targetMethod, object[] args) {
            OnInvoking (targetMethod, args);
            
            try {
                var result = targetMethod.Invoke (_decorated, args);
                OnInvoked (targetMethod, args, result);
                return result;
            } catch (TargetInvocationException exc) {
                OnException (targetMethod, args, exc);
                throw exc.InnerException;
            }
        }

        protected abstract void OnException(MethodInfo methodInfo, object[] args, Exception exception);
        protected abstract void OnInvoked(MethodInfo methodInfo, object[] args, object result);
        protected abstract void OnInvoking(MethodInfo methodInfo, object[] args);        
    }
}