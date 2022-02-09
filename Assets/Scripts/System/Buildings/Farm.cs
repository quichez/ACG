using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ACG.Buildings;
using System;

[RequireComponent(typeof(Granary))]
public class Farm : Building
{
    public override string Name => "Farm";
    public override string Description => "Gain two industry points and two food points per source of animal resource.";
    public override Type SettlementType => typeof(Village);
    public override int purchaseCost => 100;

    public override int industryCost => 30;


    public override void Unlock(int amt)
    {
        Debug.Log("I was unlocked");
    }
}
