using UnityEngine;

namespace Game.Tiles
{
    [CreateAssetMenu(fileName = "TileSO", menuName = "ScriptableObjects/tiles")]
    public class TileType : ScriptableObject
    {
        [SerializeField] private Sprite _sprite;

        public Sprite Sprite => _sprite;
    }
}