using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatrMocking;
internal class SimpleCaller
{
    private readonly IMediator mediator;

    public SimpleCaller(IMediator mediator)
    {
        this.mediator = mediator;
    }
    public async Task<string> CallHandler(string s)
    {
        return (await mediator.Send(new SimpleRequest(s))).Res;
    }

    public async Task<string> CallDifferentHandler(string s)
    {
        return (await mediator.Send(new DifferentSimpleRequest(s))).Res;
    }


}
