using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Settlement))]
public class MoneyMonobehavior : MonoBehaviour
{
    Settlement _settlement => GetComponent<Settlement>();
    
    
    [SerializeField] private int _startingMoney = 5;

    private Money _money;

    private void Start()
    {
        _money = new Money(_startingMoney);
        TurnManager.Instance.SubscribeToTurnManager(UseMoney);
    }

    public void UseMoney()
    {

    }

}
