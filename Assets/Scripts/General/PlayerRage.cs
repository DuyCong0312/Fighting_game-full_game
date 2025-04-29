using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRage : MonoBehaviour
{
    [SerializeField] private float maxRage = 100;
    public float currentRage;
    [SerializeField] private RageBar rageBar;

    void Start()
    {
        currentRage = 0;
        rageBar.UpdateRageBar(currentRage, maxRage);
    }
    public void GetRage(float rage)
    {
        if (currentRage >= maxRage)
        {
            return;
        }
        currentRage += rage; 
        rageBar.UpdateRageBar(currentRage, maxRage);    
    }
    
    public void CostRage(float rageCost)
    {
        if (currentRage < rageCost)
        {
            return;
        }
        currentRage -= rageCost;
        rageBar.UpdateRageBar(currentRage, maxRage);

    }
    public void ResetRage()
    {
        currentRage = 0;
        rageBar.UpdateRageBar(currentRage, maxRage);
    }
}
