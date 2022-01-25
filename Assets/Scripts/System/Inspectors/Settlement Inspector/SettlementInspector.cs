using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ACG.Inspectors;

public class SettlementInspector : Inspector
{
    public static SettlementInspector Instance;
    [SerializeField] SettlementActionPanel settlementActionPanel;

    private void Awake() => Instance = this;

    public override void Clear()
    {
        settlementActionPanel.ClearPanelButtons();
        settlementActionPanel.gameObject.SetActive(false);
    }

    public override void Fill(IInspectable inspectable)
    {
        if(inspectable is Settlement settlement)
        {
            settlementActionPanel.gameObject.SetActive(true);
            settlementActionPanel.FIllPanelWithButtons(settlement);
        }
    }
    
}
