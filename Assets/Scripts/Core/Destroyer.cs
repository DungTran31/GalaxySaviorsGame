using UnityEngine;

namespace DungTran31.Core
{
    public class Destroyer : MonoBehaviour
    {
        private void Start()
        {
        	Destroy(gameObject, 2f);
        }
    }
}
