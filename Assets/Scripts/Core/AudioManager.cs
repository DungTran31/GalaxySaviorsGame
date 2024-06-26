using UnityEngine;
using DungTran31.Utilities;
using UnityEngine.SceneManagement;

namespace DungTran31.Core
{
    public class AudioManager : SingletonPersistent<AudioManager>
    {
        [Header("Audio Source")]
        [SerializeField] public AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;

        [Header("Audio Clip")]
        public AudioClip background;
        public AudioClip death;
        public AudioClip score;
        public AudioClip pressed;

        private void Start()
        {
            musicSource.clip = background;
            musicSource.Play();
        }

        public void PlaySfx(AudioClip clip)
        {
            sfxSource.PlayOneShot(clip);
        }

        private void Update()
        {
            if (SceneManager.GetActiveScene().name.StartsWith("PreLevel"))
            {
                musicSource.Pause();
            }
            else
            {
                musicSource.UnPause();
            }
        }
    }
}