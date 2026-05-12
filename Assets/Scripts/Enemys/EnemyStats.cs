using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Scriptable Objects/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    public float moveSpeed;
    public int maxHealth;
    public int damage;
    public float attackRange;
    public float attackCooldown;
}
