using System;
using UnityEngine;

public class Player : SingletonPersistent<Player>
{
    public int Money => GetTotalMoney();
    public int GlobalHappiness = 3;
    public int GlobalUnhappiness = 0;

    public int Luck { get; private set; }


    public int GetTotalMoney()
    {
        int total = 0;
        foreach (Settlement settlement in TurnManager.Instance.Settlements)
        {
            if (settlement is IInputResources inputs) total += inputs.FindInputResource(typeof(Money))?.Amount ?? 0;
        }
        return total;
    }  

    public int GetTotalHappiness()
    {
        int total = 0;
        foreach (var item in TurnManager.Instance.Settlements)
        {
            if(item is IHappinessSettlement happyCamper)
            {
                total += happyCamper.CalculateTotalLocalHappiness();
            }
        }
        return total;
    }
}