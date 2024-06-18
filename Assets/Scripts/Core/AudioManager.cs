using UnityEngine;
using DungTran31.Utilities;

namespace DungTran31.Core
{
    public class AudioManager : Singleton<AudioManager>
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
    }
}