namespace resilientapi;

public class HelloService : IHelloService
{
    public string Hello(string message)
    {
        return $"Hello {message}";
    }
}
