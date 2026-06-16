using UnityEngine;

public class PanelDamageable : MonoBehaviour, IDamageable
{
    [SerializeField] private int health = 3;

    public void TakeDamage(int damage)
    {
        health -= damage;

        Debug.Log($"{gameObject.name} recibió daño. Vida restante: {health}");

        if (health <= 0)
        {
            DestroyPanel();
        }
    }

    private void DestroyPanel()
    {
        Debug.Log($"{gameObject.name} destruido");

        GameEvents.OnPanelDestroyed?.Invoke();

        Destroy(gameObject);
    }

    private void OnEnable()
    {
        GameEvents.OnPanelDestroyed += Test;
    }

    private void OnDisable()
    {
        GameEvents.OnPanelDestroyed -= Test;
    }

    private void Test()
    {
        Debug.Log("PANEL DESTRUIDO EVENTO");
    }
}