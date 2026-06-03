using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public int health = 100;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            GameManager.Instance.PlayerDied();

            Destroy(gameObject);
        }
    }
}
