using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Tiles
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Tile: MonoBehaviour
    {
        public TileType tileType;
        public bool IsInteractable { get; private set; }
        
        public bool IsMatched { get; private set; }

        public void SetType(TileType tileType)
        {
            this.tileType = tileType;
            IsInteractable = tileType.IsInteractive;
            IsMatched = false;
            GetComponent<SpriteRenderer>().sprite = tileType.Sprite;
        }
        public TileType GetTileType() => tileType;

        public bool SetMatch(bool value) => IsMatched = value;
    }
}