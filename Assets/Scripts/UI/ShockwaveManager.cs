using DungTran31.GamePlay.Enemy;
using DungTran31.Utilities;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace DungTran31.UI
{
    public class ShockwaveManager : MonoBehaviour
    {
        [SerializeField] private float _shockWaveTime = 0.75f;

        private Coroutine _shockWaveCoroutine;
        private Material _material;
        private static int _waveDistanceFromCenter = Shader.PropertyToID("_WaveDistanceFromCenter");
        private static int _ringSpawnPosition = Shader.PropertyToID("_RingSpawnPosition");

        private void Awake()
        {
            _material = GetComponent<SpriteRenderer>().material;
            _material.SetFloat(_waveDistanceFromCenter, -0.1f);
        }

        private void OnEnable()
        {
            EnemyHealth.OnEnemyDeath += CallShockWave;
        }

        private void OnDisable()
        {
            EnemyHealth.OnEnemyDeath -= CallShockWave;
        }

        private void CallShockWave(EnemyHealth.EnemyDeathEventArgs args)
        {
            if (_shockWaveCoroutine != null)
            {
                StopCoroutine(_shockWaveCoroutine);
            }
            // Convert world position to screen position
            Vector3 screenPos = Camera.main.WorldToScreenPoint(args.Position);
            // Normalize screen position
            Vector2 normalizedPos = new Vector2(screenPos.x / Screen.width, screenPos.y / Screen.height);

            _material.SetVector(_ringSpawnPosition, normalizedPos);


            _shockWaveCoroutine = StartCoroutine(ShockWaveAction(-0.1f, 1f));

        }

        private IEnumerator ShockWaveAction(float startPos, float endPos)
        {
            _material.SetFloat(_waveDistanceFromCenter, startPos);

            float lerpedAmount = 0;
            float elapsedTime = 0;
            while(elapsedTime < _shockWaveTime)
            {
                elapsedTime += Time.deltaTime;
                lerpedAmount = Mathf.Lerp(startPos, endPos, elapsedTime / _shockWaveTime);
                _material.SetFloat(_waveDistanceFromCenter, lerpedAmount);
                yield return null;
            }
            _material.SetFloat(_waveDistanceFromCenter, -0.1f);

        }
    }
}
