public struct SettlementLink
{
    public ILinkableSettlement Target { get; }
    public int Distance { get; }

    public SettlementLink(ILinkableSettlement tgt, int dst)
    {
        Target = tgt;
        Distance = dst;
    }
}
