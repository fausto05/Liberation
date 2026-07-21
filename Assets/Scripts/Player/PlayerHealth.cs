using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public int health = 100;
    private int maxHealth = 100;
    public Color CurrentHealthColor { get; private set; }
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private PlayerMovement playerMovement;
    private DamageFlash damageFlash;

    private bool isDead = false;
    public bool IsDead => isDead;

    private Color originalColor;
    private Color grayColor = new Color(0.5f, 0.5f, 0.5f, 1f);

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();
        damageFlash = GetComponent<DamageFlash>();

        originalColor = spriteRenderer.color;
    }

    private void Start()
    {
        SaveData data = SaveSystem.Load();

        health = data.playerHealth;

        health = Mathf.Clamp(health, 1, maxHealth);

        UpdateSpriteColor();
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;

        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);

        SaveData data = SaveSystem.Load();
        data.playerHealth = health;
        SaveSystem.Save(data);

        UpdateSpriteColor();

        damageFlash?.Flash();

        if (health <= 0)
        {
            isDead = true;
            playerMovement.CanMove = false;
            animator.SetTrigger("Death");
        }
    }

    private void UpdateSpriteColor()
    {

        float healthPercent = (float)health / maxHealth;

        CurrentHealthColor = Color.Lerp(
            grayColor,
            originalColor,
            healthPercent
        );

        spriteRenderer.color = CurrentHealthColor;
    }

    public void OnDeathAnimationEnd()
    {
        GameManager.Instance.PlayerDied();
        Destroy(gameObject);
    }

    public void Heal(int amount)
    {
        if (isDead)
            return;

        health += amount;

        health = Mathf.Clamp(health, 0, maxHealth);

        SaveData data = SaveSystem.Load();

        data.playerHealth = health;

        SaveSystem.Save(data);

        UpdateSpriteColor();
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
}
