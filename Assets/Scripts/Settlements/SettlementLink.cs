using System;
using UnityEngine;

[Serializable]
public struct SettlementLink
{
    public ILinkableSettlement Target { get; }
    public int Distance { get; }

    public SettlementLink(ILinkableSettlement tgt, int dst)
    {
        Target = tgt;
        Distance = dst;
    }

    public Settlement GetTargetAsSettlement() => (Settlement)Target;
    public string GetTargetName() => (Target as Settlement).Name;
}
