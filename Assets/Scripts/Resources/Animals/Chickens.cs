using ACG.Resources;
using System;

public class Chickens : Resource, IExpenseResource
{
    public override string Name => "Chickens";

    public override string Description => "Cluck.";

    public int Cost { get; private set; } = 1;

    public Type ResourceRequired => typeof(Money);

    public Chickens() : base() { }
    public Chickens(int amt = 1) : base(amt) { }

    public void SetCostToAmount(int amt)
    {
        Cost = amt;
    }

    public void ChangeCostByAmount(int amt)
    {
        Cost += amt;
    }
}
