using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Menu.Levels
{
    [RequireComponent(typeof(Button))] 
    public class StartLevelButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;
        public int Number { get; private set; }
        public TMP_Text Label => _label;
        private Button _button;
        
        private void OnEnable()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(StartLevelButtonClick);
        }
        private void OnDisable() => _button.onClick.RemoveListener(StartLevelButtonClick);

        public void SetNumber(int value) => Number = Mathf.Clamp(value,1,5);

        public void SetLabel() => Label.text = Number.ToString();

        public void StartLevelButtonClick()
        {
          
        }
        
    }
}