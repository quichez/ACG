using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ACG.Inspectors;

public class CellTitlePanel : InspectorPanel
{
    [SerializeField] TextMeshProUGUI _text;

    public void SetCellTitleText(string title)
    {
        _text.text = title;
    }

    private void OnDisable()
    {
        SetCellTitleText("");
    }
}
