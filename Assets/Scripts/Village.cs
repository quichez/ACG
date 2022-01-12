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

    public LinkedList<ILinkableSettlement> LinkedSettlements => throw new System.NotImplementedException();

    public void ChangePopulationByAmount(int amt)
    {
        Population += amt;
    }

    public void LinkSettlementTo(ILinkableSettlement other)
    {
        throw new System.NotImplementedException();
    }

    protected override void Start()
    {
        InputResources.Add(new Money(100));
        OutputResource.Add(new Cows(1));
        OutputResource.Add(new Chickens(2));
        (InputResources.Find(res => res.GetType() == typeof(Money)) as IRenewableResource).ChangeRenewalAmountByAmount(2);
        base.Start();
    }
}
