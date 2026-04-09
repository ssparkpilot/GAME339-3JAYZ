namespace Game339.Shared.Services.Implementation
{
    public class PlotRulesService : IPlotRulesService
    {
        public bool CanPlaceTower(
            bool plotOccupied,
            bool hasSelectedTower,
            bool correctPlacementType,
            bool canAfford)
        {
            return !plotOccupied
                   && hasSelectedTower
                   && correctPlacementType
                   && canAfford;
        }

        public bool CanUpgradeTower(
            bool plotOccupied,
            bool hasSelectedTower,
            bool correctPlacementType,
            bool canAfford)
        {
            return plotOccupied
                   && hasSelectedTower
                   && correctPlacementType
                   && canAfford;
        }

        public bool CanSellTower(bool plotOccupied)
        {
            return plotOccupied;
        }
    }
}