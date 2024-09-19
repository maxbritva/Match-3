using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Menu.Levels
{
    public class MenuLevelsSequenceView: MonoBehaviour
    {
        [SerializeField] private List<StartLevelButton> _levelButtons = new List<StartLevelButton>();
        private SetupLevelSequence _setupLevelSequence;
        private void OnValidate()
        {
            if (_levelButtons.Count != 5)
                throw new ArgumentOutOfRangeException("Level buttons must contain 5 elements");
        }

        public void SetupButtonsView()
        {
            for (int i = 0; i < _levelButtons.Count; i++)
            {
                _levelButtons[i].SetNumber(_setupLevelSequence.CurrentLevelsSequence.LevelSequence[i].Number);
                _levelButtons[i].SetLabel();
            } 
        }

        [Inject] private void Construct(SetupLevelSequence setupLevelSequence) => _setupLevelSequence = setupLevelSequence;
    }
}