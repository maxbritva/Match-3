using UnityEngine;

namespace Game.Tiles
{
    [CreateAssetMenu(fileName = "TileSO", menuName = "ScriptableObjects")]
    public class TileSO : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite _sprite;
    }
}