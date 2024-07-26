using Fluxor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PlanityBlazor.BlazorApp;
using PlanityBlazor.BlazorApp.BeautySalonContext;
using PlanityBlazor.BlazorApp.BeautySalonContext.GetBeautySalonsQuery;
using PlanityBlazor.BlazorApp.Shared.Reactive;

namespace PlanityBlazorApp.BlazorAppTest;

public class BeautySalonsBeautySelectorTests
{
 
    private MyState _currentState;
    private BeautySalonsSelector InitStoreBoilerplate(MyState state)
    {
        _currentState = state;
        return new BeautySalonsSelector();
    }

    [Fact]
    public void InitialBeautySalonsViewModelShouldHaveAnySalons()
    {
        //arrange
        var beautySalonSelector = InitStoreBoilerplate(new MyState());
        
        //act
        var beautySalonsViewModel = beautySalonSelector.OnNext(_currentState);

        //assert
        ThenShouldHaveExpectedSalons(beautySalonsViewModel, new BeautySalonsViewModel());
    }

    [Fact]
    public void ShouldBeProgressTrueOnLoading()
    {
        //arrange
        var beautySalonSelector = InitStoreBoilerplate(new MyState()
        {
            Progress = true
        });
        
        //act
        var beautySalonsViewModel = beautySalonSelector.OnNext(_currentState);
        
        //assert
        ThenShouldHaveExpectedSalons(beautySalonsViewModel, new BeautySalonsViewModel() {InProgress = true});
    }

    [Fact]
    public void CompleteGetSalonsEffectShouldUpdateViewModel()
    {
        //arrange
        var beautySalonSelector = InitStoreBoilerplate(new MyState()
        {
            Salons = new List<string>()
            {
                "A beauty salon",
                "a second beauty salon"
            }
        });
        
        //act
        var beautySalonsViewModel = beautySalonSelector.OnNext(_currentState);

        //assert
        ThenShouldHaveExpectedSalons(beautySalonsViewModel, new BeautySalonsViewModel(){BeautySalons = ["A beauty salon", "a second beauty salon"]});
    }

    private void ThenShouldHaveExpectedSalons(BeautySalonsViewModel beautySalonsViewModel,
        BeautySalonsViewModel expectedViewModel)
    {
        beautySalonsViewModel.Should().BeEquivalentTo(expectedViewModel);
    }

    [Fact]
    public void WhenErrorShouldHaveItToTrue()
    {
        var beautySalonSelector = InitStoreBoilerplate(new MyState()
        {
            Salons = new List<string>()
            {
                "A beauty salon",
                "a second beauty salon"
            }
        });
        beautySalonSelector.OnNext(_currentState);

        //act
        var beautySalonsViewModel = beautySalonSelector.OnError(new Exception());
        
        //assert
        ThenShouldHaveExpectedSalons(beautySalonsViewModel, new BeautySalonsViewModel(){Error = true});
    }
}
