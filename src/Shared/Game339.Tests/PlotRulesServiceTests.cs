using Game339.Shared.Services;
using Game339.Shared.Services.Implementation;

namespace Game339.Tests;

public class PlotRulesServiceTests
{
    private IPlotRulesService _svc;

    [SetUp]
    public void Setup()
    {
        _svc = new PlotRulesService();
    }

    [Test]
    public void CanPlaceTower_Returns_True_When_All_Conditions_Are_Met()
    {
        var result = _svc.CanPlaceTower(
            plotOccupied: false,
            hasSelectedTower: true,
            correctPlacementType: true,
            canAfford: true
        );

        Assert.That(result, Is.True);
    }

    [Test]
    public void CanPlaceTower_Returns_False_When_Plot_Is_Occupied()
    {
        var result = _svc.CanPlaceTower(
            plotOccupied: true,
            hasSelectedTower: true,
            correctPlacementType: true,
            canAfford: true
        );

        Assert.That(result, Is.False);
    }

    [Test]
    public void CanUpgradeTower_Returns_True_When_All_Conditions_Are_Met()
    {
        var result = _svc.CanUpgradeTower(
            plotOccupied: true,
            hasSelectedTower: true,
            correctPlacementType: true,
            canAfford: true
        );

        Assert.That(result, Is.True);
    }

    [Test]
    public void CanSellTower_Returns_True_When_Plot_Has_Tower()
    {
        var result = _svc.CanSellTower(plotOccupied: true);
        Assert.That(result, Is.True);
    }

    [Test]
    public void CanSellTower_Returns_False_When_Empty()
    {
        var result = _svc.CanSellTower(plotOccupied: false);
        Assert.That(result, Is.False);
    }
}