using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionsPanel : MonoBehaviour
{
    [SerializeField] CellActionButton _actionButton;

    UnitActionsExtraPanel _unitActionsExtraPanel => UnitInspector.Instance.unitActionsExtraPanel;

    private void OnEnable()
    {
        FillPanelWithButtons();
    }

    private void OnDisable()
    {
        ClearPanelButtons();
    }

    private void ClearPanelButtons()
    {
        foreach (Transform item in transform)
        {
            Destroy(item.gameObject);
        }
    }

    private void FillPanelWithButtons()
    {
        switch (UnitInspector.Instance.CurrentUnit)
        {
            case SettlementUnit:
                CellActionButton clone = Instantiate(_actionButton, transform);
                clone.SetCellAction(UnitInspector.Instance.CurrentUnit.DestroyUnit, "Destroy Unit");

                CellActionButton links = Instantiate(_actionButton, transform);
                SetUnitLinkActionButton(links);

                CellActionButton buildings = Instantiate(_actionButton, transform);
                SetUnitBuildingsActionButton(buildings);
                break;

            default:
                break;
        }       
    }

    private void SetUnitLinkActionButton(CellActionButton button)
    {
        UnitInspector.Instance.EnableActionExtraPanel(false);
        button.SetCellAction(ToggleUnitLinksExtraPanel, "Links");
    }    

    private void SetUnitBuildingsActionButton(CellActionButton button)
    {
        UnitInspector.Instance.EnableActionExtraPanel(false);
        button.SetCellAction(ToggleUnitBuildingExtraPanel, "Buildings");
    }

    private void ToggleUnitLinksExtraPanel()
    {
        if (_unitActionsExtraPanel.unitLinkExtraPanel.gameObject.activeSelf)
        {
            foreach (Transform child in _unitActionsExtraPanel.transform)
            {
                child.gameObject.SetActive(false);
            }
            UnitInspector.Instance.EnableActionExtraPanel(false);
        }
        else
        {
            foreach (Transform child in _unitActionsExtraPanel.transform)
            {
                child.gameObject.SetActive(false);
            }
            UnitInspector.Instance.EnableActionExtraPanel(true);
            UnitInspector.Instance.EnableUnitActionExtraPanel(
                _unitActionsExtraPanel.unitLinkExtraPanel, true);
        }
    }

    private void ToggleUnitBuildingExtraPanel()
    {
        
        if (_unitActionsExtraPanel.unitBuildingExtraPanel.gameObject.activeSelf)
        {
            foreach (Transform child in _unitActionsExtraPanel.transform)
            {
                child.gameObject.SetActive(false);
            }
            UnitInspector.Instance.EnableActionExtraPanel(false);
        }
        else
        {
            foreach (Transform child in _unitActionsExtraPanel.transform)
            {
                child.gameObject.SetActive(false);
            }
            UnitInspector.Instance.EnableActionExtraPanel(true);
            UnitInspector.Instance.EnableUnitActionExtraPanel(
                _unitActionsExtraPanel.unitBuildingExtraPanel, true);
        }
    }
}
