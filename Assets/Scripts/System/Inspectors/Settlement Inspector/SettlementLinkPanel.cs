using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SettlementLinkPanel : MonoBehaviour
{
    Settlement linkableSettlement;
    public TextMeshProUGUI Text;

    private void Update()
    {
        SetPanelInfo();
    }

    public void InitializePanel(Settlement settlement)
    {
        linkableSettlement = settlement;
    }

    void SetPanelInfo()
    {
        if (linkableSettlement is ILinkableSettlement linkable)
        {
            if (linkable.SettlementLinks.Count > 0) Text.text = "Linked Settlements:\n";
            else gameObject.SetActive(false);
            foreach (var temp in linkable.SettlementLinks)
            {
                Text.text += temp.GetTargetName() + "\n";
            }
        } 
        else gameObject.SetActive(false);
    }
}
