using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BackgroundScroll : MonoBehaviour
    {
        private RawImage _rawImage;
        [SerializeField]private float _scrollSpeed = 0.007f;
        [SerializeField]private float _xDirection = 1f;
        [SerializeField]private float _yDirection = 1f;

        private async void Awake()
        {
            _rawImage = GetComponent<RawImage>();
            try
            {
                await Scroll();
            }
            catch (OperationCanceledException) { }
        }

        private async UniTask Scroll()
        {
            while (destroyCancellationToken.IsCancellationRequested == false)
            {
                _rawImage.uvRect = new Rect(_rawImage.uvRect.position + 
                                            new Vector2(_xDirection * _scrollSpeed, _yDirection * _scrollSpeed) 
                                            * Time.deltaTime, _rawImage.uvRect.size);
                await UniTask.Yield(PlayerLoopTiming.Update, destroyCancellationToken);
            }
        }
    }
}