using UnityEngine;
using UnityEngine.UI;

namespace DungTran31
{
    public class LevelMenuManager : MonoBehaviour
    {
        [SerializeField] private Button[] buttons;
        [SerializeField] private GameObject levelButtons;

        private void Awake()
        {
            ButtonsToArray();
            int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].interactable = false;
            }
            for (int i = 0; i < unlockedLevel; i++)
            {
                buttons[i].interactable = true;
            }
        }

        private void ButtonsToArray()
        {
            int childCount = levelButtons.transform.childCount;
            buttons = new Button[childCount];
            for (int i = 0; i < childCount; i++)
            {
                buttons[i] = levelButtons.transform.GetChild(i).GetComponent<Button>();
            }
        }
    }
}
