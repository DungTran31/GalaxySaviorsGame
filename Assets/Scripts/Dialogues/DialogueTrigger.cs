using DungTran31.Core;
using UnityEngine;

namespace DungTran31.Dialogues
{
    public class DialogueTrigger : MonoBehaviour
    {
        [Header("Ink JSON")]
        [SerializeField] private TextAsset inkJSON;

        private void Update()
        {
            if (!DialogueManager.Instance.dialogueIsPlaying)
            {
                // Check if the dialogue has not started and the interact button is pressed
                if (InputManager.Instance.GetInteractPressed() && DialogueManager.Instance.dialogueCount > 0)
                {
                    DialogueManager.Instance.notiText.SetActive(false);
                    DialogueManager.Instance.EnterDialogueMode(inkJSON);
                } 
            }
        }
    }
}
