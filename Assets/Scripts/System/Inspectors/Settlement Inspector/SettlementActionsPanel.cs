using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlementActionsPanel : MonoBehaviour
{
    [SerializeField] CellActionButton _actionButton;

    private void OnEnable()
    {
        FillPanelWithButtons(SettlementInspector.Instance.CurrentSettlement);
    }

    private void OnDisable()
    {
        ClearPanelButtons();
    }

    public void FillPanelWithButtons(Settlement settlement)
    {
        CellActionButton clone = Instantiate(_actionButton, transform);
        clone.SetCellAction(settlement.DestroySettlement, "Destroy");

        if (settlement is ILinkableSettlement)
        {
            SettlementInspector.Instance.EnableActionExtraPanel(false);
            CellActionButton linkButton = Instantiate(_actionButton, transform);
            linkButton.SetCellAction(() => SettlementInspector.Instance.ToggleActionExtraPanel("Links"), "Link Settlement");
        }
        if(settlement is Village)
        {
            SettlementInspector.Instance.EnableEditorPanel(false);
            SettlementInspector.Instance.EnableActionExtraPanel(false);
            
            CellActionButton villageEditorButton = Instantiate(_actionButton, transform);
            villageEditorButton.SetCellAction(() => TestSettlementSelector.Instance.EditorPanel.gameObject.SetActive(!TestSettlementSelector.Instance.EditorPanel.gameObject.activeSelf),"Toggle Test Editor");
            
            CellActionButton buildingControllerButton = Instantiate(_actionButton, transform);
            buildingControllerButton.SetCellAction(() => SettlementInspector.Instance.ToggleActionExtraPanel("Buildings"), "Buildings");
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
