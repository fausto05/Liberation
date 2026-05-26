using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayer;

    private PlayerRangedAttack rangedAttack;
    private PlayerMeleeAttack meleeAttack;
    private PlayerDash playerDash;

    private bool isFiring;

    private void Awake()
    {
        rangedAttack = GetComponent<PlayerRangedAttack>();
        meleeAttack = GetComponent<PlayerMeleeAttack>();
        playerDash = GetComponent<PlayerDash>();
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
            Debug.Log("MELEE");
            meleeAttack.TryAttack();
        }
        else
        {
            Debug.Log("RANGED");
            rangedAttack.TryFire();
        }
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
}
