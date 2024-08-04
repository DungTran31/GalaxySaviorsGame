using DungTran31.Dialogues;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DungTran31.Core
{
    public class TransitionInput : MonoBehaviour
    {
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space)
                && !DialogueManager.Instance.DialogueIsPlaying 
                && DialogueManager.Instance.DialogueCount == 0)
            {
                if(SceneController.Instance != null)
                    SceneController.Instance.NextLevel(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
