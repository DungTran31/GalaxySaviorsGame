using UnityEngine;
using UnityEngine.SceneManagement;

namespace DungTran31.UI
{
    public class MouseCursor : MonoBehaviour
    {
        [SerializeField] private Texture2D defaultCursor;
        [SerializeField] private Texture2D aimCursor;
        [SerializeField] private Vector2 cursorHotspot = Vector2.zero;

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name.StartsWith("Level"))
            {
                Cursor.SetCursor(aimCursor, cursorHotspot, CursorMode.Auto);
            }
            else
            {
                Cursor.SetCursor(defaultCursor, cursorHotspot, CursorMode.Auto);
            }
        }

        private void Start()
        {
            // Set the initial cursor
            OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
        }
    }
}
