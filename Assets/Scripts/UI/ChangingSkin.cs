using DungTran31.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace DungTran31.UI
{
    public class ChangingSkin : SingletonPersistent<ChangingSkin>
    {
        [SerializeField] private Image squareHeadDisplay;
        [SerializeField] private Sprite sprite1;
        [SerializeField] private Sprite sprite2;
        [SerializeField] private Sprite sprite3;
        private int whatSkin = 1;

        protected override void Awake()
        {
            base.Awake();
            whatSkin = PlayerPrefs.GetInt("SelectedSkin", 1);
            UpdateSkin();
        }

        public void ChangeSkin(int skinNumber)
        {
            whatSkin = skinNumber;
            UpdateSkin();
            PlayerPrefs.SetInt("SelectedSkin", whatSkin);
        }

        private void UpdateSkin()
        {
            switch (whatSkin)
            {
                case 1:
                    squareHeadDisplay.sprite = sprite1;
                    break;
                case 2:
                    squareHeadDisplay.sprite = sprite2;
                    break;
                case 3:
                    squareHeadDisplay.sprite = sprite3;
                    break;
                default:
                    squareHeadDisplay.sprite = sprite1; // Fallback to default skin
                    break;
            }
        }

        public int GetCurrentSkin()
        {
            return whatSkin;
        }
    }
}