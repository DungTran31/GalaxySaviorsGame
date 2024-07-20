using System;
using System.Collections;
using UnityEngine;

namespace DungTran31.GamePlay.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [Header("Health")]
        [SerializeField] private GameObject deathEffect;
        public float MaxHealth { get; private set; } = 400;
        public float CurrentHealth { get; private set; }

        [Header("iFrames")]
        [SerializeField] private float iFrameDuration = 2f;
        [SerializeField] private int numberOfFlashes = 3;
        [SerializeField] private bool isInvulnerable = false;
        private SpriteRenderer spriteRenderer;

        private void Start()
        {
            spriteRenderer = GameObject.FindGameObjectWithTag("PlayerHead").GetComponent<SpriteRenderer>();
            CurrentHealth = MaxHealth;
        }

        public void TakeDamage(float amount)
        {
            if (isInvulnerable) return;
            CurrentHealth = Mathf.Clamp(CurrentHealth - amount, 0, MaxHealth);
            if (CurrentHealth <= 0)
            {
                Instantiate(deathEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            else
            {
                StartCoroutine(Invunerability());
            }
        }

        public void Heal(float amount)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0, MaxHealth);

            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
        }

        public void IncreaseMaxHealth(float amount)
        {
            MaxHealth += amount;
            CurrentHealth = MaxHealth; // Optionally reset current health to max
        }

        private IEnumerator Invunerability()
        {
            // Ignore collisions with enemies
            Physics2D.IgnoreLayerCollision(gameObject.layer, 10, true);
            // Ignore collisions with bullets
            Physics2D.IgnoreLayerCollision(gameObject.layer, 11, true);
            isInvulnerable = true;

            for (int i = 0; i < numberOfFlashes; i++)
            {
                spriteRenderer.color = new Color(1, 1, 1, 0.5f);
                yield return new WaitForSeconds(iFrameDuration / (numberOfFlashes * 2));
                spriteRenderer.color = new Color(1, 1, 1, 1);
                yield return new WaitForSeconds(iFrameDuration / (numberOfFlashes * 2));
            }

            // Re-enable collisions with enemies
            Physics2D.IgnoreLayerCollision(gameObject.layer, 10, false);
            // Re-enable collisions with bullets
            Physics2D.IgnoreLayerCollision(gameObject.layer, 11, false);
            isInvulnerable = false;
        }
    }
}


