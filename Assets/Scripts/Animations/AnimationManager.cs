using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Tiles;
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

        public async UniTask HideTile(GameObject target)
        {
            _сts = new CancellationTokenSource();
            await target.transform.DOScale(Vector3.zero, 0.05f).SetEase(Ease.OutBounce);
            target.SetActive(false);
            target.transform.localScale = Vector3.one;
            _сts.Cancel();
        }
        
        public void DoPunchAnimate(GameObject target, Vector3 scale, float duration) => 
            target.transform.DOPunchScale(scale, duration, 1, 0.5f);

        public async UniTask MoveUI(RectTransform target, Vector3 position, float duration, Ease ease) =>
            await target.DOAnchorPos(position, duration).SetEase(ease);
        
        public void AnimateTile(Tile tile, float value) => tile.transform.DOScale(value, 0.3f).SetEase(Ease.OutCubic);
        public void MoveTile(Tile tile, Vector3 position, Ease ease) =>  tile.transform.DOLocalMove(position, 0.2f).SetEase(ease); 
        public void Dispose() => _сts?.Dispose();
    }
}