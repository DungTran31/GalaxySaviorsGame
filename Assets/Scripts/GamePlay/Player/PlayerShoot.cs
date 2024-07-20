using UnityEngine;
using DungTran31.Utilities;
using DungTran31.UI;

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
        private readonly Color[]  bulletColors = new Color[]
        {
            new (247f / 255f, 118f / 255f, 34f / 255f), // fire #F77622
            new (99f / 255f, 199f / 255f, 77f / 255f), // poison #63C74D
            new (57f / 255f, 174f / 255f, 224f / 255f), // ice #39AEE0
            new (38f / 255f, 43f / 255f, 68f / 255f) // black #262B44
        };

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
                Shoot();
                fireTimer = fireRate;
            }
            else
            {
                fireTimer -= Time.deltaTime;
            }

            // Switch bullet type forward
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SwitchBulletType(true);
            }
            // Switch bullet type backward
            if (Input.GetKeyDown(KeyCode.E)) // Assuming E is the key to switch to the previous bullet type
            {
                SwitchBulletType(false);
            }
        }

        private void Shoot()
        {
            // Use the current bullet type to determine which bullet to shoot
            string poolTag = currentBulletType.ToString().ToLower() + "Bullet"; // Assuming your pool tags are named accordingly
            ObjectPooler.Instance.SpawnFromPool(poolTag, firingPoint.position, firingPoint.rotation);
        }

        private void SwitchBulletType(bool forward)
        {
            currentBulletType = (BulletType)(((int)currentBulletType + (forward ? 1 : -1) + bulletColors.Length) % bulletColors.Length);
            UpdateBulletTypeUI();
            Debug.Log("Switched to bullet type: " + currentBulletType);
        }

        private void UpdateBulletTypeUI()
        {
            bulletTypeUI.UpdateBulletTypeUI(bulletColors[(int)currentBulletType]);
        }
    }
}
