using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ACG.Inspectors;

public class SettlementInspector : Inspector
{
    public static SettlementInspector Instance;
    [SerializeField] SettlementActionsPanel settlementActionsPanel;
    [SerializeField] SettlementActionsExtraPanel settlementActionsExtraPanel;

    private void Awake() => Instance = this;

    public override void Clear()
    {
        settlementActionsPanel.ClearPanelButtons();
        settlementActionsPanel.gameObject.SetActive(false);
    }

    public override void Fill(IInspectable inspectable)
    {
        if(inspectable is Settlement settlement)
        {
            settlementActionsPanel.gameObject.SetActive(true);
            settlementActionsPanel.FIllPanelWithButtons(settlement);
        }
    }

    public void ToggleActionExtraPanel() => settlementActionsExtraPanel.gameObject.SetActive(!settlementActionsExtraPanel.gameObject.activeSelf);

    public void EnableActionExtraPanel(bool enable) => settlementActionsExtraPanel.gameObject.SetActive(enable);
}
