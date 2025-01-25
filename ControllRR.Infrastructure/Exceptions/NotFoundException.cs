namespace ControllRR.Infrastructure.Exceptions;


public class NotFoundException: ApplicationException
{
    public NotFoundException(string message): base(message)
    {

    }
}