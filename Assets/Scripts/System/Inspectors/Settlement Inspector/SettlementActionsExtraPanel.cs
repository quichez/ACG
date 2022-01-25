using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlementActionsExtraPanel : MonoBehaviour
{
    [SerializeField] LinkSettlementActionPanel linkSettlementActionPanel;
    
    public void ToggleLinkSettlementActionPanel()
    {
        linkSettlementActionPanel.gameObject.SetActive(!linkSettlementActionPanel.gameObject);
    }

    public void EnableLinkSettlementActionPanel(bool enable)
    {
        linkSettlementActionPanel.gameObject.SetActive(enable);
    }
}
