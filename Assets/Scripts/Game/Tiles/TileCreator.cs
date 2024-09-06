using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Tiles
{
    public class TileCreator
    {
        // private DiContainer _diContainer;
        private Tile _tilePrefab;
        private TileType[] _tileTypes;
        private Board.Board _board;
        private IObjectResolver  _container;

        public TileCreator()
        {
            _tilePrefab = Resources.Load<Tile>("Prefabs/TilePrefab");
            _tileTypes = new[]
            {
                Resources.Load<TileType>("Tiles/Blue"),
                Resources.Load<TileType>("Tiles/Green"),
                Resources.Load<TileType>("Tiles/Purple"),
                Resources.Load<TileType>("Tiles/White"),
                Resources.Load<TileType>("Tiles/Red"),
                Resources.Load<TileType>("Tiles/Orange"),
            };
        }

        public TileCreator(Board.Board board, IObjectResolver container)
        {
            _board = board;
            _container = container;
        }

        public void CreateTile(int x, int y, Vector3 position, Transform parent)
        {
           // Tile tile = _diContainer.InstantiatePrefabForComponent<Tile>(_tilePrefab, position, Quaternion.identity, parent);
           Tile tile = _container.Instantiate(_tilePrefab, position, Quaternion.identity, parent);
            tile.SetType(_tileTypes[Random.Range(0, _tileTypes.Length)]);
            _board.Grid.SetValue(x,y, tile);
        }

        // [Inject] private void Construct(IObjectResolver container, Board.Board board)
        // {
        //     _container = container;
        //     _board = board;
        // }
    }
}