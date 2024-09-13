using Game;
using Game.Board;
using Game.GameLoop;
using Game.GameManager;
using Game.Grid;
using Game.Tiles;
using Game.Utils;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DI
{
    public class GameScope: LifetimeScope
    {
       [SerializeField] private GameBoard gameBoard;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IObjectResolver, Container>(Lifetime.Singleton);
            builder.RegisterInstance(gameBoard);
            builder.RegisterEntryPoint<GameEntryPoint>();
            builder.Register<GridCoordinator>(Lifetime.Singleton);
            builder.Register<TileCreator>(Lifetime.Scoped);
            builder.Register<GenerateBlankTiles>(Lifetime.Singleton);
         
            builder.Register<GameDebug>(Lifetime.Singleton);
            //builder.Register<InputReader>(Lifetime.Singleton);
            builder.Register<BoardInteraction>(Lifetime.Singleton);
            builder.Register<MatchFinder>(Lifetime.Singleton);
            builder.Register<GameLooping>(Lifetime.Singleton);
            builder.Register<SetupCamera>(Lifetime.Singleton);
        }
    }
}