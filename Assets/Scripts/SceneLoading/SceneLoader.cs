﻿using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace SceneLoading
{
    public class SceneLoader: IAsyncSceneLoading
    {
        private readonly Dictionary<string, SceneInstance> _loadedScenes = new Dictionary<string, SceneInstance>();
        private LoadingView _loadingView;
        private CancellationTokenSource _cts;
        public SceneLoader(LoadingView loadingView) => _loadingView = loadingView;

        public async UniTask LoadAsync(string sceneName)
        {
            _cts = new CancellationTokenSource();
            _loadingView.SetActiveScreen(true);
            await UniTask.Delay(TimeSpan.FromSeconds(2f), _cts.IsCancellationRequested);
            var loadedScene = await Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Additive).WithCancellation(_cts.Token);
            SceneManager.SetActiveScene(loadedScene.Scene);
            _loadedScenes.Add(sceneName, loadedScene);
            _loadingView.SetActiveScreen(false);
            _cts.Cancel();
        }

        public async UniTask UnloadAsync(string sceneName)
        {
            _cts = new CancellationTokenSource();
            var sceneToUnload = _loadedScenes[sceneName];
            await Addressables.UnloadSceneAsync(sceneToUnload).WithCancellation(_cts.Token).AsUniTask();
            _cts.Cancel();
        }
    }
}