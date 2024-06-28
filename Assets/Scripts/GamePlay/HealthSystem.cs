using DungTran31.GamePlay.Player;
using System;
using UnityEngine;

namespace DungTran31.GamePlay
{
    public class HealthSystem : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 3;
        private int currentHealth;
        public bool isDead { get; private set;}
        private void Start()
        {
            isDead = false;
            currentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Die(true);
            }
        }

        public void Die(bool state)
        {
            isDead = state;
        }
    }
}
