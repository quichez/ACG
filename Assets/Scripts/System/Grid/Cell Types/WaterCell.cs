using ACG.Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCell : Cell, ICellResources, IImprovable
{
    public override Color Highlighted => Color.blue + Color.yellow / 2.0f;

    public List<Resource> CellResources { get; private set; } = new();

    public bool IsImproved => throw new System.NotImplementedException();

    public void CreateImprovement(ImprovementUnit improvementUnit)
    {
        throw new System.NotImplementedException();
    }

    public override string GetCellType() => "Water";
}
