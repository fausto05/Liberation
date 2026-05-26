using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
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
            rangedAttack.TryFire();
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.started)
            isFiring = true;

        if (context.canceled)
            isFiring = false;
    }

    public void OnMelee(InputAction.CallbackContext context)
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
}
