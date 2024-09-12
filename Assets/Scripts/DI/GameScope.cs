using Game;
using Game.Board;
using Game.Grid;
using Game.Tiles;
using Game.Utils;
using Input;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DI
{
    public class GameScope: LifetimeScope
    {
        [SerializeField] private Board _board;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IObjectResolver, Container>(Lifetime.Singleton);
            builder.Register<GridCoordinator>(Lifetime.Singleton);
            builder.Register<TileCreator>(Lifetime.Scoped);
            builder.Register<GenerateBlankTiles>(Lifetime.Singleton);
            builder.RegisterInstance(_board);
            builder.Register<GameDebug>(Lifetime.Singleton);
            //builder.Register<InputReader>(Lifetime.Singleton);
            builder.Register<BoardInteraction>(Lifetime.Singleton);
            builder.Register<GameLoop>(Lifetime.Singleton);
            builder.Register<SetupCamera>(Lifetime.Singleton);
        }
    }
}