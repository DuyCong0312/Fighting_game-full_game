using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEffect : MonoBehaviour
{
    private void EndStartPose()
    {
        GameManager.Instance.canMoveExtraCam = true;
    }
}
