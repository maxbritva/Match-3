using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Animations
{
    public interface IAnimation
    {
        UniTask Reveal(GameObject target, float delay);
        void DoPunchAnimate(GameObject target, Vector3 scale, float duration);
        UniTask MoveUI(RectTransform target, Vector3 position, float duration, Ease ease);
    }
}