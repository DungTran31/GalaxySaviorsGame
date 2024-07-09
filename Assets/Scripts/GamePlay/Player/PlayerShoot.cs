using UnityEngine;
using UnityEngine.UI;
using DungTran31.Utilities;

namespace DungTran31.GamePlay.Player
{
    public class PlayerShoot : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Image chooseSkillImage;
        [SerializeField] private Transform firingPoint;
        [Range(0.1f, 2f)]
        [SerializeField] private float fireRate = 0.5f;

        private Color fireColor = new Color(247f / 255f, 118f / 255f, 34f / 255f); // #F77622
        private Color poisonColor = new Color(99f / 255f, 199f / 255f, 77f / 255f); // #63C74D
        private Color iceColor = new Color(57f / 255f, 174f / 255f, 224f / 255f); // #39AEE0
        private Color blackColor = new Color(38f / 255f, 43f / 255f, 68f / 255f); // #262B44


        private float fireTimer;

        // Define bullet types
        public enum BulletType { fire, poison, ice, black }
        // Current bullet type
        private BulletType currentBulletType = BulletType.fire;

        private void Start()
        {
            chooseSkillImage = GameObject.Find("SkillImage").GetComponent<Image>();
            // Convert hex F77622 to normalized RGB values
            chooseSkillImage.color = fireColor;
        }

        private void Update()
        {
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
            GameObject bullet = ObjectPooler.Instance.SpawnFromPool(poolTag, firingPoint.position, firingPoint.rotation);
        }

        private void SwitchBulletType(bool forward)
        {
            if (forward)
            {
                // Cycle forward through the BulletType enumeration
                currentBulletType++;
                if ((int)currentBulletType >= System.Enum.GetValues(typeof(BulletType)).Length)
                {
                    currentBulletType = 0;
                }
            }
            else
            {
                // Cycle backward through the BulletType enumeration
                if (currentBulletType == 0)
                {
                    currentBulletType = (BulletType)System.Enum.GetValues(typeof(BulletType)).Length - 1;
                }
                else
                {
                    currentBulletType--;
                }
            }

            // Apply color to chooseSkillImage based on the current bullet type
            switch (currentBulletType)
            {
                case BulletType.fire:
                    chooseSkillImage.color = fireColor;
                    break;
                case BulletType.poison:
                    chooseSkillImage.color = poisonColor;
                    break;
                case BulletType.ice:
                    chooseSkillImage.color = iceColor;
                    break;
                case BulletType.black:
                    chooseSkillImage.color = blackColor;
                    break;
            }

            print("Switched to bullet type: " + currentBulletType);
        }
    }
}
