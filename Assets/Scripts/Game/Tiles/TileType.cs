using UnityEngine;

namespace Game.Tiles
{
    [CreateAssetMenu(fileName = "TileSO", menuName = "ScriptableObjects/tiles")]
    public class TileType : ScriptableObject
    {
        [SerializeField] private string _name; 
        [SerializeField] private Sprite _sprite;


        public string Name => _name;

        public Sprite Sprite => _sprite;
    }
}