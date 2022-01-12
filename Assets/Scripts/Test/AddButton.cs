using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ACG.Resources;
using TMPro;

public class AddButton : MonoBehaviour
{
    IOutputResources settlement;
    string resource;

    Button button => GetComponent<Button>();
    public TextMeshProUGUI _text;

    bool IsInteractable => settlement.OutputResource.Find(item => item.Name == resource) is null ? true : settlement.OutputResource.Find(item => item.Name == resource).Amount < 10;
    
    public void InitializeButton(IOutputResources settle, string res)
    {
        settlement = settle;
        resource = res;
        _text.text = "Add " + resource;
        button.interactable = IsInteractable;
    }

    public void AddResource()
    {
        if (settlement.OutputResource.Find(item => item.Name == resource) is Resource temp) temp.ChangeResourceAmount(1);
        else settlement.OutputResource.Add(ResourceFactory.GetResource(resource));
    }

    private void Update()
    {
        button.interactable = IsInteractable;
    }
}
