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
            builder.Register<GridSystem>(Lifetime.Singleton);
            builder.Register<TilePool>(Lifetime.Scoped);
            builder.Register<BlankTilesLevelSetup>(Lifetime.Singleton);
            builder.Register<GameDebug>(Lifetime.Singleton);
            //builder.Register<InputReader>(Lifetime.Singleton);
            builder.Register<MatchFinder>(Lifetime.Singleton);
            builder.Register<GameLooping>(Lifetime.Singleton);
            builder.Register<SetupCamera>(Lifetime.Singleton);
        }
    }
}