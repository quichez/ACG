using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ACG.Resources;
using ACG.Inspectors;
using TMPro;

public class CellResourcePanel : InspectorPanel
{
    TextMeshProUGUI _text => GetComponentInChildren<TextMeshProUGUI>();
    
    public void SetCellResourceText(ICellResources resources)
    {
        if (resources is null ||resources.CellResources.Count == 0) gameObject.SetActive(false);
        else
        {
            
            _text.text = "Resource:\n";
            foreach (Resource item in resources.CellResources)
            {
                _text.text += item.ToString() + "\n";
            }
        }
    }

    public void ClearCellResourceText() => _text.text = "";

    private void OnDisable()
    {
        ClearCellResourceText();
    }
}
