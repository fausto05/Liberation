using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayer;

    private PlayerRangedAttack rangedAttack;
    private PlayerMeleeAttack meleeAttack;
    private PlayerDash playerDash;
    private PlayerSpecialAttack specialAttack;

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

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            rangedAttack.TryFire();
        }
    }

    public void OnMeleeAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            meleeAttack.TryAttack();
        }
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
