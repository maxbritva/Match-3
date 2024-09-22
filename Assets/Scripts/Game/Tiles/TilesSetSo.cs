using System.Collections.Generic;
using UnityEngine;

namespace Game.Tiles
{
    [CreateAssetMenu(fileName = "TilesSetSo", menuName = "ScriptableObjects/TilesSetSo")]
    public class TilesSetSo : ScriptableObject
    {
        [SerializeField] private List<TileType> _set = new List<TileType>();
        public List<TileType> Set => _set;
    }
}