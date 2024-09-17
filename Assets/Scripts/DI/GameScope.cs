using Game.Board;
using Game.GameManager;
using Game.Grid;
using Game.MatchTiles;
using Game.Score;
using Game.Tiles;
using Game.Utils;
using Level;
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
            builder.Register<GameProgress>(Lifetime.Singleton);
            builder.Register<TilePool>(Lifetime.Scoped);
            builder.Register<BlankTilesSetup>(Lifetime.Singleton);
            builder.Register<BackgroundTilesSetup>(Lifetime.Singleton);
            builder.Register<GameDebug>(Lifetime.Singleton);
            builder.Register<MatchFinder>(Lifetime.Singleton);
            builder.Register<ScoreCalculator>(Lifetime.Singleton);
            builder.Register<SetupCamera>(Lifetime.Singleton);
        }
    }
}