using System;
using System.Collections.Generic;
using UnityEngine;

namespace Menu.Levels
{
    public class MenuLevelsSequenceView: MonoBehaviour
    {
        [SerializeField] private List<StartLevelButton> _levelButtons = new List<StartLevelButton>();
        private MenuEntryPoint
        private void OnValidate()
        {
            if (_levelButtons.Count != 5)
                throw new ArgumentOutOfRangeException("Level buttons must contain 5 elements");
        }

        private void OnEnable()
        {
            for (int i = 0; i < _levelButtons.Count; i++)
            {
                _levelButtons[i].SetNumber(_levelsSequence.LevelSequence[i].Number);
                _levelButtons[i].SetLabel();
            } 
        }
        
    }
}