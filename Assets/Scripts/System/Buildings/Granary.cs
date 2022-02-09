using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ACG.Buildings;
using System;

[RequireComponent(typeof(Village))]
public class Granary : Building
{
    public override string Name => "Granary";

    public override string Description => "Description";

    public override Type SettlementType => typeof(Village);

    public override int purchaseCost => 100;

    public override int industryCost => 20;

    public override void Unlock(int amt)
    {
        Debug.Log("I was unlocked");
    }

}
