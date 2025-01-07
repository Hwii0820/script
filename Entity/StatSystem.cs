using UnityEngine;

public abstract class StatSystem : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float attackPower;
    public float defense;

    public virtual void InitializeStats(float maxHealth, float attackPower, float defense)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
        this.attackPower = attackPower;
        this.defense = defense;
    }

    public virtual void TakeDamage(float damage)
    {
        float effectiveDamage = damage - defense;
        effectiveDamage = Mathf.Max(effectiveDamage, 0); // 최소 데미지는 0 이상.
        currentHealth -= effectiveDamage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected abstract void Die(); // 상속받는 클래스에서 구현.
}
