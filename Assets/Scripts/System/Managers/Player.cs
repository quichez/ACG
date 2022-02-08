using System.Collections.Generic;
using UnityEngine;
using ACG.Resources;

public class Player : SingletonPersistent<Player>
{
    public int Money => GetTotalMoney();
    public int GlobalHappiness = 3;
    public int GlobalUnhappiness = 0;

    public int Luck { get; private set; }

    public List<Resource> outputResources { get; } = new();

    public int GetTotalMoney()
    {
        int total = 0;
        foreach (Settlement settlement in TurnManager.Instance.Settlements)
        {
            if (settlement is IInputResources inputs) total += inputs.FindInputResource(typeof(Money))?.Amount ?? 0;
        }
        return total;
    }  

    public int GetTotalHappiness()
    {
        int total = 0;
        foreach (var item in TurnManager.Instance.Settlements)
        {
            if(item is IHappinessSettlement happyCamper)
            {
                total += happyCamper.CalculateTotalLocalHappiness();
            }
        }
        return total;
    }

    public string[] GetTotalResourcesAsString()
    {
        string[] resourceTotal = new[] { "", "" };

        foreach (var item in outputResources)
        {
            resourceTotal[0] += item.Name + "\n";
            resourceTotal[1] += item.Amount + "\n";
        }

        return resourceTotal;
    }

    public void AddToOutputResources(Resource resource)
    {
        if(outputResources.GetResource(resource,out Resource result))
        {
            result.ChangeResourceAmount(resource.Amount);
        }
        else
        {
            outputResources.Add(resource);
        }
    }

    public void AddToOutputResources(List<Resource> resources)
    {
        foreach (var item in resources)
        {
            AddToOutputResources(item);
        }
    }

    public void RemoveFromOutputResources(Resource resource)
    {
        if(outputResources.GetResource(resource, out Resource result))
        {
            if(result.Amount - resource.Amount <= 0)
            {
                outputResources.Remove(result);
            }
            else
            {
                result.ChangeResourceAmount(-resource.Amount);
            }
        }
    }

    public void RemoveFromOutputResources(List<Resource> resources)
    {
        foreach (var item in resources)
        {
            RemoveFromOutputResources(item);
        }
    }
}