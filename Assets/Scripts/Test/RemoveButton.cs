using UnityEngine;
using UnityEngine.UI;   
using ACG.Resources;
using TMPro;
public class RemoveButton : MonoBehaviour
{
    IOutputResources settlement;
    string resource;

    Button button => GetComponent<Button>();
    public TextMeshProUGUI _text;

    public void InitializeButton(IOutputResources settle, string res)
    {
        settlement = settle;
        resource = res;
        _text.text = "Remove " + resource;
    }

    public void RemoveResources()
    {
        if (settlement.OutputResource.Find(item => item.Name == resource) is Resource temp) 
        {
            if (temp.Amount == 1) settlement.OutputResource.Remove(temp);
            else temp.ChangeResourceAmount(-1);
        }        
    }

    private void Update()
    {
        var checkResource = settlement.OutputResource.Find(item => item.Name == resource);
        button.interactable = checkResource != null;
    }
}