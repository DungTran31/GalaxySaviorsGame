using UnityEngine;
using UnityEngine.UI;
using DungTran31.GamePlay.Player;
namespace DungTran31.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private PlayerHealth playerHealth;
        [SerializeField] private Image totalHealthBar;
        [SerializeField] private Image currentHealthBar;

        private void Start()
        {
            totalHealthBar.fillAmount = playerHealth.currentHealth / 10;
        }

        private void Update()
        {
            currentHealthBar.fillAmount = playerHealth.currentHealth / 10;
        }
    }
}