using Game;
using Game.Board;
using Game.Grid;
using Game.Tiles;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Zenject;

namespace DI
{
    public class GameInstaller: MonoInstaller
    {
        [SerializeField] private BoardInteraction boardInteraction;
        [SerializeField] private Board _board;
        [SerializeField] private InputReader _inputReader;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GridCoordinator>().FromNew().AsSingle().NonLazy();
            Container.Bind<Board>().FromInstance(_board).AsSingle().NonLazy();
            Container.Bind<BoardInteraction>().FromInstance(boardInteraction).AsSingle().NonLazy();
            Container.Bind<InputReader>().FromInstance(_inputReader).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameDebug>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<TileCreator>().FromNew().AsSingle().NonLazy();
        }
    }
}