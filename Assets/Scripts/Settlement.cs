using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using ACG.Resources;

public abstract class Settlement : MonoBehaviour, IGridObject
{
    public int Population = 0;
    public string Name;

    protected virtual void Start()
    {
        TurnManager.Instance.Settlements.Add(this);
        this.AlignToGrid(transform);
    }

    protected virtual void RelocateSettlement()
    {
        this.AlignToGrid(transform);
    }
}



public interface IPopulationChange
{
    void ChangePopulationByAmount(int amt);
}

public interface ILinkableSettlement
{
    //bool IsLinkableTo { get; }
    int MaximumLinkableDistance { get; }
    public LinkedList<ILinkableSettlement> LinkedSettlements { get; }

    void LinkSettlementTo(ILinkableSettlement other);
}

public interface IInputResources
{
    List<Resource> InputResources { get; }    
}

public interface IOutputResources
{
    List<Resource> OutputResource { get; }
}

public interface IHighlightWithinRange
{
    Color Unselected { get; }
    Color Selected { get; }

    void Highlight();
    void UnHighlight();
}