using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlementActionsExtraPanel : MonoBehaviour
{
    [SerializeField] LinkSettlementActionPanel linkSettlementActionPanel;
    [SerializeField] BuildingControllerPanel _buildingControllerPanel;

    public void ToggleLinkSettlementActionPanel()
    {
        linkSettlementActionPanel.gameObject.SetActive(!linkSettlementActionPanel.gameObject);
    }

    public void EnableLinkSettlementActionPanel(bool enable)
    {
        linkSettlementActionPanel.gameObject.SetActive(enable);
    }

    internal void EnableBuildingControllerPanel(bool v)
    {
        _buildingControllerPanel.gameObject.SetActive(v);
    }
}
