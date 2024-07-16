using DungTran31.GamePlay.Player;
using UnityEngine;
using UnityEngine.UI;

namespace DungTran31.UI
{
    public class CinematicBars : MonoBehaviour
    {
        private RectTransform topBar, bottomBar;
        private float changeSizeAmount;
        private float targetSize;
        private bool isActive;

        private void Awake()
        {
            GameObject gameObject = new("topBar", typeof(Image));
            gameObject.transform.SetParent(transform, false);
            gameObject.GetComponent<Image>().color = Color.black;
            topBar = gameObject.GetComponent<RectTransform>();
            topBar.anchorMin = new Vector2(0, 1);
            topBar.anchorMax = new Vector2(1, 1);
            topBar.sizeDelta = new Vector2(0, 0);

            gameObject = new GameObject("bottomBar", typeof(Image));
            gameObject.transform.SetParent(transform, false);
            gameObject.GetComponent<Image>().color = Color.black;
            bottomBar = gameObject.GetComponent<RectTransform>();
            bottomBar.anchorMin = new Vector2(0, 0);
            bottomBar.anchorMax = new Vector2(1, 0);
            bottomBar.sizeDelta = new Vector2(0, 0);
        }

        private void OnEnable()
        {
            topBar.gameObject.SetActive(true);
            bottomBar.gameObject.SetActive(true);
            PlayerCount.OnTargetKillCountReached += ControlCinematicBars;
        }

        private void OnDisable()
        {
            topBar.gameObject.SetActive(false);
            bottomBar.gameObject.SetActive(false);
            PlayerCount.OnTargetKillCountReached -= ControlCinematicBars;
        }

        private void ControlCinematicBars()
        {
            Show(100, 1);
            Hide(1f);
        }

        private void Update()
        {
            if (isActive)
            {
                Vector2 sizeDelta = topBar.sizeDelta;
                sizeDelta.y += changeSizeAmount * Time.deltaTime;
                if (changeSizeAmount > 0)
                {
                    if (sizeDelta.y >= targetSize)
                    {
                        sizeDelta.y = targetSize;
                        isActive = false;
                    }
                }
                else
                {
                    if (sizeDelta.y <= targetSize)
                    {
                        sizeDelta.y = targetSize;
                        isActive = false;
                    }
                }
                topBar.sizeDelta = sizeDelta;
                bottomBar.sizeDelta = sizeDelta;
            }
        }

        public void Show(float targetSize, float time)
        {
            this.targetSize = targetSize;
            changeSizeAmount = (targetSize - topBar.sizeDelta.y) / time;
            isActive = true;
        }

        public void Hide(float time)
        {
            targetSize = 0;
            changeSizeAmount = (targetSize - topBar.sizeDelta.y) / time;
            isActive = true;
        }
    }
}
