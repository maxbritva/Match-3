using Menu;
using VContainer;
using VContainer.Unity;

namespace DI
{
    public class MenuScope: LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<MenuEntryPoint>();
        }
    }
}