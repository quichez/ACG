using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ACG.Resources;

public class ResourcePanel : MonoBehaviour
{
    Settlement settlement;
    public TextMeshProUGUI Text;

    private void Update()
    {
        SetPanelInfo(settlement);
    }

    public void SetPanelInfo(Settlement stm)
    {
        settlement = stm;        

        if (settlement) Text.text = "Population: " + settlement.Population + "\n";
        if (settlement is IInputResources || settlement is IOutputResources) Text.text += "Resources:\n";
        if (settlement is IInputResources inputs)
        {
            Text.text += "Input:\n";
            foreach (Resource item in inputs.InputResources)
            {
                Text.text += item.Name + "   " + item.Amount + "\n";
            }
        }
        if (settlement is IOutputResources outputs)
        {
            Text.text += "Output:\n";
            foreach (Resource item in outputs.OutputResource)
            {
                Text.text += item.ToString() + "\n";
            }
        }
    }
    
    public void Clear()
    {
        settlement = null;
        Text.text = "";
    }

    private void OnDisable()
    {
        Text.text = "";
    }
}
