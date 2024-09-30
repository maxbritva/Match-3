using System;
using System.Threading;
using Animations;
using Audio;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.GameProgress;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game.FX
{
    public class EndGamePanelView : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private RectTransform _window;
        [SerializeField] private Button _closeButton;
        [SerializeField] private TMP_Text _titleText;
        private IAnimation _animationManager;
        private AudioManager _audioManager;
        private EndGame _endGame;
        private CancellationTokenSource _сts;
        private bool _isWinCondition;

        private readonly string _win = "You have won!";
        private readonly string _loose = "You have loose!";
        private void OnEnable() => _closeButton.onClick.AddListener(ExitGame);
        private void OnDisable() => _closeButton.onClick.RemoveListener(ExitGame);
        private async UniTask StartAnimation()
        {
            _сts = new CancellationTokenSource();
            _audioManager.PlayWhoosh();
            _panel.SetActive(true); 
            _animationManager.MoveUI(_window, new Vector3(0f, -115f, 0), 0.5f, Ease.InOutBack);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), _сts.IsCancellationRequested);
            _audioManager.StopMusic();
            if (_isWinCondition)
                _audioManager.PlayWin();
            else
                _audioManager.PlayLoose();
        }
        
        public async void ShowEndGamePanel(bool isWinCondition)
        {
            _isWinCondition = isWinCondition;
            _titleText.text = _isWinCondition ? _win : _loose;
            await StartAnimation();
            _closeButton.interactable = true;
        }
        
        private void ExitGame() => _endGame.End(_isWinCondition);
        
        [Inject] private void Construct(AudioManager audioManager, IAnimation animationManager, EndGame endGame)
        {
            _endGame = endGame;
            _audioManager = audioManager;
            _animationManager = animationManager;
        }

    }
}