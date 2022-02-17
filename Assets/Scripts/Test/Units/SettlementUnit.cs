using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ACG.Inspectors;

public class SettlementUnit : Unit, ILinkableUnit, IInspectable
{
    public int population { get; private set; } = 5;
    public int foodPoints { get; private set; }
    public int industryPoints { get; private set; }
    public int localHappiness { get; private set; }
    public int localUnhappiness { get; private set; }
    public int localMoney { get; private set; } = 10;    

    public override void DestroyUnit()
    {

        base.DestroyUnit();
    }
}
