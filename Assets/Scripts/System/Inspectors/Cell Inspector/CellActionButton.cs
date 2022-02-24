using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CellActionButton : MonoBehaviour
{
    Action _cellAction;

    public Button button => GetComponent<Button>();

    public void SetCellText(string text = "")
    {
        GetComponentInChildren<TextMeshProUGUI>().text = text;
    }

    public void SetCellAction(Action act)
    {
        _cellAction = act;      
        button.interactable = true;
    } 

    public void DoCellAction()
    {
        _cellAction();
    }

    private void OnEnable()
    {
        button.interactable = false;
    }
}

public static class CellActionButtonExtensions
{
    public static void SetCellActionButton(this CellActionButton cellActionButton, string text, Action action, bool cellConditionsAreMet)
    {
        cellActionButton.SetCellText(text);        
        if (cellConditionsAreMet)
        {
            cellActionButton.SetCellAction(action);
        }
    }
}