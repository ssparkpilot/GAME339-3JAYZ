using UnityEngine;
using UnityEngine.EventSystems;
using Game339.Shared.Services;
using Game339.Shared.Services.Implementation;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;

    [Header("Placement Colors")]
    [SerializeField] private Color validColor;
    [SerializeField] private Color invalidColor;
    [SerializeField] private Color unaffordableColor;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip placeSound;
    [SerializeField] private AudioClip digSound;
    [SerializeField] private AudioClip cantplaceSound;

    public GameObject towerObj;
    public Turret turret;

    private Color startColor;
    private IPlotRulesService plotRules;

    private void Awake()
    {
        plotRules = new PlotRulesService();
    }

    private void Start()
    {
        startColor = sr.color;
    }

    private void OnMouseDown()
{
    if (LevelManager.main.IsGameOver)
        return;

    if (EventSystem.current.IsPointerOverGameObject())
        return;

    bool hasSelection = BuildManager.main.GetSelectedTower() != null;
    bool isOccupied = towerObj != null;
    bool canAfford = LevelManager.main.CanAfford(
        BuildManager.main.GetSelectedTower()?.cost ?? int.MaxValue
    );
    bool correctPlacement = IsCorrectPlacementType();

    // shovel
    if (BuildManager.main.CurrentMode == BuildMode.Shovel)
    {
        if (!plotRules.CanSellTower(isOccupied))
            return;

        int refund = GetSellValue();
        LevelManager.main.IncreaseCurrency(refund);
        audioSource.PlayOneShot(digSound);

        Destroy(towerObj);
        towerObj = null;
        turret = null;

        BuildManager.main.ClearSelection();
        sr.color = startColor;
        return;
    }

    // upgrade
    if (plotRules.CanUpgradeTower(
            plotOccupied: isOccupied,
            hasSelectedTower: hasSelection,
            correctPlacementType: correctPlacement,
            canAfford: canAfford))
    {
        LevelManager.main.TrySpendCurrency(
            BuildManager.main.GetSelectedTower().cost
        );

        Destroy(towerObj);

        BuildManager.main.SetSelectedTower(
            BuildManager.main.SelectedTowerIndex + 1
        );

        towerObj = BuildManager.main.PlaceTower(transform.position);
        turret = towerObj.GetComponent<Turret>();

        audioSource.PlayOneShot(placeSound);
        sr.color = startColor;
        return;
    }

    // placement
    if (plotRules.CanPlaceTower(
            plotOccupied: isOccupied,
            hasSelectedTower: hasSelection,
            correctPlacementType: correctPlacement,
            canAfford: canAfford))
    {
        LevelManager.main.TrySpendCurrency(
            BuildManager.main.GetSelectedTower().cost
        );

        towerObj = BuildManager.main.PlaceTower(transform.position);
        turret = towerObj.GetComponent<Turret>();

        audioSource.PlayOneShot(placeSound);
        sr.color = startColor;
        return;
    }

    audioSource.PlayOneShot(cantplaceSound);
}

    private bool IsCorrectPlacementType()
    {
        if (towerObj == null)
            return true;

        return BuildManager.main.SelectedTowerIndex == turret.towerIndex
               && turret.towerIndex % 2 == 0;
    }
    
    private int GetSellValue()
    {
        Tower baseTower = BuildManager.main.GetTowerByIndex(turret.towerIndex);
        if (baseTower == null)
            return 0;

        return baseTower.cost / 2;
    }
    
    private void OnMouseEnter()
    {
        if (LevelManager.main.IsGameOver)
            return;

        // Shovel
        if (BuildManager.main.CurrentMode == BuildMode.Shovel)
        {
            sr.color = towerObj != null ? unaffordableColor : startColor;
            return;
        }

        bool hasSelection = BuildManager.main.GetSelectedTower() != null;
        bool canAfford = LevelManager.main.CanAfford(
            BuildManager.main.GetSelectedTower()?.cost ?? int.MaxValue
        );
        bool correctPlacement = IsCorrectPlacementType();

        if (!hasSelection)
        {
            sr.color = startColor;
            return;
        }

        if (!correctPlacement)
        {
            sr.color = invalidColor;
            return;
        }

        if (!canAfford)
        {
            sr.color = unaffordableColor;
            return;
        }

        sr.color = validColor;
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }
}