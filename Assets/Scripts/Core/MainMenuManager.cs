using DungTran31.UI;
using UnityEngine;

namespace DungTran31.Core
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject helpText;
        [SerializeField] private GameObject options;
        [SerializeField] private GameObject settingPanel;
        [SerializeField] private GameObject aboutPanel;
        [SerializeField] private GameObject[] cubes;

        private void Awake()
        {
            SetActiveAllCubes(true);
            settingPanel.SetActive(false);
            aboutPanel.SetActive(false);
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
            settingPanel.SetActive(true);
            AudioManager.Instance.musicSource.Pause();
            AudioManager.Instance.PlaySfx(AudioManager.Instance.pressed);
        }

        public void HideSetting()
        {
            SetActiveAllCubes(true);
            settingPanel.SetActive(false);
            AudioManager.Instance.musicSource.UnPause();
            AudioManager.Instance.PlaySfx(AudioManager.Instance.pressed);
        }

        public void ShowAbout()
        {
            SetActiveAllCubes(false);
            aboutPanel.SetActive(true);
            AudioManager.Instance.musicSource.Pause();
            AudioManager.Instance.PlaySfx(AudioManager.Instance.pressed);
        }

        public void HideAbout()
        {
            SetActiveAllCubes(true);
            aboutPanel.SetActive(false);
            AudioManager.Instance.musicSource.UnPause();
            AudioManager.Instance.PlaySfx(AudioManager.Instance.pressed);
        }

        public void OpenLink(string link)
        {
            Application.OpenURL(link);
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
