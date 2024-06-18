using DungTran31.UI;
using UnityEngine;

namespace DungTran31.Core
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject helpText;
        [SerializeField] private GameObject options;
        [SerializeField] private GameObject settingScreen;
        [SerializeField] private GameObject[] cubes;

        private void Awake()
        {
            SetActiveAllCubes(true);
            settingScreen.SetActive(false);
            helpText.SetActive(true);
            options.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) || Input.anyKeyDown)
            {
                ShowOption();
            }
        }

        public void ShowSetting()
        {
            SetActiveAllCubes(false);
            settingScreen.SetActive(true);
            AudioManager.Instance.musicSource.Pause();
        }

        public void HideSetting()
        {
            SetActiveAllCubes(true);
            settingScreen.SetActive(false);
            AudioManager.Instance.musicSource.UnPause();
        }

        private void ShowOption()
        {
            helpText.SetActive(false);
            options.SetActive(true);
            DissolveMainMenu.Instance.SetAppear();
        }

        private void SetActiveAllCubes(bool active)
        {
            foreach (var cube in cubes)
            {
                cube.SetActive(active);
            }
        }
    }
}
