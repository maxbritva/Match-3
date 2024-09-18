using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Menu.Levels
{
    [RequireComponent(typeof(Button))] [RequireComponent(typeof(TMP_Text))]
    public class StartLevelButton : MonoBehaviour
    {
        public int Number { get; private set; }
        public TMP_Text Label { get; private set; }
        private Button _button;
        
        private StartLevel _startLevel;

        private void OnEnable()
        {
            _button = GetComponent<Button>();
            Label = GetComponent<TMP_Text>();
            _button.onClick.AddListener(StartLevelButtonClick);
        }
        private void OnDisable() => _button.onClick.RemoveListener(StartLevelButtonClick);

        public int SetNumber(int value) => Number = Mathf.Clamp(value,1,5);

        public void SetLabel() => Label.text = Number.ToString();

        public void StartLevelButtonClick()
        {
          
        }
        
        [Inject] private void Construct(StartLevel startLevel) => _startLevel = startLevel;
    }
}