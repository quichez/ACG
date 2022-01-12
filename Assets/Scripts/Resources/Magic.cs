using ACG.Resources;

public class Magic : Resource, IRenewableResource
{

    public int RenewalAmount { get; set; }

    public override string Name => "Magic";

    public override string Description => "Limited Potential(TM)";

    public void ChangeRenewalAmountByAmount(int amt)
    {
        throw new System.NotImplementedException();
    }

    public void RenewResource()
    {
        throw new System.NotImplementedException();
    }

    public void RenewResourceByAmount(int amt)
    {
        throw new System.NotImplementedException();
    }

    public Magic() : base() { }
    public Magic(int amt) : base(amt) { }
}