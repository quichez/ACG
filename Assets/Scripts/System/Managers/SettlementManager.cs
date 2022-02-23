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



    [Header("Unit Types")]
    [SerializeField] SettlementUnit _settlementUnit;
    [SerializeField] List<ImprovementUnit> _improvementUnits;
    public SettlementUnit SettlementUnit => _settlementUnit;
    public List<ImprovementUnit> ImprovementUnits => _improvementUnits;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }
}
