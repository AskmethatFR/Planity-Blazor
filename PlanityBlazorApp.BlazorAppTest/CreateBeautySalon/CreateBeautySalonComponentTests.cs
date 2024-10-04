using AngleSharp.Dom;
using Bunit;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using PlanityBlazor.BlazorApp;
using PlanityBlazor.BlazorApp.BeautySalonContext;
using PlanityBlazor.BlazorApp.BeautySalonContext.GetBeautySalonsQuery;
using PlanityBlazor.BlazorApp.Components;

namespace PlanityBlazorApp.BlazorAppTest.CreateBeautySalon;

public class CreateBeautySalonComponentTests : FixtureBunit
{
    [Fact]
    public void ShouldHaveBeautySalonNameInputWithCorrectUserSetValue()
    {
        var cut = RenderComponent<CreateBeautySalonComponent>(option =>
            option.Add(a => a.CreateBeautySalonViewModel, new CreateBeautySalonViewModel()));

        var input = FindAndUpdateBeautySalonInput(cut);

        input.Should().NotBeNull();
        input.OuterHtml.Should().Contain("Beauty Salon Name");
    }

    [Fact]
    public void ShouldUpdateStoreValueOnButtonClick()
    {
        var cut = RenderComponent<CreateBeautySalonComponent>(option =>
            option.Add(a => a.CreateBeautySalonViewModel, new CreateBeautySalonViewModel()));

        FindAndUpdateBeautySalonInput(cut);

        var button = cut.Find("#createBeautySalonButton");
        button.Click();

        AssertValueOnBeautySalonTest();
        var navigationManager = Services.GetRequiredService<NavigationManager>();
        navigationManager.Uri.Should().Be("http://localhost/BeautySalons");
    }


    [Fact]
    public void ShouldShowErrorWhenBeautySalonNameIsEmpty()
    {
        var cut = RenderComponent<CreateBeautySalonComponent>(option =>
            option.Add(a => a.CreateBeautySalonViewModel, new CreateBeautySalonViewModel()
            {
                State = ViewModelState.Error,
                Message = "Beauty Salon Name is required"
            }));

        var error = cut.Find("#beauty-salon-name-error");
        error.Should().NotBeNull();
        error.TextContent.Should().Be("Beauty Salon Name is required");
    }

    [Fact]
    public void ShouldBeLoadingInButton()
    {
        var cut = RenderComponent<CreateBeautySalonComponent>(option =>
            option.Add(a => a.CreateBeautySalonViewModel, new CreateBeautySalonViewModel()
            {
                State = ViewModelState.Progress,
                Message = "Loading..."
            }));

        var error = cut.Find("#createBeautySalonButton");
        error.TextContent.Should().Be("Loading...");
    }

    private void AssertValueOnBeautySalonTest()
    {
        var state = Services.GetRequiredService<IState<BeautySalonState>>();
        state.Value.Salons.Should().ContainEquivalentOf(new BeautySalon("Beauty Salon Name"));
    }

    private IElement FindAndUpdateBeautySalonInput(IRenderedComponent<CreateBeautySalonComponent> cut)
    {
        var input = cut.Find("#beautySalonNameInput");
        input.Change("Beauty Salon Name");

        return input;
    }
}