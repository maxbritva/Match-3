using Boot.EntryPoint;
using Data;
using UnityEngine;
using VContainer.Unity;

namespace Menu
{
    public class MenuEntryPoint: IInitializable
    {
        private GameData _gameData;
        private BootEntryPoint _bootEntryPoint;

        public MenuEntryPoint(GameData gameData, BootEntryPoint bootEntryPoint)
        {
            _gameData = gameData;
            _bootEntryPoint = bootEntryPoint;
        }

        public void Initialize()
        {
           Debug.Log(12);
        }
    }
}