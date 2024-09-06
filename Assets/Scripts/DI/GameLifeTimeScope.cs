﻿using Game;
using Game.Board;
using Game.Grid;
using Game.Tiles;
using Input;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DI
{
    public class GameLifeTimeScope: LifetimeScope
    {

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<GridCoordinator>(Lifetime.Singleton);
            builder.Register<IObjectResolver, Container>(Lifetime.Scoped);
            builder.Register<Board>(Lifetime.Singleton);
            builder.Register<BoardInteraction>(Lifetime.Singleton);
            builder.Register<InputReader>(Lifetime.Singleton);
            builder.Register<GameDebug>(Lifetime.Singleton);
            builder.Register<TileCreator>(Lifetime.Singleton);
            builder.Register<GameLoop>(Lifetime.Singleton);
        }
    }
}