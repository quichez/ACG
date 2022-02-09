using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingControllerPanel : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI _text;
    [SerializeField] Transform _buildingButtonsParent;
    [SerializeField] CellActionButton _actionButtonPrefab;
    private void OnEnable()
    {
        if(SettlementInspector.Instance.CurrentSettlement.TryGetComponent(out BuildingController buildingController))
        {
            foreach (var item in buildingController.GetBuildingsNotUnlocked())
            {
                CellActionButton clone = Instantiate(_actionButtonPrefab, _buildingButtonsParent);
                clone.SetCellAction(() => buildingController.StartBuilding(item), item.Name);
            }
        }
    }

    private void OnDisable()
    {
        foreach (Transform child in _buildingButtonsParent)
        {
            Destroy(child.gameObject);
        }
    }

    private void Update()
    {
        if(SettlementInspector.Instance.CurrentSettlement.TryGetComponent(out BuildingController buildingController))
        {
            if (buildingController.buildingInProcess != null) _text.text = buildingController.buildingInProcess.Name;
            else _text.text = "No Building In Process";
        }
    }
}
