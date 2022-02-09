using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ACG.Buildings;

[RequireComponent(typeof(IndustryPointsController))]
public class BuildingController : MonoBehaviour
{
    Settlement _settlement => GetComponent<Settlement>();
    IndustryPointsController _industryPointsController => GetComponent<IndustryPointsController>();
    List<Building> _buildings;

    public Building buildingInProcess { get; private set; }
    bool IsBuildingInProcess => buildingInProcess != null;
    public int collectedIndustry { get; private set; }

    private void Awake()
    {
        Debug.Log(_settlement.GetType());
        _buildings = BuildingFactory.GetBuildingTypes(_settlement.GetType());
    }

    private void Start()
    {
        TurnManager.Instance.SubscribeToTurnManager(CollectIndustry);
    }
    public List<Building> GetBuildingsNotUnlocked() => _buildings.FindAll(building => building.Unlocked == false && building.InProcessOfUnlock == false && _buildings.IndexOf(building) < 12);
    public List<Building> GetBuildingsNotUnlocked(bool unlockedStatus) => _buildings.FindAll(building => building.Unlocked == unlockedStatus && building.InProcessOfUnlock == false);
    public List<Building> GetBuildingsUnlocked() => _buildings.FindAll(building => building.Unlocked == true);

    public List<Building> GetBuildingInProcess() => _buildings.FindAll(building => building.InProcessOfUnlock == true);

    public void StartBuilding(Building building)
    {
        buildingInProcess = _buildings.Find(bd => bd.Name == building.Name);
        buildingInProcess.StartProcess();
        Debug.Log(buildingInProcess.Name);
    }

    public void CollectIndustry()
    {
        if (!IsBuildingInProcess)
        {
            collectedIndustry = 0;
        }
        else
        {
            collectedIndustry += _industryPointsController.CalculateIndustryPointsPerTurn(this);
            if (collectedIndustry > buildingInProcess.industryCost)
            {
                buildingInProcess.StopProcess();
                buildingInProcess = null;
            }
        }


    }
}
