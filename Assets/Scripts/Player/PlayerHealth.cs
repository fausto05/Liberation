using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public int health = 100;

    public void TakeDamage(int damage)
    {
        Debug.Log("Daño recibido: " + damage);

        health -= damage;

        Debug.Log("Vida actual: " + health);
        
        if (health <= 0)
        {
            GameManager.Instance.PlayerDied();

            Destroy(gameObject);
        }
    }
}
