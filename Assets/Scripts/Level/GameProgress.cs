﻿using System;

namespace Level
{
    public class GameProgress
    {
        public event Action OnScoreChanged;
        public event Action OnMove;
        public int Score { get; private set; }
        public int GoalScore { get; private set; }
        public int Moves { get; private set; }
        
        public void LoadLevelConfiguration(int goalScore, int moves)
        {
            Score = 0;
            GoalScore = goalScore;
            Moves = moves;
        }

        public void AddScore(int value)
        {
            if(value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            Score += value;
            OnScoreChanged?.Invoke();
        }

        public bool CheckGoalScore() => Score >= GoalScore;

        public void SpendMove()
        {
            Moves--;
            OnMove?.Invoke();
        }
    }
}