using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ACG.Inspectors;

public class UnitCellActionsPanel : InspectorPanel
{
    [SerializeField] CellActionButton _actionButton;
    UnitCellActionsExtraPanel _unitCellActionsExtraPanel => UnitCellInspector.Instance.unitCellActionsExtraPanel;
    public void FillPanelWithButtons(Cell cell)
    {
        _unitCellActionsExtraPanel.gameObject.SetActive(false);

        CellActionButton createSettlement = Instantiate(_actionButton, transform);
        createSettlement.SetCellActionButton("Create Settlement", 
            () => (cell as ISettleable).CreateSettlement(SettlementManager.Instance.SettlementUnit), cell is ISettleable);

        CellActionButton createImprovement = Instantiate(_actionButton, transform);
        createImprovement.SetCellActionButton("Create Improvement",
            () => ToggleUnitCellActionsExtraPanel(_unitCellActionsExtraPanel.unitCellImprovementsExtraPanel), cell is IImprovable);

        CellActionButton createBuilding = Instantiate(_actionButton, transform);
        createBuilding.SetCellActionButton("Create Building",
            () => ToggleUnitCellActionsExtraPanel(_unitCellActionsExtraPanel.unitCellBuildingsExtraPanel), cell is IBuildable);
    }

    public void ClearPanelButtons()
    {
        foreach (Transform item in transform)
        {
            Destroy(item.gameObject);
        }
	}

    private void ToggleUnitCellActionsExtraPanel(InspectorPanel extraPanel)
    {
        bool panelWasActive = extraPanel.active;

        foreach (Transform transform in _unitCellActionsExtraPanel.transform)
        {
            transform.gameObject.SetActive(false);
        }

        _unitCellActionsExtraPanel.SetActive(!panelWasActive);

        if (!panelWasActive)
        {
            extraPanel.SetActive(true);
        }        
    }

    private void OnDisable()
    {
        ClearPanelButtons();
        _unitCellActionsExtraPanel.SetActive(false);
    }

}
