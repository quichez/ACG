using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionsExtraPanel : MonoBehaviour
{
    [SerializeField] UnitLinkExtraPanel _unitLinkExtraPanel;
    [SerializeField] BuildingControllerPanel _buildingControllerPanel;

    public UnitLinkExtraPanel unitLinkExtraPanel => _unitLinkExtraPanel;
    public BuildingControllerPanel unitBuildingExtraPanel => _buildingControllerPanel;

    public void ToggleLinkSettlementActionPanel()
    {
        _unitLinkExtraPanel.gameObject.SetActive(!_unitLinkExtraPanel.gameObject);
    }

    public void EnableUnitLinkActionPanel(bool enable)
    {
        _unitLinkExtraPanel.gameObject.SetActive(enable);
    }

    internal void EnableBuildingControllerPanel(bool v)
    {
        _buildingControllerPanel.gameObject.SetActive(v);
    }
}
