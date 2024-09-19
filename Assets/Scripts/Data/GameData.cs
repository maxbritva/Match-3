using System;

namespace Data
{
    public class GameData
    {
        public int CurrentLevel { get; private set; }
        
        public bool IsEnabledSound { get; private set; }

        public GameData()
        {
            IsEnabledSound = true;
            CurrentLevel = 2;
        }
        public void SetCurrentLevel(int value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            CurrentLevel = value;
        }

        public bool SetSoundEnable(bool value) => IsEnabledSound = value;
    }
}