using ACG.Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestCell : LandCell, ISettleable, ICellResources
{
    public override Color Highlighted => _sprite.color + Color.white / 4.0f;

    public List<Resource> CellResources { get; private set; } = new();

    public override string GetCellType() => "Forest";

    public void InitializeResources()
    {
        var valOne = (int)(Random.value * 4);
        CellResources.Add(new Lumber(valOne + 1));
    }
}
