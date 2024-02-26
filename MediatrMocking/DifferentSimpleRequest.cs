using MediatR;

namespace MediatrMocking;

public class DifferentSimpleRequest : IRequest<SimpleResponse>
{
    public string Req { get; }

    public DifferentSimpleRequest(string req)
    {
        Req = req;
    }
}
