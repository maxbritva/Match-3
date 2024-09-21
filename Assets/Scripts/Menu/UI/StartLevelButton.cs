using Menu.Levels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Menu.UI
{
    [RequireComponent(typeof(Button))] 
    public class StartLevelButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;
        [SerializeField] private Button _button;
        public int Number { get; private set; }
        private StartGame _startGame;
        private SetupLevelSequence _setupLevelSequence;

        private void OnEnable() => _button.onClick.AddListener(StartLevelButtonClick);
        private void OnDisable() => _button.onClick.RemoveListener(StartLevelButtonClick);

        public void SetNumber(int value) => Number = Mathf.Clamp(value,1,5);

        public void SetLabel() => _label.text = Number.ToString();

        public void SetButtonInteractable(bool value) => _button.interactable = value;

        private void StartLevelButtonClick() => _startGame.Start(_setupLevelSequence.CurrentLevelsSequence.LevelSequence[Number - 1]);

        [Inject] private void Construct(StartGame startGame, SetupLevelSequence setupLevelSequence)
        {
            _startGame = startGame;
            _setupLevelSequence = setupLevelSequence;
        }
        
    }
}