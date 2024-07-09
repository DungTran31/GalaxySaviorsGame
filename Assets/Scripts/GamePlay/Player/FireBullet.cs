using System.Collections;
using UnityEngine;

namespace DungTran31.GamePlay.Player
{
    public class FireBullet : MonoBehaviour
    {
        [SerializeField] private float damage;
        [Range(1, 50)]
        [SerializeField] private float speed = 20f;
        [Range(1, 10)]
        [SerializeField] private float lifeTime = 1f;


        private Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            // Restart the coroutine each time the bullet is activated
            StopAllCoroutines(); // Stop any existing coroutines to avoid duplicates
            StartCoroutine(ReturnToPoolAfterDelay());
        }

        private IEnumerator ReturnToPoolAfterDelay()
        {
            yield return new WaitForSeconds(lifeTime);
            this.gameObject.SetActive(false);
        }

        private void FixedUpdate()
        {
            rb.velocity = transform.right * speed;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Enemy")
            {
                collision.GetComponent<Enemy.EnemyHealth>().TakeFireDamage(damage);
                this.gameObject.SetActive(false);
            }
            else if (collision.tag == "Border")
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
