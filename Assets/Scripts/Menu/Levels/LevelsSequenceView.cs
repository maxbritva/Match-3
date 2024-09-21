using System;
using System.Collections.Generic;
using Data;
using Menu.UI;
using UnityEngine;
using VContainer;

namespace Menu.Levels
{
    public class LevelsSequenceView: MonoBehaviour
    {
        [SerializeField] private List<StartLevelButton> _levelButtons = new List<StartLevelButton>();
        private SetupLevelSequence _setupLevelSequence;
        private void OnValidate()
        {
            if (_levelButtons.Count != 5)
                throw new ArgumentOutOfRangeException("Level buttons must contain 5 elements");
        }

        public void SetupButtonsView(int currentLevel)
        {
            for (int i = 0; i < _levelButtons.Count; i++)
            {
                _levelButtons[i].SetNumber(_setupLevelSequence.CurrentLevelsSequence.LevelSequence[i].Number);
                _levelButtons[i].SetLabel();
                if(_levelButtons[i].Number >  currentLevel)
                    _levelButtons[i].SetButtonInteractable(false);
            } 
        }

        private void CheckLevelButtonAvailable()
        {
            
        }

        [Inject] private void Construct(SetupLevelSequence setupLevelSequence) => _setupLevelSequence = setupLevelSequence;
    }
}