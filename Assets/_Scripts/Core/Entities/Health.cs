using UnityEngine;
using System;

namespace KingOfGuns.Core.Entities
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int _maxHealth;
        private int _currentHealth;

        public Action OnTakeDamage;
        public Action OnDie;

        private void Awake() => _currentHealth = _maxHealth;

        public void Reset() => _currentHealth = _maxHealth;

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;

            if (_currentHealth <= 0)
                Die();

            OnTakeDamage?.Invoke();
        }

        public void Die() => OnDie?.Invoke();
    }
}