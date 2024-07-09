using UnityEngine;

namespace DungTran31.GamePlay.Collectible
{
    public class HealthCollectible : MonoBehaviour
    {
        [SerializeField] private float healthValue;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Player")
            {
                collision.GetComponent<Player.PlayerHealth>().Heal(healthValue);
                gameObject.SetActive(false);
            }
        }
    }
}