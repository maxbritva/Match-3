using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Animations
{
    public interface IAnimation
    {
        UniTask Reveal(GameObject target);
    }
}