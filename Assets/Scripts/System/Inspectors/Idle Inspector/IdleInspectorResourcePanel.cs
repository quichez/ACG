using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IdleInspectorResourcePanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _resourceNameText;
    [SerializeField] TextMeshProUGUI _resourceAmountText;

    private void OnEnable()
    {
        string[] resourceText = Player.Instance.GetTotalResourcesAsString();
        _resourceNameText.text = resourceText[0];
        _resourceAmountText.text = resourceText[1];
    }

    private void OnDisable()
    {
        _resourceNameText.text = "";
        _resourceAmountText.text = "";
    }
}
