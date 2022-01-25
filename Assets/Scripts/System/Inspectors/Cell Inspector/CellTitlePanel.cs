using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CellTitlePanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;

    public void SetCellTitleText(string title)
    {
        _text.text = title;
    }
}
