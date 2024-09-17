using Data;
using UnityEngine;
using UnityEngine.Audio;
using VContainer;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager: MonoBehaviour
    {
        [Header("Sources")]
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _soundSource;
        [SerializeField] private AudioMixer _audioMixer;
        
        [Header("Clips")]
        [SerializeField] AudioClip _menuMusic;
        [SerializeField] AudioClip _gameMusic;
        [SerializeField] AudioClip _click;
        [SerializeField] AudioClip _deselect;
        [SerializeField] AudioClip _match;
        [SerializeField] AudioClip _noMatch;
        [SerializeField] AudioClip _whoosh;
        [SerializeField] AudioClip _pop;
        private GameData _gameData;

        public void PlayClick() => _soundSource.PlayOneShot(_click);
        public void PlayDeselect() => _soundSource.PlayOneShot(_deselect);
        public void PlayMatch() => _soundSource.PlayOneShot(_match);
        public void PlayNoMatch() => _soundSource.PlayOneShot(_noMatch);
        public void PlayWhoosh() => PlayRandomPitch(_whoosh);
        public void PlayPop() => PlayRandomPitch(_pop);

        public void SetSoundVolume()
        {
            if (_gameData.IsEnabledSound)
            {
                _musicSource.clip = _menuMusic;
                _audioMixer.SetFloat("Volume", -6f);
                _musicSource.Play();
            }
            else
            {
                _audioMixer.SetFloat("Volume", -80f);
                _musicSource.Stop();
            }
        }

        public void PlayGameMusic() {
            _musicSource.Stop();
            _musicSource.clip = _gameMusic;
            SetSoundVolume();
        }
        public void PlayMenuMusic() {
            _musicSource.Stop();
            _musicSource.clip = _menuMusic;
            SetSoundVolume();
        }
        private void PlayRandomPitch(AudioClip audioClip) 
        {
            _soundSource.pitch = Random.Range(0.9f, 1.1f);
            _soundSource.PlayOneShot(audioClip);
            _soundSource.pitch = 1f;
        }
        
        [Inject] private void Construct(GameData gameData) => _gameData = gameData;
      
    }
}