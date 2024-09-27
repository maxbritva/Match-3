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
        [SerializeField] private AudioSource _pitchSoundSource;
        [SerializeField] private AudioSource _normalSoundSource;
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
        [SerializeField] AudioClip _stopMusic;
        private GameData _gameData;

        public void PlayClick() => PlayNormalPitch(_click);
        public void PlayDeselect() => PlayNormalPitch(_deselect);
        public void PlayMatch() => PlayNormalPitch(_match);
        public void PlayNoMatch() => PlayNormalPitch(_noMatch);
        public void PlayWhoosh() => PlayRandomPitch(_whoosh);
        public void PlayPop() => PlayRandomPitch(_pop);
        
        private void PlayStopMusic() => PlayNormalPitch(_stopMusic);

        public void SetSoundVolume()
        {
            if (_gameData.IsEnabledSound)
            {
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

        public void StopMusic()
        {
            _musicSource.Stop();
            PlayStopMusic();
        }
        private void PlayRandomPitch(AudioClip audioClip) 
        {
            _pitchSoundSource.pitch = Random.Range(0.8f, 1.2f);
            _pitchSoundSource.PlayOneShot(audioClip);
        }
        private void PlayNormalPitch(AudioClip audioClip) => _normalSoundSource.PlayOneShot(audioClip);

        [Inject] private void Construct(GameData gameData) => _gameData = gameData;
    }
}