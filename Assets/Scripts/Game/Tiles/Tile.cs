using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Tiles
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Tile: MonoBehaviour
    {
        public TileType tileType;

        public void SetType(TileType tileType)
        {
            this.tileType = tileType;
            GetComponent<SpriteRenderer>().sprite = tileType.Sprite;
        }
        public TileType GetTileType() => tileType;
    }
}