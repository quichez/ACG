using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ACG.Inspectors;
public class UnitCellActionsExtraPanel : InspectorPanel
{
    public UnitCellImprovementsExtraPanel unitCellImprovementsExtraPanel => GetComponentInChildren<UnitCellImprovementsExtraPanel>(true);

    public void EnableImprovementsExtraPanel(bool enb) => unitCellImprovementsExtraPanel.gameObject.SetActive(enb);

    private void OnDisable()
    {
        EnableImprovementsExtraPanel(false);
    }
}
