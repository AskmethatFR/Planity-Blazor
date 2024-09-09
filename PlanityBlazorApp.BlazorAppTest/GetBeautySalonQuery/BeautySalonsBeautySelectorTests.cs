using PlanityBlazor.BlazorApp;
using PlanityBlazor.BlazorApp.BeautySalonContext;

namespace PlanityBlazorApp.BlazorAppTest.GetBeautySalonQuery;

public class BeautySalonsBeautySelectorTests
{
 
    private BeautySalonState _currentState;
    private BeautySalonsSelector InitStoreBoilerplate(BeautySalonState state)
    {
        _currentState = state;
        return new BeautySalonsSelector();
    }

    [Fact]
    public void InitialBeautySalonsViewModelShouldHaveAnySalons()
    {
        //arrange
        var beautySalonSelector = InitStoreBoilerplate(new BeautySalonState());
        
        //act
        var beautySalonsViewModel = beautySalonSelector.OnNext(_currentState);

        //assert
        ThenShouldHaveExpectedSalons(beautySalonsViewModel, new BeautySalonsViewModel(){Status = ViewModelState.Nothing, NothingMessage = "Nothing here"});
    }

    [Fact]
    public void ShouldBeProgressTrueOnLoading()
    {
        //arrange
        var beautySalonSelector = InitStoreBoilerplate(new BeautySalonState()
        {
            Progress = true
        });
        
        //act
        var beautySalonsViewModel = beautySalonSelector.OnNext(_currentState);
        
        //assert
        ThenShouldHaveExpectedSalons(beautySalonsViewModel, new BeautySalonsViewModel() { Status = ViewModelState.Progress, LoadingMessage = "Loading..."});
    }

    [Fact]
    public void CompleteGetSalonsEffectShouldUpdateViewModel()
    {
        //arrange
        var beautySalonSelector = InitStoreBoilerplate(new BeautySalonState()
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
        ThenShouldHaveExpectedSalons(beautySalonsViewModel, new BeautySalonsViewModel(){BeautySalons = ["A beauty salon", "a second beauty salon"], Status = ViewModelState.Completed});
    }

    private void ThenShouldHaveExpectedSalons(BeautySalonsViewModel beautySalonsViewModel,
        BeautySalonsViewModel expectedViewModel)
    {
        beautySalonsViewModel.Should().BeEquivalentTo(expectedViewModel);
    }

    [Fact]
    public void WhenErrorShouldHaveItToTrue()
    {
        var beautySalonSelector = InitStoreBoilerplate(new BeautySalonState()
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
        ThenShouldHaveExpectedSalons(beautySalonsViewModel, new BeautySalonsViewModel(){ Status = ViewModelState.Error, ErrorMessage = "An error occured"});
    }
}
