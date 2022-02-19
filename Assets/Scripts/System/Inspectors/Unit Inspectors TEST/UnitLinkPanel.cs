using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitLinkPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    void Update()
    {
        SetPanelInfo(UnitInspector.Instance.CurrentUnit as ILinkableUnit);   
    }

    private void SetPanelInfo(ILinkableUnit linkableUnit)
    {
        _text.text = linkableUnit?.ToString() ?? "balls";
    }
}
