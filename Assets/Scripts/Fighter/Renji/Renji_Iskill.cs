using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Renji_Iskill : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private GameObject IskillObject;
    [SerializeField] private Transform[] skillTransforms;
    [SerializeField] private float speed;

    private bool objGetSpeed = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void ActiveRenjiIskill()
    {
        StartCoroutine(SpawnAndGiveThemSpeed());
    }
    private IEnumerator SpawnAndGiveThemSpeed()
    {
        List<Renji_dart> darts = new List<Renji_dart>();

        foreach (Transform t in skillTransforms)
        {
            GameObject obj = Instantiate(IskillObject, t.position, t.rotation);

            Renji_dart dart = obj.GetComponent<Renji_dart>();
            if (dart != null)
            {
                dart.SetReturnPos(t);
                darts.Add(dart);
            }

            Projectile proj = obj.GetComponent<Projectile>();
            if (proj != null)
            {
                proj.SetOwner(this.gameObject);
            }
        }

        while (!objGetSpeed)
            yield return null;

        foreach (Renji_dart dart in darts)
        {
            dart.moveSpeed = speed;
            yield return new WaitForSeconds(0.075f);
        }
    }

    private void AcceptForObjGetSpeed()
    {
        StartCoroutine(AcceptForObjGetSpeedEnum());
    }

    private IEnumerator AcceptForObjGetSpeedEnum()
    {
        objGetSpeed = true;
        anim.speed = 0f;
        yield return new WaitForSeconds(2f);
        anim.speed = 1f;
    }
}
