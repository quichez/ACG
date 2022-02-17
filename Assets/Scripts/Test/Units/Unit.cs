using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ACG.Resources;

public abstract class Unit : MonoBehaviour, ISelectable
{
    public string unitName { get; protected set; }
    public Cell cell { get; private set; }
    public List<Resource> unitResources { get; } = new();

    public void GetBaseResourceFromCell(Cell cell)
    {
        this.cell = cell;
        if(cell is ICellResources cr)
        {
            unitResources.AddRangeToResourceList(cr.CellResources);
        }
    }

    public void OnSelect()
    {
        GetComponent<Renderer>().material.color = Color.red;
        HighlightCellsWithinRange();
        UnitInspector.Instance.FillPanels(this);
    }

    public void OnDeselect()
    {
        GetComponent<Renderer>().material.color = Color.green;
        ClearHighlightedCellsWithinRange();
        UnitInspector.Instance.ClearPanels();
    }

    private void HighlightCellsWithinRange()
    {
        if (this is ILinkableSettlement linkable)
        {
            Collider[] cellsInRange = Physics.OverlapBox(transform.position, Vector3.one * linkable.MaximumLinkableDistance, Quaternion.identity, SelectorManager.Instance.CellMask);
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
            Collider[] cellsInRange = Physics.OverlapBox(transform.position, Vector3.one * linkable.MaximumLinkableDistance, Quaternion.identity, SelectorManager.Instance.CellMask);
            foreach (var cell in cellsInRange)
            {
                if (cell.TryGetComponent(out IHighlightWithinRange hl)) hl.UnHighlight();
            }
        }
    }

    /// <summary>
    /// Base implementation is to be called last. This destroys the GameObject.
    /// </summary>
    public virtual void DestroyUnit()
    {
        OnDeselect();
        Destroy(gameObject);
    }

    private void Start()
    {
        unitName = Random.value.ToString();
    }
}

public interface ILinkableUnit
{

}

public interface IUpgradeableUnit
{

}
