using ACG.Resources;
using System;

public class Lumber : Resource, IExpenseResource
{
    public override string Name => "Lumber";

    public override string Description => "Trees!";

    public int Cost { get; private set; } = 1;
    public Type ResourceRequired => typeof(Money);

    public Lumber() : base() { }
    public Lumber(int amt) : base(amt) { }

    public void SetCostToAmount(int amt)
    {
        Cost = amt;
    }

    public void ChangeCostByAmount(int amt)
    {
        Cost += amt;
    }
}
