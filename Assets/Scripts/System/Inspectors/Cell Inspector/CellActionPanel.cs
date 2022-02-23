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
            if (settle is FieldCell)
            {
                CellActionButton clone = Instantiate(_actionButton, transform);
                //clone.SetCellAction(() => settle.CreateSettlement(SettlementManager.Instance.SettlementUnit), "Create Settlement");
            }

            if (settle is ForestCell)
            {
                CellActionButton clone = Instantiate(_actionButton, transform);
                //clone.SetCellAction(() => settle.CreateSettlement(SettlementManager.Instance.LumberMill), "Create Lumber Mill");
            }
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
