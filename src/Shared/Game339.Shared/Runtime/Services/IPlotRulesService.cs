namespace Game339.Shared.Services
{
    public interface IPlotRulesService
    {
        bool CanPlaceTower(
            bool plotOccupied,
            bool hasSelectedTower,
            bool correctPlacementType,
            bool canAfford
        );

        bool CanUpgradeTower(
            bool plotOccupied,
            bool hasSelectedTower,
            bool correctPlacementType,
            bool canAfford
        );

        bool CanSellTower(bool plotOccupied);
    }
}