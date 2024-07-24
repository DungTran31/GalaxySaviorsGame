using UnityEngine;

namespace DungTran31.GamePlay.Player.SO
{
    [CreateAssetMenu(fileName = "BulletSO", menuName = "ScriptableObjects/PlayerBulletSO", order = 3)]
    public class PlayerBulletSO : ScriptableObject
    {
        [field: SerializeField] public float Damage { get; private set;}
        [field: Range(1, 50)]
        [field: SerializeField] public float Speed { get; private set;}
        [field: Range(1, 10)]
        [field: SerializeField] public float LifeTime { get; private set; }

        public void IncreaseDamageByPercentage(float percentage)
        {
            Damage += Mathf.RoundToInt(Damage * (percentage / 100f));
        }

        private float originalDamage;

        private void OnEnable()
        {
            // Store the original damage value when the ScriptableObject is enabled
            originalDamage = Damage;
        }

        private void OnDisable()
        {
            // Reset the damage value to the original when the ScriptableObject is disabled
            Damage = originalDamage;
        }
    }
}
