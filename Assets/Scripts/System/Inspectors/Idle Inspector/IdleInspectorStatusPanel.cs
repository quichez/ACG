using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IdleInspectorStatusPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _moneyText;
    [SerializeField] TextMeshProUGUI _happinessText;

    private void Update()
    {
        _moneyText.text = "$: " + Player.Instance.GetTotalMoney().ToString();
                
        _happinessText.text = ":) : " + ColoredHappinessText();
    }

    private string ColoredHappinessText()
    {
        if (Player.Instance.GetTotalHappiness() > 0)
        {
            return "<color=#00FF00>" + Player.Instance.GetTotalHappiness().ToString() + "</color>";
        }
        else if (Player.Instance.GetTotalHappiness() < 0)
        {
            return "<color=#FF0000>" + Player.Instance.GetTotalHappiness().ToString() + "</color>";
        }
        else
        {
            return "<color=#FFFFFF>" + Player.Instance.GetTotalHappiness().ToString() + "</color>";
        }
    }
}
