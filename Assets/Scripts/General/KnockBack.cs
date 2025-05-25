using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class KnockBack : MonoBehaviour
{
    [Header("KnockBack")]
    //[SerializeField] private float knockBackTime = 0.5f;
    [SerializeField] private float hitDirectionForceX = 2f;
    [SerializeField] private float hitDirectionForceY = 2f;

    [Header("BlowUp")]
    public Transform opponentDirection;
    [SerializeField] private float blowUpPowerX = 1f;
    [SerializeField] private float blowUpPowerY = 1.5f;

    private PlayerState playerState;
    private CheckGround groundCheck;
    private Rigidbody2D rb;
    public enum KnockbackType { Linear, Arc, BlownUp }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerState = GetComponent<PlayerState>();
    }

    public void KnockBackAction(Vector2 direction, KnockbackType type)
    {
        switch (type)
        {
            case KnockbackType.Linear:
                KnockBackLinear(direction);
                break;
            case KnockbackType.Arc:
                KnockBackArc(direction);
                break;
            case KnockbackType.BlownUp:
                BlownUp(direction);
                break;
        }
    }

    private void KnockBackLinear(Vector2 hitDirection)
    {
        Vector2 hitForce = hitDirection * hitDirectionForceX;
        rb.AddForce(hitForce, ForceMode2D.Impulse);
    }

    private void KnockBackArc(Vector2 hitDirection)
    {
        Vector2 hitForce;
        hitForce.x = hitDirection.x * hitDirectionForceX;
        hitForce.y = hitDirection.y * hitDirectionForceY;
        rb.AddForce(hitForce, ForceMode2D.Impulse);
    }

    private void BlownUp(Vector2 hitDirection)
    {
        Vector2 direction = hitDirection.normalized;
        Vector2 blowForce = new Vector2(direction.x * blowUpPowerX, direction.y * blowUpPowerY);
        rb.AddForce(blowForce, ForceMode2D.Impulse);
    }

    //public void KnockBackAction(Vector2 hitDirection)
    //{
    //    StartCoroutine(KnockBackLinear(hitDirection));
    //}

    //private IEnumerator KnockBackLinear(Vector2 hitDirection)
    //{
    //    Vector2 hitForce = hitDirection * hitDirectionForceX;
    //    float elapsedTime = 0f;
    //    while (elapsedTime < knockBackTime)
    //    {
    //        rb.velocity = new Vector2(hitForce.x, hitForce.y * 5f);
    //        elapsedTime += Time.deltaTime;
    //        yield return null;
    //    }

    //    rb.velocity = Vector2.zero;
    //}
}
