using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ACG.Resources;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;
    private float TurnTimer = 0.0f;

    public List<Settlement> Settlements { get; } = new();
    
    private void Awake()
    {
        if (Instance != this && Instance != null)
            Destroy(this);
        else Instance = this;

        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (TurnTimer >= 1.0f) TurnCycle();
        TurnTimer += Time.deltaTime;

    }

    private void TurnCycle()
    {
        foreach (Settlement settlement in Settlements)
        {
            if (settlement is IInputResources inputs)
            {
                foreach (IRenewableResource renewable in inputs.InputResources)
                {
                    renewable.RenewResource();
                }
            }

            if(settlement is ILinkableSettlement linkable)
            {
                linkable.GetGoldBonusFromLinkedSettlements();
            }

            if (settlement is IOutputResources outputs)
            {
                outputs.CalculateAndSpendOnExpenseResources();
                outputs.SetAllEffectiveResourceAmounts();
                
            }
        }
        TurnTimer = 0.0f;
    }
}
