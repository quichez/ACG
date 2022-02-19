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
    [SerializeField] UnitLinkPanel _linkPanel;
    [SerializeField] UnitActionsPanel _unitActionsPanel;
    [SerializeField] UnitActionsExtraPanel _unitActionsExtraPanel;
    [SerializeField] IdleInspectorResourcePanel _idleInspectorResourcePanel;

    public UnitActionsExtraPanel unitActionsExtraPanel => _unitActionsExtraPanel;
    private void Awake()
    {
        Instance = this;
    }

    public override void ClearPanels()
    {        
        _namePanel.gameObject.SetActive(false);
        _resourcePanel.gameObject.SetActive(false);
        _linkPanel.gameObject.SetActive(false);
        _unitActionsPanel.gameObject.SetActive(false);
        _unitActionsExtraPanel.gameObject.SetActive(false);
        //_editorPanel.gameObject.SetActive(false);

    }

    public override void FillPanels(Unit unit)
    {
        CurrentUnit = unit; // only need to set it to new unit when selected.
        _namePanel.gameObject.SetActive(true);
        _unitActionsPanel.gameObject.SetActive(true);
        switch (unit)
        {
            case SettlementUnit st:
                _resourcePanel.gameObject.SetActive(true);
                if(st is ILinkableUnit linkable)
                {
                    _linkPanel.gameObject.SetActive(true);
                }
                break;
            default:
                break;
        }

    }

    public void EnableActionExtraPanel(bool enb) => _unitActionsExtraPanel.gameObject.SetActive(enb);

    public void EnableUnitLinkExtraPanel(bool enb) => _unitActionsExtraPanel.EnableUnitLinkActionPanel(enb);

    public void EnableUnitActionExtraPanel(MonoBehaviour extraPanel, bool enb)
    {
        EnableActionExtraPanel(enb);
        extraPanel.gameObject.SetActive(enb);
    }

    public void ToggleActionExtraPanel()
    {

    }
}

