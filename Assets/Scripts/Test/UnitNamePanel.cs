using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitNamePanel : MonoBehaviour
{
    public TextMeshProUGUI Text;

    public void SetText(string name = "") => Text.text = name;

    private void OnEnable()
    {
        Text.text = UnitInspector.Instance.CurrentUnit.unitName;
    }

    private void OnDisable()
    {
        Text.text = "";
    }
}
