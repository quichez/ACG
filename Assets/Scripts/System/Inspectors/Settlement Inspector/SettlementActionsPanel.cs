using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlementActionsPanel : MonoBehaviour
{
    [SerializeField] CellActionButton _actionButton;

    public void FIllPanelWithButtons(Settlement settlement)
    {





        CellActionButton clone = Instantiate(_actionButton, transform);
        clone.SetCellAction(settlement.DestroySettlement, "Destroy");

        if (settlement is ILinkableSettlement)
        {
            SettlementInspector.Instance.EnableActionExtraPanel(false);
            CellActionButton linkButton = Instantiate(_actionButton, transform);
            linkButton.SetCellAction(SettlementInspector.Instance.ToggleActionExtraPanel, "Link Settlement");
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
