using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ACG.Inspectors;

public class UnitCellActionsPanel : MonoBehaviour
{
    [SerializeField] CellActionButton _actionButton;
    UnitCellActionsExtraPanel _unitCellActionsExtraPanel => UnitCellInspector.Instance.unitCellActionsExtraPanel;
    public void FillPanelWithButtons(Cell cell)
    {
        CellActionButton createSettlement = Instantiate(_actionButton, transform);
        createSettlement.SetCellText("Create Settlement");

        CellActionButton createImprovement = Instantiate(_actionButton, transform);
        SetCellImproveActionButton(createImprovement);


        if (cell is ISettleable settle)
        {
            createSettlement.SetCellAction(() => settle.CreateSettlement(SettlementManager.Instance.SettlementUnit));            
        }

        if(cell is IImprovable improvable)
        {
            createImprovement.SetCellAction(() => SetCellImproveActionButton(createImprovement));
        }

    }

    public void ClearPanelButtons()
    {
        foreach (Transform item in transform)
        {
            Destroy(item.gameObject);
        }
	}

    private void SetCellImproveActionButton(CellActionButton button)
    {
        _unitCellActionsExtraPanel.gameObject.SetActive(false);
        button.SetCellText("Create Improvement");
        button.SetCellAction(() => ToggleUnitCellActionsExtraPanel(_unitCellActionsExtraPanel.unitCellImprovementsExtraPanel));
    }

    private void ToggleUnitCellActionsExtraPanel(InspectorPanel extraPanel)
    {
        bool panelWasActive = _unitCellActionsExtraPanel.unitCellImprovementsExtraPanel.active;

        foreach (Transform transform in _unitCellActionsExtraPanel.transform)
        {
            transform.gameObject.SetActive(false);
        }

        if (panelWasActive)
        {
            _unitCellActionsExtraPanel.gameObject.SetActive(false);
        }
        else
        {
            _unitCellActionsExtraPanel.gameObject.SetActive(true);
            _unitCellActionsExtraPanel.EnableImprovementsExtraPanel(true);
        }
    }
}
