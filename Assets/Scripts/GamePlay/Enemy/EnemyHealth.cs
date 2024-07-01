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
        [SerializeField] private GameObject floatingTextPrefab;

        public float currentHealth { get; private set; }
        private void OnEnable()
        {
            currentHealth = maxHealth;
            floatingHealthBar.UpdateHealthBar(currentHealth, maxHealth);
            floatingHealthBar = GetComponentInChildren<FloatingHealthBar>();
        }

        public void TakeDamage(float amount)
        {
            ShowDamage(amount.ToString());
            currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
            floatingHealthBar.UpdateHealthBar(currentHealth, maxHealth);
            if (currentHealth <= 0)
            {
                Instantiate(bloodEffect, transform.position, Quaternion.identity);
                this.gameObject.SetActive(false);
            }
        }

        public void ShowDamage(string text)
        {
            if (floatingTextPrefab)
            {
                GameObject floatingText = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
                floatingText.GetComponentInChildren<TextMesh>().text = text;
            }
        }
    }
}
