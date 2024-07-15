using UnityEngine;

namespace DungTran31.GamePlay.Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        private Transform target;
        private bool hasCollided = false; // Add a flag to check if collision has been processed to ensure only happens once

        private void Start()
        {
            GetTarget();
        }

        private void Update()
        {
            if (!target)
            {
                GetTarget();
                hasCollided = false; // Reset the flag when a new target is acquired
            }
        }

        private void GetTarget()
        {
            if (GameObject.FindGameObjectWithTag("Player") != null)
                target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player") && !hasCollided)
            {
                Destroy(collision.gameObject);
                target = null;
                hasCollided = true; // Set the flag to true after processing the collision
            }
        }
    }
}
