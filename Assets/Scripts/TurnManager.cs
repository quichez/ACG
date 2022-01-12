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
            IInputResources inputs = settlement as IInputResources;
            IOutputResources outputs = settlement as IOutputResources;
            
            if (inputs != null)
            {
                foreach (IRenewableResource renewable in inputs.InputResources)
                {
                    renewable.RenewResource();
                }                                
            }

            if (outputs != null)
            {
                foreach (Resource temp in outputs.OutputResource)
                {
                    if (temp is IExpenseResource expense)
                    {
                        Resource res = inputs.InputResources.Find(res => res.GetType() == expense.ResourceRequired);
                        res.ChangeResourceAmount(-expense.Cost * (expense as Resource).Amount);

                    }
                }
            }
        }
        TurnTimer = 0.0f;
    }
}
