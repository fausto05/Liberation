using UnityEngine;

public class HealthPickup : PickupBase
{
    [SerializeField] private int healAmount = 50;

    protected override bool OnPickup(GameObject player)
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

        if (playerHealth == null)
            return false;

        if (playerHealth.GetHealth() >= playerHealth.GetMaxHealth())
            return false;

        playerHealth.Heal(healAmount);

        Debug.Log("Curado: " + healAmount);

        return true;
    }
}