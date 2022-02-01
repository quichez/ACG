using ACG.Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Village : Settlement, IPopulationChange, IInputResources, IOutputResources, ILinkableSettlement, IHappinessSettlement
{
    //public bool IsLinkableTo => throw new System.NotImplementedException();

    public int MaximumLinkableDistance { get; private set; } = 3;

    public List<Resource> InputResources { get; private set; } = new();

    public List<Resource> OutputResource { get; private set; } = new();

    public LinkedList<ILinkableSettlement> LinkedSettlements { get; } = new();
    public LinkedList<SettlementLink> SettlementLinks { get; private set; } = new();

    public int LocalHappiness { get; private set; } = 3;
    public int LocalUnhappiness { get; private set; } = 0;


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
                if (!LinkedSettlements.Contains(link) && cell.gameObject != gameObject)
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

    public void LinkSettlementTo(ILinkableSettlement other)
    {
        LinkedSettlements.AddLast(other);
        Debug.Log("hi");
    }

    private void LinkSettlementTo_2(SettlementLink newLink)
    {

    }

    protected override void Start()
    {
        base.Start();
        Name = Random.value.ToString();
        var newMoney = new Money(10);
        (newMoney as IRenewableResource).ChangeRenewalAmountByAmount(1);
        InputResources.Add(newMoney);

    }

    public override void SetCellLocation(Cell cell)
    {
        base.SetCellLocation(cell);
        if (cell is ICellResources resources)OutputResource.AddRange(resources.CellResources);
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

        //localHappy += 
        foreach (ILinkableSettlement item in LinkedSettlements)
        {
            // I am going to need a struct to get the linked settlement and the distance from the current settlement.
        }
        localUnhappy += Population / 4;

        LocalHappiness = localHappy;
        LocalUnhappiness = localUnhappy;

        return LocalHappiness - LocalUnhappiness;
    }
}
