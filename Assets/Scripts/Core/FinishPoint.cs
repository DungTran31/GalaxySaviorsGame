using UnityEngine;

namespace DungTran31.Core
{
    public class FinishPoint : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                SceneController.Instance.NextLevel();
            }
        }
    }
}
