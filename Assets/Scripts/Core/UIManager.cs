using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.EventSystems;

namespace DungTran31.Core
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }
        public static bool IsGamePaused { get; private set; }

        [SerializeField] private GameObject pauseScreen;
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private InputActionAsset gameplayInputActions;
        [SerializeField] private InputActionAsset uiInputActions;

        private InputSystemUIInputModule inputModule;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            pauseScreen.SetActive(false);
            gameOverScreen.SetActive(false);
            IsGamePaused = false;

            InitializeInputModule();
        }

        private void InitializeInputModule()
        {
            inputModule = FindObjectOfType<EventSystem>().GetComponent<InputSystemUIInputModule>();
            inputModule.actionsAsset = gameplayInputActions;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
        }

        public void TogglePause()
        {
            IsGamePaused = !IsGamePaused;
            pauseScreen.SetActive(IsGamePaused);
            Time.timeScale = IsGamePaused ? 0 : 1;
            SwitchInputActions(IsGamePaused ? uiInputActions : gameplayInputActions);
        }

        public void ChangeGameOverScreenState(bool state)
        {
            gameOverScreen.SetActive(state);
            if (state)
            {
                Time.timeScale = 0;
                print("Triggered");
                SwitchInputActions(uiInputActions);
            }
            else
            {
                Time.timeScale = 1;
                print("Triggered");
                SwitchInputActions(gameplayInputActions);
            }
        }

        public void RestartGame()
        {
            Time.timeScale = 1;
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reloads the current scene
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            InitializeInputModule();
            SwitchInputActions(gameplayInputActions);
        }

        public void Quit()
        {
            Application.Quit(); // Quits the game (only works in build)

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Exits play mode
#endif
        }

        private void SwitchInputActions(InputActionAsset newActions)
        {
            if (inputModule != null)
            {
                inputModule.actionsAsset = newActions;

                if (newActions == gameplayInputActions)
                {
                    // Reassign actions
                    inputModule.move = InputActionReference.Create(newActions.FindAction("game/move"));
                    inputModule.submit = InputActionReference.Create(newActions.FindAction("game/submit"));
                }
                else if (newActions == uiInputActions)
                {
                    // Reassign actions
                    inputModule.point = InputActionReference.Create(newActions.FindAction("UI/Point"));
                    inputModule.leftClick = InputActionReference.Create(newActions.FindAction("UI/Click"));
                    inputModule.middleClick = InputActionReference.Create(newActions.FindAction("UI/MiddleClick"));
                    inputModule.rightClick = InputActionReference.Create(newActions.FindAction("UI/RightClick"));
                    inputModule.scrollWheel = InputActionReference.Create(newActions.FindAction("UI/ScrollWheel"));
                    inputModule.move = InputActionReference.Create(newActions.FindAction("UI/Navigate"));
                    inputModule.submit = InputActionReference.Create(newActions.FindAction("UI/Submit"));
                    inputModule.cancel = InputActionReference.Create(newActions.FindAction("UI/Cancel"));
                }
            }
        }
    }
}
