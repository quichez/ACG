using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlementActionPanel : MonoBehaviour
{
    [SerializeField] CellActionButton _actionButton;

    public void FIllPanelWithButtons(Settlement settlement)
    {





        CellActionButton clone = Instantiate(_actionButton, transform);
        clone.SetCellAction(settlement.DestroySettlement, "Destroy");
        
    }

    public void ClearPanelButtons()
    {
        foreach (Transform item in transform)
        {
            Destroy(item.gameObject);
        }
    }
}
