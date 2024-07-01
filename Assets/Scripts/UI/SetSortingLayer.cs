using UnityEngine;

namespace DungTran31.UI
{
    public class SetSortingLayer : MonoBehaviour
    {
        [SerializeField] private string sortingLayerName = "Foreground"; 
        [SerializeField] private int orderInLayer = 1; 

        private void Start()
        {
            TextMesh textMesh = GetComponent<TextMesh>();
            if (textMesh != null)
            {
                Renderer textRenderer = textMesh.GetComponent<Renderer>();
                if (textRenderer != null)
                {
                    textRenderer.sortingLayerName = sortingLayerName;
                    textRenderer.sortingOrder = orderInLayer;
                }
            }
        }
    }
}
