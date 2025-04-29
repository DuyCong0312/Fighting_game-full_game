using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RageBar : MonoBehaviour
{
    [SerializeField] private Slider rageBar;

    public void UpdateRageBar(float currentRage, float maxRage)
    {
        rageBar.value = currentRage / maxRage;
    }
}

