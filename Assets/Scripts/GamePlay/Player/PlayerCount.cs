using DungTran31.GamePlay.Enemy;
using TMPro;
using UnityEngine;

namespace DungTran31.GamePlay.Player
{
    public class PlayerCount : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI killRemainingCountText;
        [SerializeField] private int targetKillCountText;
        public int killCount { get; private set; }

        private void Start()
        {
            killCount = 0;
            print("Kill count: " + killCount);
            print("Target kill count: " + targetKillCountText);
            UpdateKillCountUI(); // Initialize UI on start
        }

        private void OnEnable()
        {
            EnemyHealth.OnEnemyDeath += IncrementKillCount;
        }

        private void OnDisable()
        {
            EnemyHealth.OnEnemyDeath -= IncrementKillCount;
        }

        private void IncrementKillCount()
        {
            killCount++; // Increment the kill count
            UpdateKillCountUI(); // Update the UI
        }

        // Updates the kill count UI text
        private void UpdateKillCountUI()
        {
            if (killRemainingCountText != null)
            {
                int remainingKillCount = targetKillCountText - killCount;
                print("Remaining kill count: " + remainingKillCount);
                killRemainingCountText.text = remainingKillCount.ToString();
            }
        }

        
    }

}
