using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health;

    private int _maxHealth;

    public static event UnityAction Died;
    public event UnityAction<int> HealthChanged;

    private void Start()
    {
        HealthChanged?.Invoke(_health);
        _maxHealth = _health;
    }

    private void Die()
    {
        Died?.Invoke();
    }

    public void ApplyDamage(int damage)
    {
        _health -= damage;
        HealthChanged?.Invoke(_health);

        if (_health <= 0)
            Die();
    }

    public void AddHealth(int addingHealth)
    {
        if (_health + addingHealth <= _maxHealth)
        {
            _health += addingHealth;
            HealthChanged?.Invoke(_health);
        }
    }
}