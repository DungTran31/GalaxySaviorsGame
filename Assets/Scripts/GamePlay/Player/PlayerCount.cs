using DungTran31.GamePlay.Enemy;
using System;
using TMPro;
using UnityEngine;

namespace DungTran31.GamePlay.Player
{
    public class PlayerCount : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI killRemainingCountText;
        [SerializeField] private GameObject bossPrefab;
        [SerializeField] private int targetKillCount;

        public static event Action OnTargetKillCountReached;
        public int KillCount { get; private set; }
        private bool bossSpawned = false;

        private void Start()
        {
            bossSpawned = false;
            KillCount = 0;
            UpdateKillCountUI(); // Initialize UI on start
        }

        private void OnEnable() => EnemyHealth.OnEnemyDeath += IncrementKillCount;

        private void OnDisable() => EnemyHealth.OnEnemyDeath -= IncrementKillCount;

        private void IncrementKillCount(EnemyHealth.EnemyDeathEventArgs args)
        {
            KillCount++; // Increment the kill count
            UpdateKillCountUI(); // Update the UI
        }

        // Updates the kill count UI text
        private void UpdateKillCountUI()
        {
            if (killRemainingCountText != null)
            {
                int remainingKillCount = targetKillCount - KillCount;
                print("Remaining kill count: " + remainingKillCount);
                if(remainingKillCount <= 0)
                {
                    remainingKillCount = 0;
                    if (!bossSpawned)
                    {
                        // Spawn the boss when the player reaches the target kill count
                        Instantiate(bossPrefab, Vector3.zero, Quaternion.identity);
                        OnTargetKillCountReached?.Invoke();
                    }
                    bossSpawned = true;
                }
                killRemainingCountText.text = remainingKillCount.ToString();
            }
        }

        
    }

}
