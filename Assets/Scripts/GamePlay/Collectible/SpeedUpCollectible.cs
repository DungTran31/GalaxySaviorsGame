using UnityEngine;

namespace DungTran31.GamePlay.Collectible
{
    public class SpeedUpCollectible : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                collision.GetComponent<Player.PlayerMovement>().SpeedUp();
                gameObject.SetActive(false);
            }
        }
    }
}
