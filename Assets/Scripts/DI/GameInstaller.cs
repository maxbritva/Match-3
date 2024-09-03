using Game;
using Game.Tiles;
using UnityEngine;
using Zenject;

namespace DI
{
    public class GameInstaller: MonoInstaller
    {
        [SerializeField] private BoardView _boardView;
        [SerializeField] private Board _board;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Coordinator>().FromNew().AsSingle().NonLazy();
            Container.Bind<Board>().FromInstance(_board).AsSingle().NonLazy();
            Container.Bind<BoardView>().FromInstance(_boardView).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameDebug>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<TileCreator>().FromNew().AsSingle().NonLazy();
        }
    }
}