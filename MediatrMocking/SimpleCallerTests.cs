using MediatR;
using Moq;

namespace MediatrMocking;

public class SimpleCallerTests
{
    readonly Mock<IMediator> mediatorMock = new();

    readonly SimpleCaller sut;
    public SimpleCallerTests()
    {
        sut = new SimpleCaller(mediatorMock.Object);
        mediatorMock.Invocations.Clear();
    }
    
    [Fact]
    public async Task ShouldCallHandlerWithSimpleRequest()
    {
        mediatorMock.Setup(x => x.Send(It.IsAny<SimpleRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new SimpleResponse("RES"))
            .Verifiable("Notification was not sent."); 

        string actual=await sut.CallHandler("one");
        Assert.Equal("RES", actual);
        VerifyTemplated<SimpleRequest>(Times.Once);
        VerifyTemplated<DifferentSimpleRequest>(Times.Never);
        VerifyFullyTemplated<SimpleRequest,SimpleResponse>(Times.Once);
        VerifyFullyTemplated<DifferentSimpleRequest, SimpleResponse>(Times.Never);

    }

    [Fact]
    public async Task ShouldCallHandlerWithDifferentSimpleRequest()
    {
        mediatorMock.Setup(x => x.Send(It.IsAny<DifferentSimpleRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new SimpleResponse("RES"))
            .Verifiable("Notification was not sent.");

        string actual = await sut.CallDifferentHandler("one");
        Assert.Equal("RES", actual);
        VerifyTemplated<SimpleRequest>(Times.Never);
        VerifyTemplated<DifferentSimpleRequest>(Times.Once);
        VerifyFullyTemplated<SimpleRequest, SimpleResponse>(Times.Never);
        VerifyFullyTemplated<DifferentSimpleRequest, SimpleResponse>(Times.Once);
    }

    [Fact]
    public async Task ShouldFail()
    {
        mediatorMock.Setup(x => x.Send(It.IsAny<DifferentSimpleRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new SimpleResponse("RES"))
            .Verifiable("Notification was not sent.");

        string actual = await sut.CallDifferentHandler("one");
        Assert.Equal("RES", actual);
        VerifyTemplated<SimpleRequest>(Times.Once);
    }

    [Fact]
    public async Task ShouldFailFullyTemplated ()
    {
        mediatorMock.Setup(x => x.Send(It.IsAny<DifferentSimpleRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new SimpleResponse("RES"))
            .Verifiable("Notification was not sent.");

        string actual = await sut.CallDifferentHandler("one");
        Assert.Equal("RES", actual);
        VerifyFullyTemplated<SimpleRequest,SimpleResponse>(Times.Once);

    }


    private void VerifyTemplated<T>(Func<Times> howManyTimes)  where T : IRequest<SimpleResponse> 
    {
        mediatorMock.Verify(x => x.Send<SimpleResponse>(It.IsAny<T>(), It.IsAny<CancellationToken>()), howManyTimes);
    }

    private void VerifyFullyTemplated<TRequest, TResponse>(Func<Times> howManyTimes) where TRequest : IRequest<TResponse>
    {
        mediatorMock.Verify(x => x.Send<TResponse>(It.IsAny<TRequest>(), It.IsAny<CancellationToken>()), howManyTimes);
    }

}