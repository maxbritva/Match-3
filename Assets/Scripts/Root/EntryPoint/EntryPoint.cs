using GameRoot;
using SceneLoading;
using UnityEngine;

namespace Root.EntryPoint
{
    public class EntryPoint
    {
        private static EntryPoint _instance;
        private SceneLoader _sceneLoader;
        private UIRootView _uiRootView;
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize()
        {
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            _instance = new EntryPoint();
            _instance.StartGame();
        }
        
        private EntryPoint()
        {
            _sceneLoader = new SceneLoader();
            var prefabUIRoot = Resources.Load<UIRootView>("UIRoot");
            _uiRootView = Object.Instantiate(prefabUIRoot);
            Object.DontDestroyOnLoad(_uiRootView.gameObject);
        }
        

        private void StartGame()
        {
            
        }
    }
}