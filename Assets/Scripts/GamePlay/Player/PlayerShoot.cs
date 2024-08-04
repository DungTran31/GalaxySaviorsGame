using UnityEngine;
using DungTran31.Utilities;
using DungTran31.UI;
using DungTran31.Core;

namespace DungTran31.GamePlay.Player
{
    public class PlayerShoot : MonoBehaviour
    {
        [SerializeField] private Transform firingPoint;
        [Range(0.1f, 2f)]
        [SerializeField] private float fireRate = 0.5f;

        private float fireTimer;
        // Define bullet types
        public enum BulletType { fire, poison, ice, black }
        private BulletType currentBulletType = BulletType.fire;
        private BulletTypeUI bulletTypeUI;

        private void Start()
        {
            bulletTypeUI = FindObjectOfType<BulletTypeUI>();
            UpdateBulletTypeUI();
        }

        private void Update()
        {
            if (Dialogues.DialogueManager.Instance.DialogueIsPlaying) return;

            if (Input.GetKeyDown(KeyCode.Space) && fireTimer <= 0f)
            {
                AudioManager.Instance.PlaySfx(AudioManager.Instance.shoot);
                Shoot();
                fireTimer = fireRate;
            }
            else
            {
                fireTimer -= Time.deltaTime;
            }

            // Switch bullet type forward
            if (Input.GetKeyDown(KeyCode.E))
            {
                SwitchBulletType(true);
            }
            // Switch bullet type backward
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SwitchBulletType(false);
            }
        }

        private void Shoot()
        {
            // Use the current bullet type to determine which bullet to shoot
            string poolTag = currentBulletType.ToString().ToLower() + "Bullet"; // Assuming your pool tags are named accordingly
            GameObject bullet = ObjectPooler.Instance.SpawnFromPool(poolTag, firingPoint.position, firingPoint.rotation);

            if (bullet != null)
            {
                if (bullet.TryGetComponent<TrailRenderer>(out var trailRenderer))
                {
                    // Ensure the TrailRenderer is properly reset or managed
                    trailRenderer.Clear();
                }
            }
        }

        private void SwitchBulletType(bool forward)
        {
            AudioManager.Instance.PlaySfx(AudioManager.Instance.switched);
            int bulletTypeCount = System.Enum.GetValues(typeof(BulletType)).Length;
            currentBulletType = (BulletType)(((int)currentBulletType + (forward ? 1 : -1) + bulletTypeCount) % bulletTypeCount);
            UpdateBulletTypeUI();
            Debug.Log("Switched to bullet type: " + currentBulletType);
        }
        private void UpdateBulletTypeUI()
        {
            if (bulletTypeUI != null)
            {
                bulletTypeUI.UpdateBulletTypeUI((int)currentBulletType);
            }
        }
    }
}
