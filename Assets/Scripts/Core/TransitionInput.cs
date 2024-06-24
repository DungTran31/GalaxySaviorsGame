using DungTran31.Dialogues;
using UnityEngine;

namespace DungTran31.Core
{
    public class TransitionInput : MonoBehaviour
    {
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Return)
                && !DialogueManager.Instance.dialogueIsPlaying 
                && DialogueManager.Instance.dialogueCount == 0)
            {
                if(SceneController.Instance != null)
                    SceneController.Instance.NextLevel();
            }
        }
    }
}
