using System.Collections.Generic;
using Game.Tiles;
using UnityEngine;

namespace Level
{
    [CreateAssetMenu(fileName = "LevelConfiguration", menuName = "ScriptableObjects/LevelConfiguration")]
    public class LevelConfiguration : ScriptableObject
    {
        [Header("Grid")]
        [SerializeField] private List<BlankTile> _blankTilesLayout;
        [SerializeField] private int _gridWidth; 
        [SerializeField] private int _gridHeight;
        
        [Header("level")]
        [SerializeField] private int _goalScore;
        [SerializeField] private int _moves;
        
        [SerializeField] private int _levelNumber;
        [SerializeField] private TileSets _tileSets;
        public List<BlankTile> BlankTilesLayout => _blankTilesLayout;
        public int GoalScore => _goalScore;
        public int Moves => _moves;
        public int GridWidth => _gridWidth;
        public int GridHeight => _gridHeight;
        public TileSets TileSets => _tileSets;
        public int Number => _levelNumber;
    }

    public enum TileSets
    {
        Kingdom,
        Gem
    }
}