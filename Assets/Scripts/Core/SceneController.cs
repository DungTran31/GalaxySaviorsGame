using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DungTran31.Utilities;

namespace DungTran31.Core
{
    public class SceneController : Singleton<SceneController>
    {
        [SerializeField] private Animator transition;
        private static readonly int Start = Animator.StringToHash("Start");

        // Modified NextLevel method to accept a scene index
        public void NextLevel(int sceneIndex)
        {
            StartCoroutine(LoadLevel(sceneIndex));
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlaySfx(AudioManager.Instance.pressed);
        }

        // Modified LoadLevel coroutine to accept a scene index
        IEnumerator LoadLevel(int sceneIndex)
        {
            if (transition != null)
            {
                transition.SetTrigger(Start);
                yield return new WaitForSeconds(0.2f); // Adjust the wait time as needed
                SceneManager.LoadSceneAsync(sceneIndex);
            }
            else
            {
                Debug.LogWarning("Transition Animator is not assigned in the SceneController.");
            }
        }
    }
}
