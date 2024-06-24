using UnityEngine;
using DungTran31.Utilities;

namespace DungTran31
{
    public class PlayerShoot : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform firingPoint;
        [Range(0.1f, 2f)]
        [SerializeField] private float fireRate = 0.5f;

        private float fireTimer;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && fireTimer <= 0f)
            {
                Shoot();
                fireTimer = fireRate;
            } else
            {
                fireTimer -= Time.deltaTime;
            }
        }

        private void Shoot()
        {
            GameObject bullet = ObjectPooler.Instance.SpawnFromPool("bullet", firingPoint.position, firingPoint.rotation);
        }
    }
}
