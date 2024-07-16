using UnityEngine;
using UnityEngine.UI;

namespace DungTran31.UI
{
    public class BossHealthBar : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        private new Camera camera;
        private void Start()
        {
            camera = Camera.main;
        }

        public void UpdateHealthBar(float currentValue, float maxValue)
        {
            slider.value = currentValue / maxValue;
        }
    }
}
