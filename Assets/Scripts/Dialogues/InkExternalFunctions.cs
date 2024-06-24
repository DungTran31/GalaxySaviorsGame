using UnityEngine;
using Ink.Runtime;

namespace DungTran31.Dialogues
{
    public class InkExternalFunctions : MonoBehaviour
    {
        public void Bind(Story story, Animator emoteAnimator)
        {
            story.BindExternalFunction("playEmote", (string emoteName) => PlayEmote(emoteName, emoteAnimator));
        }

        public void Unbind(Story story)
        {
            story.UnbindExternalFunction("playEmote");
        }

        public void PlayEmote(string emoteName, Animator emoteAnimator)
        {
            if (emoteAnimator != null)
            {
                emoteAnimator.Play(emoteName);
            }
            else
            {
                Debug.LogWarning("Tried to play emote, but emote animator was "
                    + "not initialized when entering dialogue mode.");
            }
        }
    }
}
