namespace ZumApi.BLL.Exceptions;

public class InvalidTagException : Exception
{
    public InvalidTagException(string message) : base(message) { }
}
