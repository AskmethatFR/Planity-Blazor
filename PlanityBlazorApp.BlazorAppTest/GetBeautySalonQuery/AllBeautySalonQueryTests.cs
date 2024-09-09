using PlanityBlazor.BlazorApp.BeautySalonContext.GetBeautySalonsQuery;

namespace PlanityBlazorApp.BlazorAppTest.GetBeautySalonQuery;

public class AllBeautySalonQueryTests
{

    [Fact]
    public async Task ShouldHaveNoBeautySalons()
    {
        var inMemoryBeautySalonGateway = GivenBeautySalonInGateway(new List<string>());
        
        var result = await WhenBeautySalonAreGet(inMemoryBeautySalonGateway);
        
        result.Should().BeEmpty();
    }
    
    [Fact]
    public async Task ShouldHaveExpectedBeautySalons()
    {
        var expectedBeautySalons = new List<string>
        {
            "BeautySalon1",
            "BeautySalon2",
            "BeautySalon3",
            "BeautySalon4",
        };
        
        var inMemoryBeautySalonGateway = GivenBeautySalonInGateway(expectedBeautySalons);

        var result = await WhenBeautySalonAreGet(inMemoryBeautySalonGateway);

        result.Should().BeEquivalentTo(expectedBeautySalons);
    }

    private Task<List<string>> WhenBeautySalonAreGet(InMemoryBeautySalonGateway inMemoryBeautySalonGateway)
    {
        var sut = new AllBeautySalonQuery(inMemoryBeautySalonGateway);
        var result = sut.Handle();
        return result;
    }

    private InMemoryBeautySalonGateway GivenBeautySalonInGateway(List<string> beautySalons)
    {
        var inMemoryBeautySalonGateway = new InMemoryBeautySalonGateway();
        inMemoryBeautySalonGateway.All = beautySalons;
        return inMemoryBeautySalonGateway;
    }
}