using System.Collections.Generic;
using Game.Tiles;
using UnityEngine;
using UnityEngine.Serialization;

namespace Level
{
    [CreateAssetMenu(fileName = "LevelConfiguration", menuName = "ScriptableObjects/LevelConfiguration")]
    public class LevelConfiguration : ScriptableObject
    {
        [SerializeField] private int _levelGridWidth;
        [SerializeField] private int _levelGridHeight;
        [SerializeField] private List<BlockTile> _blockTilesLayout;
        
        public List<BlockTile> BlockTilesLayout => _blockTilesLayout;

        public int LevelGridWidth => _levelGridWidth;
        public int LevelGridHeight => _levelGridHeight;
        // graphic set
        
    }
}