using ACG.Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldCell : LandCell, ISettleable, ICellResources, IImprovable, IBuildable
{
    public override Color Highlighted => _unselected + Color.white / 4.0f;

    public List<Resource> CellResources { get; private set; } = new();

    public bool IsImproved => throw new System.NotImplementedException();

    public bool ContainsBuilding => throw new System.NotImplementedException();

    public void CreateImprovement(ImprovementUnit improvementUnit)
    {
        throw new System.NotImplementedException();
    }

    public override string GetCellType() => "Field";

    public void InitializeResources()
    {
        //roll chances for each type of resource
        int valOne = (int)(Random.value * 3);
        int valTwo = (int)(Random.value * 3);        

        if (valOne > 0) CellResources.Add(new Cows(valOne));
        if (valTwo > 0) CellResources.Add(new Chickens(valTwo));
    }
}
