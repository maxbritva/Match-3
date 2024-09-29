using System;
using Level;

namespace Data
{
    public class GameData
    {
        public LevelConfiguration CurrentLevel { get; private set; }
        public int CurrentLevelIndex { get; private set; }
        public bool IsEnabledSound { get; private set; }

        public GameData()
        {
            IsEnabledSound = true;
            CurrentLevelIndex = 2;
        }
        public void SetCurrentLevelIndex(int value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            CurrentLevelIndex = value;
        }

        public void OpenNextLevel() => CurrentLevelIndex++;

        public bool SetSoundEnable(bool value) => IsEnabledSound = value;

        public void SetCurrentLevel(LevelConfiguration levelToSet)
        {
            if (levelToSet != null) CurrentLevel = levelToSet;
        }
    }
}