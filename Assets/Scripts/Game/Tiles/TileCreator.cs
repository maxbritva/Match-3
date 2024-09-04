using UnityEngine;
using Zenject;

namespace Game.Tiles
{
    public class TileCreator
    {
        private DiContainer _diContainer;
        private Tile _tilePrefab;
        private TileType[] _tileTypes;
        private Board.Board _board;

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

        public void CreateTile(int x, int y, Vector3 position, Transform parent)
        {
            Tile tile = _diContainer.InstantiatePrefabForComponent<Tile>(_tilePrefab, position, Quaternion.identity, parent);
            tile.SetType(_tileTypes[Random.Range(0, _tileTypes.Length)]);
            _board.Grid.SetValue(x,y, tile);
        }

        [Inject] private void Construct(DiContainer diContainer, Board.Board board)
        {
            _diContainer = diContainer;
            _board = board;
        }
    }
}