using ACG.Resources;

public class Money : Resource, IRenewableResource
{
    public override string Name => "Money";

    public override string Description => "dolla";

    public int RenewalAmount { get; private set; } = 1;

    public Money() : base() { }

    public Money(int amt = 1) : base(amt) { }

    public void RenewResourceByAmount(int amt)
    {
        ChangeResourceAmount(amt);
    }
    
    public void ChangeRenewalAmountByAmount(int amt)
    {
        RenewalAmount += amt;
    }

    public void RenewResource()
    {
        ChangeResourceAmount(RenewalAmount);
    }
}
