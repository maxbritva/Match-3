using System.Collections.Generic;
using Animations;
using Audio;
using Cysharp.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using VContainer;

namespace Menu.UI
{
    public class MenuView : MonoBehaviour
    {
        [SerializeField] private GameObject _leftTower;
        [SerializeField] private GameObject _rightTower;
        [SerializeField] private GameObject _wall;
        [SerializeField] private GameObject _logo;
        [SerializeField] private List<GameObject> _levelButtons = new List<GameObject>();

        private AudioManager _audioManager;
        private IAnimation _animationManager;
        public async UniTask StartAnimation()
        {
            _audioManager.PlayWhoosh();
            _leftTower.SetActive(true);
            await _animationManager.Move(_leftTower, new Vector3(-13f, -0.74f, 0), 0.2f, Ease.InOutBack);
           _rightTower.SetActive(true);
           await _animationManager.Move(_rightTower, new Vector3(13f, -0.74f, 0), 0.3f, Ease.InOutBack);
           _wall.SetActive(true);
           _audioManager.PlayWhoosh();
           await _animationManager.Move(_wall, new Vector3(0.2f, -8f, 0), 0.3f, Ease.InOutBack);
           _logo.SetActive(true);
           _audioManager.PlayWhoosh();
           await _animationManager.Move(_logo, new Vector3(-1.32f, -5f, 0), 0.7f, Ease.OutBounce);
           foreach (var button in _levelButtons)
           {
               _audioManager.PlayPop();
               button.SetActive(true);
               await _animationManager.Reveal(button, 0.1f);
           }
        }

        [Inject] private void Construct(AudioManager audioManager, IAnimation animationManager)
        {
            _audioManager = audioManager;
            _animationManager = animationManager;
        }
    }
}