using System;
using System.Collections.Generic;
using Level;
using UnityEngine;

namespace Menu.Levels
{
    [CreateAssetMenu(fileName = "LevelsSequenceSo", menuName = "ScriptableObjects/LevelsSequenceSo")]
    public class LevelsSequence : ScriptableObject
    {
        [SerializeField] private List<LevelConfiguration> _levelSequence = new List<LevelConfiguration>();
        public List<LevelConfiguration> LevelSequence => _levelSequence;
        private void OnValidate()
        {
            if (_levelSequence.Count != 5) throw new ArgumentOutOfRangeException("Levels sequence must contain 5 elements");
        }
    }
}