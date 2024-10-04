using Fluxor;
using Microsoft.Extensions.DependencyInjection;
using PlanityBlazor.BlazorApp;
using PlanityBlazor.BlazorApp.BeautySalonContext;
using PlanityBlazor.BlazorApp.BeautySalonContext.CreateBeautySalon;
using PlanityBlazor.BlazorApp.BeautySalonContext.GetBeautySalonsQuery;

namespace PlanityBlazorApp.BlazorAppTest.CreateBeautySalon;

public class CreateABeautySalonTests : Fixture
{
    private CreateBeautySalonSelector InitStoreBoilerplate()
    {
        return new CreateBeautySalonSelector();
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
        InitStoreBoilerplate().OnNext(beautySalonState.Value).State.Should().Be(ViewModelState.Error);
    }

    [Fact]
    public void BeautySalonOnErrorStateShouldBeEmptyAfterCompleteAction()
    {
        var beautySalonState = ServiceProvider.GetRequiredService<IState<BeautySalonState>>();
        var sut = ServiceProvider.GetRequiredService<IDispatcher>();
        //set state in error
        CallSutDispatcher(sut, string.Empty);


        var aBeautySalon = "A beauty salon";
        CallSutDispatcher(sut, aBeautySalon);

        ExpectStateWithCorrectSalon(aBeautySalon, beautySalonState);
    }

    [Fact]
    public void ShouldBeLoading()
    {
        InitStoreBoilerplate().OnNext(new BeautySalonState()
        {
            Progress = true
        }).State.Should().Be(ViewModelState.Progress);
    }


    private void ExpectStateWithCorrectSalon(string aBeautySalon, IState<BeautySalonState> beautySalonState)
    {
        InitStoreBoilerplate().OnNext(beautySalonState.Value).State.Should().Be(ViewModelState.Completed);
        beautySalonState.Value.Salons.Should().ContainEquivalentOf(new BeautySalon(aBeautySalon));
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