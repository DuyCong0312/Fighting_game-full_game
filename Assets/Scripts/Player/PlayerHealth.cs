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
    private float damageThreshold = 20f;  
    private float damageTimeLimit = 2f; 
    private float accumulatedDamage = 0f;
    private float timeSinceLastDamage = 0f;
    void Start()
    {
        StartCoroutine(InitializeComponentsInChildren());
        knockBack = GetComponent<KnockBack>();
        playerState = GetComponent<PlayerState>();
        playerRage = GetComponent<PlayerRage>();
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

    private void Update()
    {
        if (timeSinceLastDamage < damageTimeLimit)
        {
            timeSinceLastDamage += Time.deltaTime;
        }
        else
        {
            accumulatedDamage = 0;
        }
        CheckHeavyHurt();
    }
    public void TakeDamage(float damage, Vector2 direction)
    {
        playerState.isAttacking = false;
        playerState.isUsingSkill = false;

        if (playerState.isDefending)
        {
            damage = 2f;
            currentHealth -= damage;
            knockBack.KnockBackAction(direction / 2f);
            playerRage.GetRage(2f);
        }
        else
        {
            currentHealth -= damage;
            playerState.isGettingHurt = true;
            if (currentHealth <= 0)
            {
                PlayHeavyHurt();
            }
            else
            {
                anim.SetTrigger(CONSTANT.getHurt);
                knockBack.KnockBackAction(direction);
            }
            playerRage.GetRage(5f);
            accumulatedDamage += damage;
            timeSinceLastDamage = 0f;
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

    private void CheckHeavyHurt()
    {
        if (accumulatedDamage >= damageThreshold && timeSinceLastDamage<damageTimeLimit)
        {
            PlayHeavyHurt();
        }
    }

    public void PlayHeavyHurt()
    {
        anim.Play(animationHeavyHurtName);
        knockBack.BlowUpAction();
        playerState.isGettingHurt = true;
        accumulatedDamage = 0f;
    }
}
