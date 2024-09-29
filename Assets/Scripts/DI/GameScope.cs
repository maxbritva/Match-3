using Game.Board;
using Game.FX;
using Game.GameManager;
using Game.GameProgress;
using Game.Grid;
using Game.MatchTiles;
using Game.Score;
using Game.Tiles;
using Game.Utils;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DI
{
    public class GameScope: LifetimeScope
    {
       [SerializeField] private GameBoard _gameBoard;
       [SerializeField] private EndGamePanelView _endGamePanelView;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IObjectResolver, Container>(Lifetime.Singleton);
            builder.Register<GameResourcesLoader>(Lifetime.Singleton);
            builder.RegisterInstance(_gameBoard);
            builder.RegisterInstance(_endGamePanelView);
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
            builder.Register<FXPool>(Lifetime.Scoped);
            builder.Register<EndGame>(Lifetime.Scoped);
        }
    }
}