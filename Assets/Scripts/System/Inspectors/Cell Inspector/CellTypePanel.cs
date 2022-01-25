using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CellTypePanel : MonoBehaviour
{
    TextMeshProUGUI _text => GetComponentInChildren<TextMeshProUGUI>();
    public void SetCellTypeText(string type) => _text.text = type;
}
