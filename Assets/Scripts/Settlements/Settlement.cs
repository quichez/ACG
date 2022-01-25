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
        TestSettlementSelector.Instance.EnableSettlementSelectorPanels();
        GetComponent<Renderer>().material.color = Color.red;
        TestSettlementSelector.Instance.NamePanel.SetText(Name);
        TestSettlementSelector.Instance.EditorPanel.SetPanelInformation(this);
        TestSettlementSelector.Instance.ExplorerPanel.SetPanelInfo(this);
        if(this is ILinkableSettlement) TestSettlementSelector.Instance.LinkPanel.InitializePanel(this);
        HighlightCellsWithinRange();

        SettlementInspector.Instance.Fill(this);
    }

    public void OnDeselect()
    {
        GetComponent<Renderer>().material.color = Color.green;
        TestSettlementSelector.Instance.EnableSettlementSelectorPanels(false);
        TestSettlementSelector.Instance.NamePanel.SetText();
        TestSettlementSelector.Instance.EditorPanel.Clear();
        TestSettlementSelector.Instance.ExplorerPanel.Clear();
        ClearHighlightedCellsWithinRange();
        cell.ClearSettlement();
        SettlementInspector.Instance.Clear();
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

    public void DestroySettlement()
    {
        OnDeselect();        
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
    int MaximumLinkableDistance { get; }
    public LinkedList<ILinkableSettlement> LinkedSettlements { get; }

    List<ILinkableSettlement> FindLinkableSettlements();
    void LinkSettlementTo(ILinkableSettlement other);
}

public interface IInputResources
{
    List<Resource> InputResources { get; }    
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