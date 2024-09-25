using Animations;
using Audio;
using Boot.EntryPoint;
using Data;
using SceneLoading;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DI
{
    public class RootScope : LifetimeScope
    {
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private LoadingView _loadingView;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<BootEntryPoint>();
            builder.Register<GameData>(Lifetime.Singleton);
            builder.Register<IAsyncSceneLoading,SceneLoader>(Lifetime.Singleton);
            builder.Register<IAnimation, AnimationManager>(Lifetime.Singleton);
            builder.RegisterInstance(_audioManager);
            builder.RegisterInstance(_loadingView);
        }
    }
}