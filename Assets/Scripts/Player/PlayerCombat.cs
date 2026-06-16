using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayer;

    private PlayerRangedAttack rangedAttack;
    private PlayerMeleeAttack meleeAttack;
    private PlayerDash playerDash;
    private PlayerSpecialAttack specialAttack;

    private bool isFiring;

    private void Awake()
    {
        rangedAttack = GetComponent<PlayerRangedAttack>();
        meleeAttack = GetComponent<PlayerMeleeAttack>();
        playerDash = GetComponent<PlayerDash>();
        specialAttack = GetComponent<PlayerSpecialAttack>();
    }

    private void OnEnable()
    {
        meleeAttack.OnHitConnected += specialAttack.RegisterHit;
    }

    private void OnDisable()
    {
        meleeAttack.OnHitConnected -= specialAttack.RegisterHit;
    }

    private void Update()
    {
        if (isFiring)
        {
            SmartAttack();
        }
    }

    private void SmartAttack()
    {
        if (meleeAttack.HasEnemyInRange())
        {
            meleeAttack.TryAttack();
            return;
        }

        rangedAttack.TryFire();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
            isFiring = true;

        if (context.canceled)
            isFiring = false;
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            playerDash.TryDash();
        }
    }

    public void OnSpecialAttack(InputAction.CallbackContext context)
    {
        if (context.started)
            specialAttack.TryUseSpecial();
    }
}
