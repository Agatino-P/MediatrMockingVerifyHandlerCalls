using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatrMocking;
public class SimpleHandler : IRequestHandler<SimpleRequest,SimpleResponse>
{
    public async Task<SimpleResponse> Handle(SimpleRequest request, CancellationToken cancellationToken)
    {
        return new SimpleResponse(request.Req.ToUpper());
    }
}

