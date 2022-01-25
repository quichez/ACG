using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlementManager : MonoBehaviour
{
    public static SettlementManager Instance;

    [SerializeField] Village _village;
    [SerializeField] LumberMill _lumberMill;

    public Village Village => _village;
    public LumberMill LumberMill => _lumberMill;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }
}
