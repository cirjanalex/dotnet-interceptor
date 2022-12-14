namespace GenericInterceptor.Services {

    public class DoStuff : IDoStuff, IDisposable {        
        public string DoWork (string input) {
            return $"Work done on {input}";
        }

        public void Dispose()
        {
            Console.WriteLine("Disposing the underlying instance");
        }
    }
}