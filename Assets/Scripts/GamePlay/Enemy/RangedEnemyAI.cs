using UnityEngine;
using DungTran31.Utilities;

namespace DungTran31.GamePlay.Enemy
{
    public class RangedEnemyAI : MonoBehaviour
    {
        private enum EnemyState
        {
            Roaming,
            Chase,
            Attack
        }

        [SerializeField] internal float speed = 5f;
        [SerializeField] private float playerAwarenessDistance = 10f;
        [SerializeField] private float distanceToStop = 5f;
        [SerializeField] private Transform firingPoint;
        [SerializeField] private float distanceToShoot = 10f;
        [SerializeField] private float fireRate = 0.5f;
        [SerializeField] private float orbitRadius = 5f;

        private Rigidbody2D rb;
        private Transform target;
        private Vector2 direction;
        private Vector2 targetDirection;
        private float changeDirectionCooldown;
        private bool awareOfPlayer;
        private EnemyState state;
        private float timeToFire;
        private bool hasCollided = false; // Add a flag to check if collision has been processed to ensure only happens once

        private void Awake()
        {
            state = EnemyState.Roaming;
            if (GameObject.FindGameObjectWithTag("Player") != null)
                target = GameObject.FindGameObjectWithTag("Player").transform;
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (Dialogues.DialogueManager.Instance.DialogueIsPlaying) return;
            if (target != null)
                AwarePlayerDistance();
            PreventEnemyGoingOffScreen();
            RotateFiringPoint();
        }

        private void AwarePlayerDistance()
        {
            Vector2 directionTemp = (target.position - transform.position);
            direction = directionTemp.normalized;

            if (directionTemp.magnitude <= playerAwarenessDistance)
            {
                awareOfPlayer = true;
                state = EnemyState.Chase;
            }
            else
            {
                awareOfPlayer = false;
                state = EnemyState.Roaming;
            }
        }

        private void FixedUpdate()
        {
            switch (state)
            {
                case EnemyState.Roaming:
                    Roaming();
                    break;
                case EnemyState.Chase:
                    Chase();
                    break;
                case EnemyState.Attack:
                    Attack();
                    break;
            }
            UpdateTargetDirection();
            SetVelocity();
        }

        private void SetVelocity()
        {
            if (target != null) // Check if the target exists
            {
                float distance = Vector2.Distance(transform.position, target.position);
                if (distance >= distanceToStop)
                {
                    rb.velocity = targetDirection * speed;
                }
                else
                {
                    rb.velocity = Vector2.zero;
                }
            }
            else // If the target doesn't exist, stop moving
            {
                rb.velocity = Vector2.zero;
            }
        }

        private void UpdateTargetDirection()
        {
            if (awareOfPlayer)
            {
                targetDirection = direction;
            }
            else
            {
                HandleRandomDirectionChange();
            }
        }

        private void HandleRandomDirectionChange()
        {
            changeDirectionCooldown -= Time.deltaTime;
            if (changeDirectionCooldown <= 0)
            {
                targetDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                changeDirectionCooldown = Random.Range(1f, 5f);
            }
        }

        private void Roaming()
        {
            // Roaming behavior implementation
            HandleRandomDirectionChange();
        }

        private void Chase()
        {
            // Chase behavior implementation
            if (Vector2.Distance(transform.position, target.position) <= distanceToShoot) // Example attack range
            {
                state = EnemyState.Attack; // Transition to Attack state when close enough to the player
            }
        }

        private void Attack()
        {
            // Attack behavior implementation
            rb.velocity = Vector2.zero; // Stop movement during attack
            Shoot();
        }

        private void Shoot()
        {
            if (timeToFire <= 0)
            {
                ObjectPooler.Instance.SpawnFromPool("enemyBullet", firingPoint.position, firingPoint.rotation);
                timeToFire = fireRate;
            }
            else
            {
                timeToFire -= Time.deltaTime;
            }
        }

        private void RotateFiringPoint()
        {
            if (target == null || firingPoint == null) return; // Add null check for firingPoint

            Vector2 targetDirection = (target.position - transform.position).normalized;
            // Calculate the angle between the gameobject and the player
            float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;

            // Rotate the FiringPoint according to the calculated angle
            firingPoint.rotation = Quaternion.Euler(0, 0, angle);

            // Set the position of the FiringPoint on the circle around the gameobject
            Vector2 orbitPosition = (Vector2)transform.position + (targetDirection.normalized * orbitRadius);
            firingPoint.position = orbitPosition;
        }

        private void PreventEnemyGoingOffScreen()
        {
            // Define the boundaries of the rectangle
            float minX = -48f; // Minimum X coordinate
            float maxX = 48f;  // Maximum X coordinate
            float minY = -34.5f; // Minimum Y coordinate
            float maxY = 34.5f;  // Maximum Y coordinate

            // Clamp the player's position within the defined boundaries
            float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
            float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);

            // Update the player's position to the clamped position
            transform.position = new Vector2(clampedX, clampedY);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player") && !hasCollided)
            {
                Destroy(collision.gameObject);
                target = null;
                hasCollided = true; // Set the flag to true after processing the collision
                state = EnemyState.Roaming; // Transition back to Roaming state after attack
            }
        }
    }
}
