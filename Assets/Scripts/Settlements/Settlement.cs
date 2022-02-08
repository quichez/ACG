using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using ACG.Resources;
using ACG.Inspectors;

public abstract class Settlement : MonoBehaviour, IGridObject, ISelectable, IInspectable
{
    public int Population = 0;
    public string Name;
    Cell cell;

    protected virtual void Start()
    {
        TurnManager.Instance.Settlements.Add(this);
        this.AlignToGrid(transform);
    }

    public virtual void SetCellLocation(Cell cell) => this.cell = cell;

    protected virtual void RelocateSettlement()
    {
        this.AlignToGrid(transform);
    }

    public void OnSelect()
    {
        GetComponent<Renderer>().material.color = Color.red;
        HighlightCellsWithinRange();

        SettlementInspector.Instance.EnableSettlementInspectorPanels();
        SettlementInspector.Instance.FillPanels(this);

    }

    public void OnDeselect()
    {
        GetComponent<Renderer>().material.color = Color.green;
        ClearHighlightedCellsWithinRange();

        SettlementInspector.Instance.ClearPanels();
        SettlementInspector.Instance.EnableSettlementInspectorPanels(false);

        cell.ClearSettlement();
    }

    private void HighlightCellsWithinRange()
    {
        if (this is ILinkableSettlement linkable)
        {
            Collider[] cellsInRange = Physics.OverlapBox(transform.position, Vector3.one * linkable.MaximumLinkableDistance, Quaternion.identity, TestSettlementSelector.Instance.CellMask);
            foreach (var cell in cellsInRange)
            {
                if (cell.TryGetComponent(out IHighlightWithinRange hl)) hl.Highlight();
            }
        }
    }

    private void ClearHighlightedCellsWithinRange()
    {
        if (this is ILinkableSettlement linkable)
        {
            Collider[] cellsInRange = Physics.OverlapBox(transform.position, Vector3.one * linkable.MaximumLinkableDistance, Quaternion.identity, TestSettlementSelector.Instance.CellMask);
            foreach (var cell in cellsInRange)
            {
                if (cell.TryGetComponent(out IHighlightWithinRange hl)) hl.UnHighlight();
            }
        }
    }

    public virtual void DestroySettlement()
    {
        OnDeselect();
        TurnManager.Instance.Settlements.Remove(this);        
        Destroy(gameObject);
    }
}



public interface IPopulationChange
{
    void ChangePopulationByAmount(int amt);
}

public interface ILinkableSettlement
{
    //bool IsLinkableTo { get; }
    bool IsSingleLinkable { get; }
    int MaximumLinkableDistance { get; }
    LinkedList<ILinkableSettlement> LinkedSettlements { get; }
    List<SettlementLink> SettlementLinks { get; }
    List<ILinkableSettlement> FindLinkableSettlements();
    void LinkSettlementTo(SettlementLink link);
    bool IsLinkedToSettlement(ILinkableSettlement link)
    {
        foreach (var item in SettlementLinks)
        {
            if (item.Target == link) return true;
        }
        return false;
    }
    void UnlinkSettlement();
    void GetGoldBonusFromLinkedSettlements()
    {
        if(this is IInputResources currInputs)
        {
            int bonusGold = 0;
            foreach (ILinkableSettlement item in LinkedSettlements)
            {
                if(item is IInputResources inputs)
                {
                    if (inputs.FindInputResource(typeof(Money)) is IRenewableResource res)
                    {
                        bonusGold += Mathf.CeilToInt((float)res.RenewalAmount / 4);
                    }
                }
            }

            Resource res2 = currInputs.FindInputResource(typeof(Money));
            if (res2 != null)
            {
                res2.ChangeResourceAmount(bonusGold);
            }
        }
    }
}

public interface IInputResources
{
    List<Resource> InputResources { get; }  
    Resource FindInputResource(Type inputResource)
    {
        return InputResources.Find(res => res.GetType() == inputResource);
    }
}

public interface IOutputResources
{
    List<Resource> OutputResource { get; }
    void SetEffectiveResourceAmount(Resource res, bool status);
    void SetAllEffectiveResourceAmounts();
    void CalculateAndSpendOnExpenseResources();
}

public interface IHighlightWithinRange
{
    Color Unselected { get; }
    Color Highlighted { get; }

    void Highlight();
    void UnHighlight();
}

/// <summary>
/// This settlement has happiness.
/// </summary>
public interface IHappinessSettlement
{
    int LocalHappiness { get; }
    int LocalUnhappiness { get; }

    int CalculateTotalLocalHappiness()
    {
        return LocalHappiness - LocalUnhappiness;
    }
}