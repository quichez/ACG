using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ACG.Inspectors;

public class CellTypePanel : InspectorPanel
{
    TextMeshProUGUI _text => GetComponentInChildren<TextMeshProUGUI>();
    public void SetCellTypeText(string type) => _text.text = type;

    private void OnDisable()
    {
        SetCellTypeText("");
    }
}
