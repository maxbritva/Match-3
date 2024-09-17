using System.Collections.Generic;
using Game.Tiles;
using UnityEngine;
using UnityEngine.Serialization;

namespace Level
{
    [CreateAssetMenu(fileName = "LevelConfiguration", menuName = "ScriptableObjects/LevelConfiguration")]
    public class LevelConfiguration : ScriptableObject
    {
        [Header("Grid")]
        [SerializeField] private List<BlankTile> _blankTilesLayout;
        [SerializeField] private List<TileType> _tilesSet;
        [SerializeField] private int _gridWidth; 
        [SerializeField] private int _gridHeight;
        
        [Header("level")]
        [SerializeField] private int _goalScore;
        [SerializeField] private int _moves;
       
        
        public List<BlankTile> BlankTilesLayout => _blankTilesLayout;
        public int GoalScore => _goalScore;
        public int Moves => _moves;
        public int GridWidth => _gridWidth;
        public int GridHeight => _gridHeight;
        public List<TileType> TilesSet => _tilesSet;
        // graphic set
        
    }
}