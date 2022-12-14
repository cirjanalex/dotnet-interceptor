# Generic Interceptor for .NET applications

When you need to capture the request and response of a certain class you can use the **Interceptor** class to wrap an instance and capture all the calls to the methods an properties of that class.  
This can be usefull in scenarios where you want to record the behaviour of an application and replay it later. Rather than creating manual interceptor classes for all the components that communicate with outside services you can use the **Interceptor**.

In the code you can see the usage in a typical .NET application that uses the application builder pattern.  
In production code you don't want to leave the interceptor activated, so having a setting that allows you to turn it on and off is recommended.  