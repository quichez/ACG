using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ACG.Inspectors;

public class UnitInspector : Inspector2
{
    public static UnitInspector Instance;
    public Unit CurrentUnit { get; private set; }

    [SerializeField] UnitNamePanel _namePanel;    
    [SerializeField] UnitResourcePanel _resourcePanel;
    [SerializeField] SettlementLinkPanel _linkPanel;
    [SerializeField] SettlementActionsPanel _settlementActionsPanel;
    [SerializeField] SettlementActionsExtraPanel _settlementActionsExtraPanel;
    [SerializeField] IdleInspectorResourcePanel _idleInspectorResourcePanel;

    private void Awake()
    {
        Instance = this;
    }

    public override void ClearPanels()
    {        
        _namePanel.gameObject.SetActive(false);
        _resourcePanel.gameObject.SetActive(false);
        //_editorPanel.gameObject.SetActive(false);

    }

    public override void FillPanels(Unit unit)
    {
        CurrentUnit = unit; // only need to set it to new unit when selected.
        _namePanel.gameObject.SetActive(true);

        switch (unit)
        {
            case SettlementUnit st:
                _resourcePanel.gameObject.SetActive(true);
                break;
            default:
                break;
        }

    }
}
