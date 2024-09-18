using DG.Tweening;
using TMPro;
using UnityEngine;
using VContainer;

namespace Game.GameProgress
{
    public class GameProgressView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _score;
        [SerializeField] private TMP_Text _goalScore;
        [SerializeField] private TMP_Text _moves;
        
        private GameProgress _gameProgress;
        private void OnEnable()
        {
            _gameProgress.OnScoreChanged += UpdateScore;
            _gameProgress.OnMove += UpdateMoves;
        }

        private void OnDisable()
        {
            _gameProgress.OnScoreChanged -= UpdateScore;
            _gameProgress.OnMove -= UpdateMoves;
        }
        private void Start()
        {
            _goalScore.text = _gameProgress.GoalScore.ToString();
            _score.text = _gameProgress.Score.ToString();
            _moves.text = _gameProgress.Moves.ToString();
        }

        private void UpdateScore()
        {
            _score.text = _gameProgress.Score.ToString();
            AnimateText(_score);
        }

        private void UpdateMoves()
        {
            _moves.text = _gameProgress.Moves.ToString();
           AnimateText(_moves);
        }

        private void AnimateText(TMP_Text text) => text.transform.DOPunchScale(Vector3.one * 0.3f, 0.3f, 1, 0.5f);

        [Inject] private void Construct(GameProgress gameProgress) => _gameProgress = gameProgress;
    }
}