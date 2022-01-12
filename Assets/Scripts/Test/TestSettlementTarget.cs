using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSettlementTarget : MonoBehaviour
{
    private void OnMouseEnter()
    {
        Debug.Log("hi");
        GetComponent<Renderer>().material.color = Color.red;
    }

    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }
}
