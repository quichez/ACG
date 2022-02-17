using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCellActionPanel : MonoBehaviour
{
    [SerializeField] CellActionButton _actionButton;

    public void FillPanelWithButtons(Cell cell)
    {
        if (cell is ISettleable settle)
        {

            CellActionButton clone1 = Instantiate(_actionButton, transform);
            clone1.SetCellAction(() => settle.CreateSettlement(SettlementManager.Instance.SettlementUnit), "Create Village");            
        }
    }

    public void ClearPanelButtons()
    {
        foreach (Transform item in transform)
        {
            Destroy(item.gameObject);
        }
	}
}
