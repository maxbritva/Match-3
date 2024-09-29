using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Animations
{
    public class AnimationManager: IDisposable, IAnimation
    {
        private CancellationTokenSource _сts;
        
        public async UniTask Reveal(GameObject target, float delay)
        {
            _сts = new CancellationTokenSource();
            target.transform.localScale = Vector3.one * 0.1f;
            target.transform.DOScale(Vector3.one, delay).SetEase(Ease.OutBounce);
           await UniTask.Delay(TimeSpan.FromSeconds(delay), _сts.IsCancellationRequested);
            _сts.Cancel();
        }

        public async UniTask Move(GameObject target, Vector3 position, float duration, Ease ease) => 
            await target.transform.DOMove(position, duration).From().SetEase(ease);
        public async UniTask MoveUI(RectTransform target, Vector3 position, float duration, Ease ease) =>
            await target.DOAnchorPos(position, duration).SetEase(ease);
        public void Dispose() => _сts?.Dispose();
    }
}