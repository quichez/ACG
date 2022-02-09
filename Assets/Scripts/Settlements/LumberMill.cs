using ACG.Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumberMill : Settlement, IInputResources, IOutputResources, ILinkableSettlement
{
    public int MaximumLinkableDistance { get; private set; } = 2;

    public LinkedList<ILinkableSettlement> LinkedSettlements { get; private set; } = new();

    public List<Resource> InputResources { get; private set; } = new();

    public List<Resource> OutputResource { get; private set; } = new();

    public List<SettlementLink> SettlementLinks { get; private set; } = new();

    public bool IsSingleLinkable => true;

    public void CalculateAndSpendOnExpenseResources()
    {        
        foreach (Resource temp in OutputResource) // Loop through each resource to calculate resource expenses
        {
            if (temp is IExpenseResource expense)
            {
                if(expense is Lumber lum)
                {
                    Resource lumRes = InputResources.Find(res => res.GetType() == expense.ResourceRequired);
                    lumRes.ChangeResourceAmount(-expense.Cost * (expense as Resource).Amount / 2);
                }
            }
        }
    }

    public List<ILinkableSettlement> FindLinkableSettlements()
    {
        List<ILinkableSettlement> linkableSettlements = new();

        if(SettlementLinks.Count > 0) return linkableSettlements;

        Collider[] linkablesInRange = Physics.OverlapBox(transform.position, Vector3.one * MaximumLinkableDistance, Quaternion.identity, TestSettlementSelector.Instance.SettlementMask);
        foreach (var cell in linkablesInRange)
        {
            if (cell.TryGetComponent(out ILinkableSettlement link))
            {
                if (!(this as ILinkableSettlement).IsLinkedToSettlement(link) && cell.gameObject != gameObject)
                {
                    linkableSettlements.Add(link);
                }
            }
        }
        return linkableSettlements;
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

    public void SetEffectiveResourceAmount(Resource res, bool status)
    {
        if (!status) res.SetEffectiveAmount(res.Amount);
        else res.SetEffectiveAmount(res.Amount/2);
    }

    public override void SetCellLocation(Cell cell)
    {
        base.SetCellLocation(cell);
        if(cell is ICellResources resources)
        {
            var lumber = resources.CellResources.Find(item => item.GetType() == typeof(Lumber));
            if (lumber != null)
            {
                OutputResource.Add(new Lumber(lumber.Amount * 2));
            }
            else
            {
                OutputResource.Add(new Lumber(2));
            }
        }
    }

    protected override void Start()
    {
        base.Start();
        var money = new Money(10);
        money.SetRenewalAmountTo(0);
        InputResources.Add(money);
        TurnManager.Instance.SubscribeToTurnManager(OnTurnAction);

    }

    public void LinkSettlementTo(SettlementLink link)
    {
        SettlementLinks.Add(link);
    }

    public void UnlinkSettlement()
    {
        foreach (var item in TurnManager.Instance.Settlements)
        {
            if(item is ILinkableSettlement linkable)
            {
                linkable.SettlementLinks.Remove(linkable.SettlementLinks.Find(x => (Object)x.Target == this));
            }
        }
    }

    private void Update()
    {
        
    }

    private void OnTurnAction()
    {
        Debug.Log("hi");
    }

    public override void DestroySettlement()
    {
        UnlinkSettlement();
        base.DestroySettlement();
    }
}
