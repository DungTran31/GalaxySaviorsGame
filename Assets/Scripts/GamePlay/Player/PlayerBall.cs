using DungTran31.GamePlay.Enemy;
using System;
using UnityEngine;

namespace DungTran31.GamePlay.Player
{
    public class PlayerBall : MonoBehaviour
    {
        [SerializeField] private float damage = 100f;
        [SerializeField] private GameObject chain;

        private void Start()
        {
            gameObject.SetActive(false);
            chain.SetActive(false);
        }

        public void ActivatePlayerBall()
        {
            this.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
            gameObject.SetActive(true);
            chain.SetActive(true);
        }

        private void Update()
        {
            if (Dialogues.DialogueManager.Instance.DialogueIsPlaying) return;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                if (collision.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth))
                {
                    enemyHealth.TakeDamage(damage);
                }
                else if (collision.TryGetComponent<BossHealth>(out BossHealth bossHealth))
                {
                    bossHealth.TakeDamage(damage);
                }
            }
        }
    }
}
