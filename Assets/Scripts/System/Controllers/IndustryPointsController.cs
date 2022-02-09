using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ACG.Resources;

[RequireComponent(typeof(Village))]
public class IndustryPointsController : MonoBehaviour
{

    Village _village => GetComponent<Village>();
    BuildingController _buildingController => GetComponent<BuildingController>();

    public int LocalIndustryPointsPerTurn { get; private set; } = 0;

    public int CalculateIndustryPointsPerTurn(BuildingController bc)
    {
        int ip = 0;

        foreach (var item in _village.OutputResource)
        {
            if(item is IIndustryResource)
            {
                ip += item.Amount;                               
            }

            if (bc.GetBuildingsUnlocked().Find(bd => bd.Name == "Woodshop") != null)
            {
                ip += _village.OutputResource.GetResourceAmount(typeof(Lumber));
            }

            if (bc.GetBuildingsUnlocked().Find(bd => bd.Name == "Farm") != null)
            {
                ip += _village.OutputResource.GetResourceAmount(typeof(Cows));
                ip += _village.OutputResource.GetResourceAmount(typeof(Chickens));
            }
        }

        return ip;
    }
}
