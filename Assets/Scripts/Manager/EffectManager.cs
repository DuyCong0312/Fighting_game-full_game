using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance { get; private set; }

    public GameObject jump;
    public GameObject groundDash;
    public GameObject airDash;
    public GameObject touchGround;
    public GameObject hit;
    public GameObject slashHit01;
    public GameObject slashHit02;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public void SpawnEffect(GameObject name, Vector2 pos, Quaternion rot)
    {
        Instantiate(name, pos, rot);
    }

    public void SpawnEffectUseTransform(GameObject name, Transform pos, Quaternion rot)
    {
        Instantiate(name, pos.position, rot);
    }
}
