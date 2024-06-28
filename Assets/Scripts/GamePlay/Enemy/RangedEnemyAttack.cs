using UnityEngine;
using DungTran31.Utilities;

namespace DungTran31.GamePlay.Enemy
{
    public class RangedEnemyAttack : MonoBehaviour
    {
        [SerializeField] private Transform firingPoint;
        [SerializeField] private float distanceToShoot = 10f;
        [SerializeField] private float fireRate = 0.5f;
        [SerializeField] private float _orbitRadius = 5f;
        private Transform target;
        private bool hasCollided = false; // Add a flag to check if collision has been processed to ensure only happens once

        private float timeToFire;

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
            else
            {
                RotateFiringPoint();
            }
        }

        private void FixedUpdate()
        {
            if (target != null)
            {
                if (Vector2.Distance(transform.position, target.position) <= distanceToShoot)
                {
                    Shoot();
                }
            }
        }

        private void Shoot()
        {
            if(timeToFire <= 0)
            {
                GameObject bullet = ObjectPooler.Instance.SpawnFromPool("enemyBullet", firingPoint.position, firingPoint.rotation);
                timeToFire = fireRate;
            }
            else
            {
                timeToFire -= Time.deltaTime;
            }

        }
        private void RotateFiringPoint()
        {
            Vector2 targetDirection = (target.position - transform.position).normalized;
            // Tính góc giữa gameobject và player
            float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;

            // Xoay FiringPoint theo góc tính được
            firingPoint.rotation = Quaternion.Euler(0, 0, angle);

            // Đặt vị trí của FiringPoint trên đường tròn quanh gameobject
            Vector2 orbitPosition = (Vector2)transform.position + (targetDirection.normalized * _orbitRadius);
            firingPoint.position = orbitPosition;
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
                collision.gameObject.GetComponent<HealthSystem>().Die(true);
                Destroy(collision.gameObject);

                target = null;
                hasCollided = true; // Set the flag to true after processing the collision
            }
        }
    }
}
