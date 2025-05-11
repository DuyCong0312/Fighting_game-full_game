using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class KnockBack : MonoBehaviour
{
    [Header("KnockBack")]
    [SerializeField] private float knockBackTime = 0.5f;
    [SerializeField] private float hitDirectionForce = 2f;

    [Header("BlowUp")]
    [SerializeField] private Transform opponentDirection;
    [SerializeField] private float blowUpPowerX = 1f;
    [SerializeField] private float blowUpPowerY = 1.5f;

    private PlayerState playerState;
    private CheckGround groundCheck;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerState = GetComponent<PlayerState>();
    }

    public void KnockBackAction(Vector2 hitDirection)
    {
        StartCoroutine(KnockBackRoutine(hitDirection));
    }
    public void BlowUpAction()
    {
        BlowUp();
    }

    private IEnumerator KnockBackRoutine(Vector2 hitDirection)
    {
        Vector2 hitForce = hitDirection * hitDirectionForce;
        float elapsedTime = 0f;
        while (elapsedTime < knockBackTime)
        {
            rb.velocity = new Vector2(hitForce.x, hitForce.y * 5f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector2.zero;
    }

    private void BlowUp()
    {
        Vector2 blowDirection = new Vector2(opponentDirection.transform.right.x, this.transform.up.y).normalized;
        Vector2 blowForce = new Vector2(blowDirection.x * blowUpPowerX, blowDirection.y * blowUpPowerY);
        rb.AddForce(blowForce, ForceMode2D.Impulse);
    }
}
