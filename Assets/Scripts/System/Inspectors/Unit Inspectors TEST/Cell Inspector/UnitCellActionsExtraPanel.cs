using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ACG.Inspectors;
public class UnitCellActionsExtraPanel : InspectorPanel
{
    public UnitCellImprovementsExtraPanel unitCellImprovementsExtraPanel => GetComponentInChildren<UnitCellImprovementsExtraPanel>(true);
    public UnitCellBuildingsExtraPanel unitCellBuildingsExtraPanel => GetComponentInChildren<UnitCellBuildingsExtraPanel>(true);

    private void OnDisable()
    {
        unitCellImprovementsExtraPanel.SetActive(false);
        unitCellBuildingsExtraPanel.SetActive(false);
    }
}
