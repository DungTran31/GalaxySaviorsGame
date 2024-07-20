using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DungTran31.Core
{
    public class UIManager : MonoBehaviour
    {
        //Quit game/exit play mode if in Editor
        public void Quit()
        {
            Application.Quit(); //Quits the game (only works in build)

            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; //Exits play mode
            #endif
        }
    }
}