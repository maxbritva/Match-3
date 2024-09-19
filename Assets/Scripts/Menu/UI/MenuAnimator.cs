using System.Collections.Generic;
using Audio;
using Cysharp.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using VContainer;

namespace Menu.UI
{
    public class MenuAnimator : MonoBehaviour
    {
        [SerializeField] private GameObject _leftTower;
        [SerializeField] private GameObject _rightTower;
        [SerializeField] private GameObject _wall;
        [SerializeField] private GameObject _logo;
        [SerializeField] private List<GameObject> _levelButtons = new List<GameObject>();

        private AudioManager _audioManager;
        public async UniTask StartAnimation()
        {
            var cancellationTokenOnDestroy = this.GetCancellationTokenOnDestroy();
            _audioManager.PlayWhoosh();
            _leftTower.SetActive(true);
            await _leftTower.transform.DOMove(new Vector3(-13f, -0.74f, 0), 0.2f).From().SetEase(Ease.InOutBack);
            _rightTower.SetActive(true);
            await _rightTower.transform.DOMove(new Vector3(13f, -0.74f, 0), 0.3f).From().SetEase(Ease.InOutBack);
           _wall.SetActive(true);
           _audioManager.PlayWhoosh();
           await _wall.transform.DOMove(new Vector3(0.2f, -8f, 0), 0.3f).From().SetEase(Ease.InSine).WithCancellation(cancellationTokenOnDestroy);
           _logo.SetActive(true);
           _audioManager.PlayWhoosh();
           await _logo.transform.DOMove(new Vector3(-1.32f, -5f, 0), 0.7f).From().SetEase(Ease.OutBounce).WithCancellation(cancellationTokenOnDestroy);
           foreach (var button in _levelButtons)
           {
               _audioManager.PlayPop();
               button.SetActive(true);
               await button.transform.DOScale(Vector3.zero, 0.1f).From().SetEase(Ease.InOutBack).WithCancellation(cancellationTokenOnDestroy);
           }
        }

        [Inject] private void Construct(AudioManager audioManager) => _audioManager = audioManager;
    }
}