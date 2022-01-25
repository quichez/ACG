using ACG.Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Village : Settlement, IPopulationChange, IInputResources, IOutputResources, ILinkableSettlement
{
    //public bool IsLinkableTo => throw new System.NotImplementedException();

    public int MaximumLinkableDistance { get; private set; } = 3;

    public List<Resource> InputResources { get; private set; } = new();

    public List<Resource> OutputResource { get; private set; } = new();

    public LinkedList<ILinkableSettlement> LinkedSettlements { get; private set; } = new();

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

    public void LinkSettlementTo(ILinkableSettlement other)
    {
        LinkedSettlements.AddLast(other);
        Debug.Log("hi");
    }

    protected override void Start()
    {
        base.Start();

        InputResources.Add(new Money(10));        
        (InputResources.Find(res => res.GetType() == typeof(Money)) as IRenewableResource).ChangeRenewalAmountByAmount(1);

    }

    public override void SetCellLocation(Cell cell)
    {
        base.SetCellLocation(cell);
        if (cell is ICellResources resources)OutputResource.AddRange(resources.CellResources);
        foreach (IExpenseResource expense in OutputResource)
        {
            expense.SetCostToAmount(0);
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

}
