using Menu;
using Menu.Levels;
using Menu.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DI
{
    public class MenuScope: LifetimeScope
    {
        [SerializeField] private MenuLevelsSequenceView _levelsSequenceView;
        [SerializeField] private MenuAnimator _menuAnimator;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<MenuEntryPoint>();
            builder.Register<SetupLevelSequence>(Lifetime.Singleton);
            builder.RegisterInstance(_levelsSequenceView);
            builder.RegisterInstance(_menuAnimator);
        }
    }
}