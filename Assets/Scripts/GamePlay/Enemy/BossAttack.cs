using UnityEngine;

namespace DungTran31.GamePlay.Enemy
{
    public class BossAttack : MonoBehaviour
    {
        [SerializeField] private float damage = 100f;
        [SerializeField] private float dashSpeed = 20f;
        [SerializeField] private float dashDistance = 5f;
        [SerializeField] private float attackCooldown = 2f;

        private Transform target;
        private Vector2 attackStartPosition;
        internal bool isDashing = false;
        internal bool isReturning = false;
        private bool hasCollided = false;
        private float cooldownTimer = 0f;

        private void Start()
        {
            GetTarget();
        }

        private void Update()
        {
            if (Dialogues.DialogueManager.Instance.DialogueIsPlaying) return;

            if (!target)
            {
                GetTarget();
                hasCollided = false; // Reset the flag when a new target is acquired
            }

            if (cooldownTimer > 0)
            {
                cooldownTimer -= Time.deltaTime;
                return;
            }

            Attack();
        }

        private void Attack()
        {
            if (isDashing)
            {
                DashTowardsPlayer();
            }
            else if (isReturning)
            {
                ReturnToAttackStartPosition();
            }
            else if (target && Vector2.Distance(transform.position, target.position) <= dashDistance)
            {
                StartDash();
            }
        }

        private void GetTarget()
        {
            if (target != null)
            {
                target = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }

        private void StartDash()
        {
            isDashing = true;
            attackStartPosition = transform.position; // Store the position where the boss starts the attack
        }

        private void DashTowardsPlayer()
        {
            MoveTowards(target.position);

            if (Vector2.Distance(transform.position, target.position) <= 0.1f)
            {
                isDashing = false;
                isReturning = true;
                cooldownTimer = attackCooldown;
            }
        }

        private void ReturnToAttackStartPosition()
        {
            MoveTowards(attackStartPosition);

            if (Vector2.Distance(transform.position, attackStartPosition) <= 0.1f)
            {
                isReturning = false;
            }
        }

        private void MoveTowards(Vector2 targetPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, dashSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player") && !hasCollided)
            {
                collision.gameObject.GetComponent<Player.PlayerHealth>().TakeDamage(damage);
                hasCollided = true; // Set the flag to true after processing the collision
            }
        }
    }
}
