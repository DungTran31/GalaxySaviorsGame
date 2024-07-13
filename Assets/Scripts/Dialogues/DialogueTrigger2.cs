using UnityEngine;
using DungTran31.GamePlay.Player;

namespace DungTran31.Dialogues
{
    public class DialogueTrigger2 : MonoBehaviour
    {
        [Header("Ink JSON")]
        [SerializeField] private TextAsset inkJSON;
        private PlayerCount playerCount;

        private void Start()
        {
            // Find the PlayerCount instance in the scene at the start
            playerCount = FindObjectOfType<PlayerCount>();
            if (playerCount == null)
            {
                Debug.LogError("PlayerCount instance not found in the scene.");
            }
        }
    }
}
