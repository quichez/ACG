using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellActionPanel : MonoBehaviour
{
    [SerializeField] CellActionButton _actionButton;

    public void FillPanelWithButtons(Cell cell)
    {
        if (cell is ISettleable settle)
        {
            CellActionButton clone = Instantiate(_actionButton, transform);
            clone.SetCellAction(settle.CreateSettlement, "Create Settlement");
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
