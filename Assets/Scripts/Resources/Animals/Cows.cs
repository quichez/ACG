using System;
using ACG.Resources;

public class Cows : Resource, IExpenseResource
{
    public override string Name => "Cows";

    public override string Description => "Moo.";

    public int Cost { get; private set; } = 2;

    public Type ResourceRequired => typeof(Money);

    public Cows() : base() { }

    public Cows(int amount = 1) : base(amount) { }

    public void SetCostToAmount(int amt)
    {
        Cost = amt;
    }

    public void ChangeCostByAmount(int amt)
    {
        Cost += amt;
    }

    public void PayForResource()
    {
        throw new System.NotImplementedException();
    }
}
