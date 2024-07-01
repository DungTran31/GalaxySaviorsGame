using DungTran31.UI;
using UnityEngine;

namespace DungTran31.GamePlay.Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        [Header("Health")]
        [SerializeField] private float maxHealth = 2f;
        [SerializeField] private GameObject bloodEffect;
        [SerializeField] FloatingHealthBar floatingHealthBar;
        public float currentHealth { get; private set; }
        private void Start()
        {
            currentHealth = maxHealth;
            floatingHealthBar.UpdateHealthBar(currentHealth, maxHealth);

            floatingHealthBar = GetComponentInChildren<FloatingHealthBar>();
        }

        public void TakeDamage(float amount)
        {
            currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
            floatingHealthBar.UpdateHealthBar(currentHealth, maxHealth);
            if (currentHealth <= 0)
            {
                Instantiate(bloodEffect, transform.position, Quaternion.identity);
                this.gameObject.SetActive(false);
            }
        }
    }
}
