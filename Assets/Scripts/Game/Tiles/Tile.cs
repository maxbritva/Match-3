using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Tiles
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Tile: MonoBehaviour
    {
        public TileType tileType;
        private bool _isUseful;

        public void SetType(TileType tileType)
        {
            this.tileType = tileType;
            GetComponent<SpriteRenderer>().sprite = tileType.Sprite;
        }
        public TileType GetTileType() => tileType;
        public bool CanUseTile(bool value) => _isUseful = value;


    }
}