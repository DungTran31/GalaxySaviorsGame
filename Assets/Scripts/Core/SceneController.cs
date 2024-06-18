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
        }

        IEnumerator LoadLevel()
        {
            transition.SetTrigger(Start);
            yield return new WaitForSeconds(1);
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
