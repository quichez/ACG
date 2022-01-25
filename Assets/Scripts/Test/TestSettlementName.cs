using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TestSettlementName : MonoBehaviour
{
    public TextMeshProUGUI Text;

    public void SetText(string name = "") => Text.text = name;
}
