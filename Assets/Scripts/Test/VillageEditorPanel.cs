using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ACG.Resources;

public class VillageEditorPanel : MonoBehaviour
{ 
    public AddButton addPrefab;
    public RemoveButton removePrefab;

    public void SetPanelInformation(Settlement settlement)
    {
        Clear();

        if (settlement is IOutputResources outResource)
        {
            foreach (string resName in ResourceFactory.GetResourceNames())
            {
                AddButton cloneAdd = Instantiate(addPrefab, transform);
                cloneAdd.InitializeButton(outResource, resName);

                RemoveButton cloneRemove = Instantiate(removePrefab, transform);                
                cloneRemove.InitializeButton(outResource, resName);
            }
        }
    }

    public void Clear()
    {
        foreach (Transform item in transform)
        {
            Destroy(item.gameObject);
        }
    }
}
