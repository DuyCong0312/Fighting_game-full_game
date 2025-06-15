using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHealth : MonoBehaviour
{
    public GameSettingSO gameSetRuntime;
    [SerializeField] private float maxHealth;
    public float currentHealth;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private string animationHeavyHurtName;

    private Animator anim;
    private KnockBack knockBack;
    private PlayerState playerState;
    private PlayerRage playerRage;
    private PlayerStateMachine playerStateMachine;

    void Start()
    {
        StartCoroutine(InitializeComponentsInChildren());
        knockBack = GetComponent<KnockBack>();
        playerState = GetComponent<PlayerState>();
        playerRage = GetComponent<PlayerRage>();
        playerStateMachine = GetComponent<PlayerStateMachine>();

        maxHealth = gameSetRuntime.playerHealth;
        currentHealth = maxHealth;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    private IEnumerator InitializeComponentsInChildren()
    {
        while (anim == null)
        {
            anim = GetComponentInChildren<Animator>();
            yield return null;
        }
    }

    public void TakeDamage(float damage, Vector2 direction, KnockBack.KnockbackType type)
    {
        playerState.isAttacking = false;
        playerState.isUsingSkill = false;

        if (playerState.isDefending)
        {
            damage = 2f;
            currentHealth -= damage;
            knockBack.KnockBackAction(direction / 2f,type);
            playerRage.GetRage(2f);
        }
        else
        {
            currentHealth -= damage;
            playerState.isGettingHurt = true;
            if (currentHealth <= 0)
            {
                knockBack.KnockBackAction(new Vector2(knockBack.opponentDirection.transform.right.x, this.transform.up.y), KnockBack.KnockbackType.BlownUp);
                PlayHeavyHurt();
            }
            else
            {
                if (type == KnockBack.KnockbackType.BlownUp)
                {
                    PlayHeavyHurt();
                }
                else
                {
                    playerStateMachine.ChangeState(new HurtState(playerStateMachine));
                }

                knockBack.KnockBackAction(direction, type);
            }
            playerRage.GetRage(5f);
        }

        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    public void PlayWinPose()
    {
        StartCoroutine(WaitAndPlayPose(CONSTANT.WinPose));
    }

    public void PlayLosePose()
    {
        StartCoroutine(WaitAndPlayPose(CONSTANT.LosePose));
    }

    private IEnumerator WaitAndPlayPose(string poseName)
    {
        while (playerState.isGettingHurt)
        {
            yield return null;
        }

        anim.Play(poseName);
    }

    public void PlayHeavyHurt()
    {
        anim.Play(animationHeavyHurtName);
        playerState.isGettingHurt = true;
    }
}
