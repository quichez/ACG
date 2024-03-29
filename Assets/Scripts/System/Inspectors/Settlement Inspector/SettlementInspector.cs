using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ACG.Inspectors;

public class SettlementInspector : Inspector
{
    public static SettlementInspector Instance;
    public Settlement CurrentSettlement { get; private set; }

    [SerializeField] ResourcePanel _explorerPanel;
    [SerializeField] VillageEditorPanel _editorPanel;
    [SerializeField] UnitNamePanel _namePanel;
    [SerializeField] SettlementLinkPanel _linkPanel;
    [SerializeField] SettlementActionsPanel _settlementActionsPanel;
    [SerializeField] UnitActionsExtraPanel _settlementActionsExtraPanel;
    [SerializeField] IdleInspectorResourcePanel _idleInspectorResourcePanel;


    private void Awake() => Instance = this;

    public override void ClearPanels()
    {
        _namePanel.SetText();
        _explorerPanel.Clear();
        _editorPanel.Clear();
        _settlementActionsPanel.gameObject.SetActive(false);
        _settlementActionsExtraPanel.gameObject.SetActive(false);
    }

    public override void FillPanels(IInspectable inspectable)
    {
        if (inspectable is Settlement settlement)
        {
            CurrentSettlement = settlement;
            _namePanel.SetText(CurrentSettlement.Name);
            _editorPanel.SetPanelInformation(CurrentSettlement);
            _explorerPanel.SetPanelInfo(CurrentSettlement);
            _settlementActionsPanel.gameObject.SetActive(true);

            if (CurrentSettlement is ILinkableSettlement) _linkPanel.InitializePanel(CurrentSettlement);
            _idleInspectorResourcePanel.gameObject.SetActive(false);    
        }
    }

    public void EnableSettlementInspectorPanels(bool active = true)
    {
        _explorerPanel.gameObject.SetActive(active);
        _editorPanel.gameObject.SetActive(active);
        _namePanel.gameObject.SetActive(active);
        _linkPanel.gameObject.SetActive(active);
    }

    public void EnableEditorPanel(bool enable) => _editorPanel.gameObject.SetActive(enable);
    public void ToggleActionExtraPanel(string extraPanel) 
    {
        _settlementActionsExtraPanel.gameObject.SetActive(!_settlementActionsExtraPanel.gameObject.activeSelf);
        if (_settlementActionsExtraPanel.gameObject.activeSelf)
        {
            if (extraPanel == "Links") _settlementActionsExtraPanel.EnableUnitLinkActionPanel(true);
            if (extraPanel == "Buildings") _settlementActionsExtraPanel.EnableBuildingControllerPanel(true);
        }
    }


    public void EnableActionExtraPanel(bool enable) => _settlementActionsExtraPanel.gameObject.SetActive(enable);

}
