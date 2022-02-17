using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ACG.Inspectors;
using ACG.Resources;

public class ImprovementUnit : Unit, ILinkableUnit, IInspectable
{
    public int localMoney { get; private set; } = 10;

}
