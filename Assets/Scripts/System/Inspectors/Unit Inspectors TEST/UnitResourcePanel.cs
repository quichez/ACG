using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitResourcePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Text;
    Unit _unit;

    private void OnEnable()
    {
        _unit = UnitInspector.Instance.CurrentUnit;
    }

    private void OnDisable()
    {
        _unit = null;
        Text.text = "";
    }

    private void Update()
    {
        SetPanelText();
    }

    private void SetPanelText()
    {
        Text.text = "";
        if (_unit is SettlementUnit su)
        {
            Text.text += "Population: " + su.population + "\n";
        }

        Text.text += "Resources:\n";

        foreach (var item in _unit.unitResources)
        {
            Text.text += item.Name + "   " + item.Amount + "\n";
        }
    }
}
