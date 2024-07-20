using DungTran31.GamePlay.Player.SO;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DungTran31.GamePlay.Player
{
    public class PlayerExp : MonoBehaviour
    {
        [SerializeField] private Image experienceBar; 
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private LevelSO levelConfig; 
        [SerializeField] private PlayerHealth playerHealth; // Reference to the PlayerHealth script
        [SerializeField] private PlayerMovement playerMovement; // Reference to the PlayerMovement script
        [SerializeField] private PlayerBulletSO fireBulletSO; // Reference to the FireBullet ScriptableObject
        [SerializeField] private PlayerBulletSO iceBulletSO; // Reference to the IceBullet ScriptableObject
        [SerializeField] private PlayerBulletSO blackBulletSO; // Reference to the BlackBullet ScriptableObject
        [SerializeField] private PlayerBulletSO poisonBulletSO; // Reference to the BlackBullet ScriptableObject
        [SerializeField] private int level = 1; // Start at level 1
        [SerializeField] private int experience = 0; // Start with 0 experience
        private int requireExperience;

        public void IncreaseExp(int value)
        {
            experience += value;
            if (experience >= requireExperience)
            {
                while (experience >= requireExperience)
                {
                    experience -= requireExperience;
                    LevelUp();
                }
            }
            UpdateUI();
        }

        public void LevelUp()
        {
            level++;
            CalculateRequireExp();

            // Assuming you have references set up
            playerHealth.IncreaseMaxHealth(50); // Increase max health by 50
            playerMovement.IncreaseMoveSpeed(1); // Increase move speed by 1

            List<int> damageValues = new() { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };

            // Shuffle the list to randomize the order of damage values
            System.Random rng = new();
            int n = damageValues.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                (damageValues[k], damageValues[n]) = (damageValues[n], damageValues[k]); // Using tuple to swap values
            }

            // Assign unique random damage increases to each bullet type
            fireBulletSO.IncreaseDamage(damageValues[0]);
            iceBulletSO.IncreaseDamage(damageValues[1]);
            blackBulletSO.IncreaseDamage(damageValues[2]);
            poisonBulletSO.IncreaseDamage(damageValues[3]);
        }

        private void Start()
        {
            CalculateRequireExp();
            UpdateUI();
        }

        private void CalculateRequireExp()
        {
            requireExperience = levelConfig.GetRequiredExp(level);
            UpdateUI();
        }

        private void UpdateUI()
        {
            experienceBar.fillAmount = (float)experience / (float)requireExperience; 
            levelText.text = "Level: " + level;
            //experienceText.text = experience + "/" + requireExperience + "Exp"; 
        }
    }
}
