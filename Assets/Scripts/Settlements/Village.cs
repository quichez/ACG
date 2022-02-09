using ACG.Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Village : Settlement, IPopulationChange, IInputResources, IOutputResources, ILinkableSettlement, IHappinessSettlement
{
    public int MaximumLinkableDistance { get; private set; } = 3;

    public List<Resource> InputResources { get; private set; } = new();

    public List<Resource> OutputResource { get; private set; } = new();

    public LinkedList<ILinkableSettlement> LinkedSettlements { get; } = new();

    public List<SettlementLink> SettlementLinks { get; private set; } = new();

    public int LocalHappiness { get; private set; } = 3;

    public int LocalUnhappiness { get; private set; } = 0;

    public int LocalFoodPoints { get; private set; } = 0;    

    public bool IsSingleLinkable => false;

    public void ChangePopulationByAmount(int amt)
    {
        Population += amt;
    }

    public List<ILinkableSettlement> FindLinkableSettlements()
    {
        List<ILinkableSettlement> linkableSettlements = new();

        Collider[] linkablesInRange = Physics.OverlapBox(transform.position, Vector3.one * MaximumLinkableDistance, Quaternion.identity, TestSettlementSelector.Instance.SettlementMask);
        foreach (var cell in linkablesInRange)
        {
            if (cell.TryGetComponent(out ILinkableSettlement link))
            {
                if (!IsLinkedToSettlement(link) && cell.gameObject != gameObject)
                {
                    linkableSettlements.Add(link);
                }
            }
        }
        return linkableSettlements;
    }

    /*private SettlementLink FindLinkableSettlements_2()
    {
        Collider[] linkablesInRange = Physics.OverlapBox(transform.position, Vector3.one * MaximumLinkableDistance, Quaternion.identity, TestSettlementSelector.Instance.SettlementMask);
        foreach (var cell in linkablesInRange)
        {
            if (cell.TryGetComponent(out ILinkableSettlement link))
            {
                if (!LinkedSettlements.Contains(link) && cell.gameObject != gameObject)
                {
                    linkableSettlements.Add(link);
                }
            }
        }

    }*/

    public void LinkSettlementTo(SettlementLink newLink)
    {
        SettlementLinks.Add(newLink);
 
        if (newLink.Target is IOutputResources outputs)
        {
            foreach (var resource in outputs.OutputResource)
            {
                Player.Instance.AddToOutputResources(resource);
            }
        }
    }

    public void UnlinkSettlement()
    {
        foreach (var item in TurnManager.Instance.Settlements)
        {
            if (item is ILinkableSettlement linkable)
            {
                linkable.SettlementLinks.Remove(linkable.SettlementLinks.Find(x => (Object)x.Target == this));
            }
        }
    }

    protected override void Start()
    {
        base.Start();
        Name = Random.value.ToString();
        var newMoney = new Money(10);
        (newMoney as IRenewableResource).ChangeRenewalAmountByAmount(1);
        InputResources.Add(newMoney);

        TurnManager.Instance.SubscribeToTurnManager(OnTurnAction);

    }

    public override void SetCellLocation(Cell cell)
    {
        base.SetCellLocation(cell);
        if (cell is ICellResources resources)
        {
            OutputResource.AddRange(resources.CellResources);
            Player.Instance.AddToOutputResources(resources.CellResources);
        }

        foreach (IExpenseResource expense in OutputResource)
        {
            expense.SetCostToAmount(0); // This makes them equal ZERO on start -- that means building a Village on the resource makes it free.
        }
    }

    public void SetEffectiveResourceAmount(Resource res, bool status)
    {
        if (!status) res.SetEffectiveAmount(res.Amount);
        else res.SetEffectiveAmount(res.Amount / 2);
    }

    public void SetAllEffectiveResourceAmounts()
    {
        foreach (Resource outRes in OutputResource) // Loop through again to set all 
        {
            if (outRes is IExpenseResource expense)
            {
                Resource res = InputResources.Find(res => res.GetType() == expense.ResourceRequired);
                SetEffectiveResourceAmount(outRes, res.Amount <= 0);
            }
        }
    }

    public void CalculateAndSpendOnExpenseResources()
    {
        foreach (Resource temp in OutputResource) // Loop through each resource to calculate resource expenses
        {
            if (temp is IExpenseResource expense)
            {
                Resource res = InputResources.Find(res => res.GetType() == expense.ResourceRequired);
                res.ChangeResourceAmount(-expense.Cost * (expense as Resource).Amount);
            }
        }
    }

    public int CalculateTotalLocalHappiness()
    {
        int localHappy = 0;
        int localUnhappy = 0;

        foreach (var item in SettlementLinks)
        {
            // I am going to need a struct to get the linked settlement and the distance from the current settlement.
            localHappy += 4 - (4 - Mathf.Clamp(Mathf.CeilToInt(item.Distance / 2), 1, 3));
        }
        localUnhappy += Population / 3;

        LocalHappiness = localHappy;
        LocalUnhappiness = localUnhappy;

        return LocalHappiness - LocalUnhappiness;
    }

    public bool IsLinkedToSettlement(ILinkableSettlement link)
    {
        foreach (var item in SettlementLinks)
        {
            if (item.Target == link) return true;
        }
        return false;
    }

    public override void DestroySettlement()
    {
        UnlinkSettlement();
        Player.Instance.RemoveFromOutputResources(OutputResource);
        base.DestroySettlement();
    }

    public void OnTurnAction()
    {
        // One in a million chance, multiplied by local unhappiness. If locally happy, this is no longer an issue.
        if(Random.value * 1000000 < (1 * DetermineUnhappinessIfUnhappy()^2))
        {
            DestroySettlement();
        }
    }

    private int DetermineUnhappinessIfUnhappy()
    {
        if (CalculateTotalLocalHappiness() < 0) return Mathf.Abs(CalculateTotalLocalHappiness());
        else return 0;
    }

}
