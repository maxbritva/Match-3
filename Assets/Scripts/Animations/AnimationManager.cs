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
        
        public async UniTask Reveal(GameObject target)
        {
            _сts = new CancellationTokenSource();
            target.transform.localScale = Vector3.one * 0.1f;
            target.transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutBounce);
            UniTask.Delay(TimeSpan.FromSeconds(1f), _сts.IsCancellationRequested);
            _сts.Cancel();
        }

        public void Dispose() => _сts?.Dispose();
    }
}