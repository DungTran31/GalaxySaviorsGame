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

        public void IncreaseDamage(float amount)
        {
            Damage += amount;
        }
    }
}
