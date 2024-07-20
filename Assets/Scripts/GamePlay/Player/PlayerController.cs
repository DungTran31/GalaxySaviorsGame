using UnityEngine;
using DungTran31.UI;

namespace DungTran31.GamePlay.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer head;
        [SerializeField] private Sprite sprite1;
        [SerializeField] private Sprite sprite2;
        [SerializeField] private Sprite sprite3;
        
        private void Start()
        {
            if (ChangingSkin.Instance != null)
            {
                int currentSkin = ChangingSkin.Instance.GetCurrentSkin();
                UpdateSkin(currentSkin);
            }
            else
            {
                Debug.Log("ChangingSkin instance is not initialized.");
            }
        }

        private void UpdateSkin(int skin)
        {
            switch (skin)
            {
                case 1:
                    head.sprite = sprite1;
                    break;
                case 2:
                    head.sprite = sprite2;
                    break;
                case 3:
                    head.sprite = sprite3;
                    break;
            }
        }
    }
}
