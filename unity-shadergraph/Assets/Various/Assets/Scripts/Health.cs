using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private int health = 100;

    public UnityEvent onDeath;
    public UnityEvent<int> onDamageTaken;
    public UnityEvent<int> onHealthChanged;

    public void LateUpdate()
    {
        if (health <= 0)
        {
            Die();
        }
    }
    public void SetHealth(int newHealth)
    {
        health = newHealth;
        onHealthChanged.Invoke(health);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        onDamageTaken.Invoke(damage);
        onHealthChanged.Invoke(health);
    }

    private void Die()
    {
        onDeath.Invoke();
    }
}
