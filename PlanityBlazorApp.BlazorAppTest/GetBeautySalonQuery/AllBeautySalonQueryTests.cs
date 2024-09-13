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
        var expectedBeautySalons = new List<BeautySalon>
        {
            new BeautySalon("BeautySalon1"),
            new BeautySalon("BeautySalon2"),
            new BeautySalon("BeautySalon3"),
            new BeautySalon("BeautySalon4")
        };

        var beautySalonsNames = expectedBeautySalons.Select(x => x.Name).ToList();
        var inMemoryBeautySalonGateway = GivenBeautySalonInGateway(beautySalonsNames);

        var result = await WhenBeautySalonAreGet(inMemoryBeautySalonGateway);

        result.Should().BeEquivalentTo(expectedBeautySalons);
    }

    private async Task<List<BeautySalon>> WhenBeautySalonAreGet(InMemoryBeautySalonGateway inMemoryBeautySalonGateway)
    {
        var sut = new AllBeautySalonQuery(inMemoryBeautySalonGateway);
        var result = await sut.Handle();
        return result;
    }

    private InMemoryBeautySalonGateway GivenBeautySalonInGateway(List<string> beautySalons)
    {
        var inMemoryBeautySalonGateway = new InMemoryBeautySalonGateway();
        inMemoryBeautySalonGateway.All = beautySalons;
        return inMemoryBeautySalonGateway;
    }
}