using UnityEngine;
using UnityEngine.SceneManagement;

namespace DungTran31.Core
{
    public class FinishLevel : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                UnlockNewLevel();
                SceneController.Instance.NextLevel(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

        private void UnlockNewLevel()
        {
            if(SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
            {
                PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 2);
                PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
                PlayerPrefs.Save();
            }
        }
    }
}