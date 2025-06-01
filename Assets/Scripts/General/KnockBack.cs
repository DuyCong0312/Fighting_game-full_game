using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class KnockBack : MonoBehaviour
{
    [Header("Knock Back Linear")]
    [SerializeField] private float knockBackTime = 0.5f;
    [SerializeField] private float knockBackLinearForce = 2f;

    [Header("Knock Back Arc")]
    [SerializeField] private float knockBackArcForceX = 2f;
    [SerializeField] private float knockBackArcForceY = 2f;

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
        StartCoroutine(KnockBackLinearEnu(hitDirection));
    }

    private void KnockBackArc(Vector2 hitDirection)
    {
        Vector2 hitForce;
        hitForce.x = hitDirection.x * knockBackArcForceX;
        hitForce.y = hitDirection.y * knockBackArcForceY;
        rb.AddForce(hitForce, ForceMode2D.Impulse);
    }

    private void BlownUp(Vector2 hitDirection)
    {
        Vector2 direction = hitDirection.normalized;
        Vector2 blowForce = new Vector2(direction.x * blowUpPowerX, direction.y * blowUpPowerY);
        rb.AddForce(blowForce, ForceMode2D.Impulse);
    }
    private IEnumerator KnockBackLinearEnu(Vector2 hitDirection)
    {
        Vector2 hitForce = hitDirection * knockBackLinearForce;
        float elapsedTime = 0f;
        while (elapsedTime < knockBackTime)
        {
            rb.velocity = new Vector2(hitForce.x, hitForce.y * 5f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector2.zero;
    }
}
