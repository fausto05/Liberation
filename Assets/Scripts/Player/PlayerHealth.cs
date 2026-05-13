using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public int health = 100;

    public void TakeDamage(int damage)
    {
        health -= damage;

        Debug.Log("Player damaged");

        if (health <= 0)
        {
            Debug.Log("Player Dead");
        }
    }
}
