using UnityEngine;

public class HealthPickup : PickupBase
{
    [SerializeField] private int healAmount = 50;

    protected override void OnPickup(GameObject player)
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

        if (playerHealth == null)
            return;

        playerHealth.Heal(healAmount);

        Debug.Log("Curado: " + healAmount);
    }
}