using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event EventHandler OnHealthDepleted;
    public event EventHandler<OnHealthChangedEventArgs> OnHealthDamaged;
    public event EventHandler<OnHealthChangedEventArgs> OnHealthHealed;
    public class OnHealthChangedEventArgs : EventArgs
    {
        public int health;
    }


    [SerializeField] private int _healthMax;
    private int _currentHealth;



    private void Awake()
    {
        _currentHealth = _healthMax;
    }



    public void Damage(int damageAmount)
    {
        _currentHealth -= damageAmount;

        OnHealthDamaged?.Invoke(this, new OnHealthChangedEventArgs() {
            health = damageAmount
        });

        if(_currentHealth <= 0)
        {
            OnHealthDepleted?.Invoke(this, EventArgs.Empty);
        }
    }

    public void Heal(int healAmount)
    {
        if(_currentHealth + healAmount > _healthMax)
        {
            healAmount = _healthMax - _currentHealth;
        }

        _currentHealth += healAmount;

        OnHealthDamaged?.Invoke(this, new OnHealthChangedEventArgs() {
            health = healAmount
        });
    }
}
