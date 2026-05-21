using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    private PlayerRangedAttack rangedAttack;
    private bool isFiring;

    private void Awake()
    {
        rangedAttack = GetComponent<PlayerRangedAttack>();
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
}
