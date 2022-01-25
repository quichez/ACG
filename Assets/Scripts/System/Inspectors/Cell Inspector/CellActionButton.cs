using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CellActionButton : MonoBehaviour
{
    Action _cellAction;

    public void SetCellAction(Action act, string text = "")
    {
        _cellAction = act;
        GetComponentInChildren<TextMeshProUGUI>().text = text;
    } 

    public void DoCellAction()
    {
        _cellAction();
    }
}
