using System;
using System.Collections;
using UnityEngine;

namespace DungTran31.GamePlay.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [Header("Health")]
        [SerializeField] private GameObject deathEffect;
        public float maxHealth { get; private set; } = 400;
        public float currentHealth { get; private set; }

        [Header("iFrames")]
        [SerializeField] private float iFrameDuration = 2f;
        [SerializeField] private int numberOfFlashes = 3;
        [SerializeField] private bool isInvulnerable = false;
        private SpriteRenderer spriteRenderer;

        private void Start()
        {
            spriteRenderer = GameObject.FindGameObjectWithTag("PlayerHead").GetComponent<SpriteRenderer>();
            currentHealth = maxHealth;
        }

        public void TakeDamage(float amount)
        {
            if (isInvulnerable) return;
            currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
            if (currentHealth <= 0)
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
            currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
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


