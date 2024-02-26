using MediatR;

namespace MediatrMocking;

public class SimpleRequest :IRequest<SimpleResponse>
{
    public string Req { get;  }

    public SimpleRequest(string req)
    {
        Req = req;
    }
}
