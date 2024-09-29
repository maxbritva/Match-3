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
        [SerializeField] private RectTransform _leftTower;
        [SerializeField] private RectTransform _rightTower;
        [SerializeField] private RectTransform _wall;
        [SerializeField] private RectTransform _logo;
        [SerializeField] private List<GameObject> _levelButtons = new List<GameObject>();

        private AudioManager _audioManager;
        private IAnimation _animationManager;
        public async UniTask StartAnimation()
        {
            _audioManager.PlayWhoosh();
            await _animationManager.MoveUI(_leftTower, new Vector3(-23.5f, -116f, 0), 0.2f, Ease.InOutBack);
            await _animationManager.MoveUI(_rightTower, new Vector3(-433f, -83f, 0), 0.3f, Ease.InOutBack);
            _audioManager.PlayWhoosh();
           await _animationManager.MoveUI(_wall, new Vector3(0f, -48f, 0), 0.3f, Ease.InOutBack);
           _audioManager.PlayWhoosh();
           await _animationManager.MoveUI(_logo, new Vector3(-106f, 84f, 0), 0.7f, Ease.OutBounce);
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