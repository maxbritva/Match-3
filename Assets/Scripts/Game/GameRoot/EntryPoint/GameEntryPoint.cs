
using Game.Utils;
using UnityEngine;

namespace Game.GameRoot.EntryPoint
{
    public class GameEntryPoint
    {
        private static GameEntryPoint _instance;
        private Coroutines _coroutines;
        private UIRootView _uiRootView;
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        // public static void Initialize()
        // {
        //     Application.targetFrameRate = 60;
        //     Screen.sleepTimeout = SleepTimeout.NeverSleep;
        //     _instance = new GameEntryPoint();
        //     _instance.StartGame();
        // }
        //
        // private GameEntryPoint()
        // {
        //     _coroutines = new GameObject("[COROUTINES]").AddComponent<Coroutines>();
        //     Object.DontDestroyOnLoad(_coroutines.gameObject);
        //
        //     var prefabUIRoot = Resources.Load<UIRootView>("UIRoot");
        //     _uiRootView = Object.Instantiate(prefabUIRoot);
        //     Object.DontDestroyOnLoad(_uiRootView.gameObject);
        // }
        //

        private void StartGame()
        {
            
        }
    }
}