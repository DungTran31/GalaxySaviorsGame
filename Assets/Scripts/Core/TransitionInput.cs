using UnityEngine;

namespace DungTran31.Core
{
    public class TransitionInput : MonoBehaviour
    {
        private void Update()
        {
            if(Input.GetMouseButtonDown(0) || Input.anyKeyDown)
            {
                SceneController.Instance.NextLevel();
            }
        }
    }
}
