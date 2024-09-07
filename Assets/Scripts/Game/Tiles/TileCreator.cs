using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Tiles
{
    public class TileCreator
    {
        private IObjectResolver _objectResolver;
        private Tile _tilePrefab;
        private TileType[] _tileTypes;

        public TileCreator(IObjectResolver objectResolver)
        {
            Debug.Log("init tilecreator");
            _objectResolver = objectResolver;
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

        public Tile CreateTile(Vector3 position, Transform parent)
        {
            Tile tile = _objectResolver.Instantiate(_tilePrefab, position, Quaternion.identity, parent);
            tile.SetType(_tileTypes[Random.Range(0, _tileTypes.Length)]);
            return tile;
        }
    }
}