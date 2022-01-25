using ACG.Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCell : Cell, ICellResources
{
    public override Color Highlighted => Color.blue + Color.yellow / 2.0f;

    public List<Resource> CellResources { get; private set; } = new();

    public override string GetCellType() => "Water";
}
