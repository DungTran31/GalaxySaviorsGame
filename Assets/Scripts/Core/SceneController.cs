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

        public void NextLevel()
        {
            StartCoroutine(LoadLevel());
            if(AudioManager.Instance != null)
                AudioManager.Instance.PlaySfx(AudioManager.Instance.pressed);
        }

        IEnumerator LoadLevel()
        {
            if (transition != null)
            {
                transition.SetTrigger(Start);
                yield return new WaitForSeconds(1); // Adjust the wait time as needed
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                Debug.LogWarning("Transition Animator is not assigned in the SceneController.");
            }
        }
    }
}
