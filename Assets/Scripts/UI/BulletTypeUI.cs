using UnityEngine;
using UnityEngine.UI;

namespace DungTran31.UI
{
    public class BulletTypeUI : MonoBehaviour
    {
        private Image skillImage;

        private void Awake()
        {
            skillImage = GetComponent<Image>();
        }

        public void UpdateBulletTypeUI(Color color)
        {
            skillImage.color = color;
        }
    }
}
