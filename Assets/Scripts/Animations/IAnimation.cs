using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Animations
{
    public interface IAnimation
    {
        UniTask Reveal(GameObject target, float delay);
        UniTask Move(GameObject target, Vector3 position, float duration, Ease ease);
        UniTask MoveUI(RectTransform target, Vector3 position, float duration, Ease ease);
    }
}