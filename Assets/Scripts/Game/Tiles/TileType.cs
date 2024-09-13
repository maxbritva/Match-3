using UnityEngine;

namespace Game.Tiles
{
    public enum TileKind
    {
        Normal,
        Blank,
        Jelly
    }
    
    [CreateAssetMenu(fileName = "TileSO", menuName = "ScriptableObjects/tiles")]
    public class TileType : ScriptableObject
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private TileKind _tileKind;
        [SerializeField] private bool _isInteractive;

        public bool IsInteractive => _isInteractive;
        public TileKind TileKind => _tileKind;
        public Sprite Sprite => _sprite;
    }
}