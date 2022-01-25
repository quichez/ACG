using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ACG.Inspectors;
using ACG.Resources;

public abstract class Cell : MonoBehaviour, IGridObject, IHighlightWithinRange, ISelectable, IInspectable
{
    protected Color _unselected;
    [SerializeField] protected SpriteRenderer _sprite;
    [SerializeField] SpriteRenderer _selectMask;

    public bool IsPopulated { get; private set; }

    public Color Unselected => _unselected;

    public abstract Color Highlighted { get; }

    public bool IsSettled { get; private set; }

    public virtual void Highlight()
    {
        _sprite.color = Highlighted;
    }

    public virtual void UnHighlight()
    {
        _sprite.color = Unselected;
    }

    public void OnDeselect()
    {
        CellInspector.Instance.ClearPanels();
        _selectMask.gameObject.SetActive(true);
    }

    public void OnSelect()
    {        
        _selectMask.gameObject.SetActive(false);
        CellInspector.Instance.FillPanels(this);
    }

    protected void Start()
    {
        _unselected = _sprite.color;
    }

    public void CreateSettlement(Settlement settlement)
    {
        if (IsPopulated) return;
        Settlement clone = Instantiate(settlement, transform.position,Quaternion.identity);
        clone.SetCellLocation(this);
        IsPopulated = true;

        CellInspector.Instance.ClearPanels();
    }

    public void ClearSettlement()
    {
        IsPopulated = false;
    }

    public abstract string GetCellType();
}

public interface ISettleable
{
    bool IsSettled { get; }
    void CreateSettlement(Settlement settlement);
    void ClearSettlement();
}

public interface ICellResources
{
    List<Resource> CellResources { get; }

    void InitializeResources()
    {
        Debug.Log("Resource intialization not yet created for this object");
    }
}
