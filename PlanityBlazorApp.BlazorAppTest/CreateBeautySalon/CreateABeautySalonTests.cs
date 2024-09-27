using Fluxor;
using Microsoft.Extensions.DependencyInjection;
using PlanityBlazor.BlazorApp.BeautySalonContext;
using PlanityBlazor.BlazorApp.BeautySalonContext.CreateBeautySalon;
using PlanityBlazor.BlazorApp.BeautySalonContext.GetBeautySalonsQuery;

namespace PlanityBlazorApp.BlazorAppTest.CreateBeautySalon;

public class CreateABeautySalonTests : Fixture
{
    private IsBeautySalonsCreationError InitStoreBoilerplate()
    {
        return new IsBeautySalonsCreationError();
    }

    [Fact]
    public void ShouldCreateABeautySalon()
    {
        var sut = PrepareSut();

        var expectedBeautySalon = "A beauty salon";
        CallSutDispatcher(sut, expectedBeautySalon);

        ExpectStateWithCorrectSalon(expectedBeautySalon,
            ServiceProvider.GetRequiredService<IState<BeautySalonState>>());
    }

    [Fact]
    public void ShouldNotCreateABeautySalon()
    {
        var sut = PrepareSut();

        var beautySalonGateway =
            (ServiceProvider.GetRequiredService<IBeautySalonGateway>() as InMemoryBeautySalonGateway)!;
        beautySalonGateway.PostReturnsError = true;

        var expectedBeautySalon = "A beauty salon";
        CallSutDispatcher(sut, expectedBeautySalon);

        var beautySalonState = ServiceProvider.GetRequiredService<IState<BeautySalonState>>();
        beautySalonState.Value.Salons.Should().NotContain(new BeautySalon(expectedBeautySalon));
    }


    [Fact]
    public void BeautySalonNotValid()
    {
        var sut = PrepareSut();

        CallSutDispatcher(sut, string.Empty);

        var beautySalonState = ServiceProvider.GetRequiredService<IState<BeautySalonState>>();
        InitStoreBoilerplate().OnNext(beautySalonState.Value).IsError.Should().BeTrue();
    }

    [Fact]
    public void BeautySalonOnErrorStateShouldBeEmptyAfterCompleteAction()
    {
        var beautySalonState = ServiceProvider.GetRequiredService<IState<BeautySalonState>>();
        beautySalonState.Value.Error = "an error";

        var sut = ServiceProvider.GetRequiredService<IDispatcher>();

        var aBeautySalon = "A beauty salon";
        CallSutDispatcher(sut, aBeautySalon);

        ExpectStateWithCorrectSalon(aBeautySalon, beautySalonState);
    }


    private void ExpectStateWithCorrectSalon(string aBeautySalon, IState<BeautySalonState> beautySalonState)
    {
        beautySalonState.Value.Salons.Should().ContainEquivalentOf(new BeautySalon(aBeautySalon));
        InitStoreBoilerplate().OnNext(beautySalonState.Value).IsError.Should().BeFalse();
    }

    private void CallSutDispatcher(IDispatcher sut, string expectedBeautySalon)
    {
        sut.Dispatch(new CreateABeautySalonAction(expectedBeautySalon));
    }

    private IDispatcher PrepareSut()
    {
        return ServiceProvider.GetRequiredService<IDispatcher>();
    }
}