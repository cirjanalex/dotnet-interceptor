using System.Reflection;

namespace GenericInterceptor {

    public interface IInterceptor {
        TInterface BuildAndDecorate<TInterface, TImplementation> (TImplementation decorated) where TImplementation : TInterface;
    }

    public class Interceptor : DispatchProxy, IInterceptor, IDisposable {
        private object _decorated;

        public Interceptor () : base () { }

        public TInterface BuildAndDecorate<TInterface, TImplementation> (TImplementation decorated)
        where TImplementation : TInterface {
            var proxy = typeof (DispatchProxy)
                .GetMethod ("Create")
                .MakeGenericMethod (typeof (TInterface), GetType ())
                .Invoke (null, Array.Empty<object> ())
            as Interceptor;
            
            proxy._decorated = decorated;

            return (TInterface) (object) proxy;
        }

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

        protected virtual void OnException (MethodInfo methodInfo, object[] args, Exception exception) {
            Console.WriteLine ("Exception");
        }
        protected virtual void OnInvoked (MethodInfo methodInfo, object[] args, object result) {
            Console.WriteLine ($"Invoked {methodInfo.Name} with params {string.Join(",", args)} and result {result}");
        }
        protected virtual void OnInvoking (MethodInfo methodInfo, object[] args) {
            Console.WriteLine ("OnInvoking");
        }

        public void Dispose()
        {
            Console.WriteLine("Disposed the interceptor");
        }
    }
}