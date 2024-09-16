using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Tiles
{
    public class TilePool
    {
        private List<Tile> _tilesPool = new List<Tile>();
        private IObjectResolver _objectResolver;
        private Tile _tilePrefab;
        private TileType[] _tileTypes;
        private TileType _blank;

        public TilePool(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
            _tilePrefab = Resources.Load<Tile>("Prefabs/TilePrefab");
            _tileTypes = new[]
            {
                Resources.Load<TileType>("Tiles/Gray"),
                Resources.Load<TileType>("Tiles/Blue"),
                Resources.Load<TileType>("Tiles/Yellow"),
                //Resources.Load<TileType>("Tiles/White"),
                Resources.Load<TileType>("Tiles/Red"),
                Resources.Load<TileType>("Tiles/Orange"),
            };
            _blank = Resources.Load<TileType>("Tiles/Blank");
        }

        public Tile GetTileFromPool(Vector3 position, Transform parent)
        {
            for (int i = 0; i < _tilesPool.Count; i++)
            {
                if(_tilesPool[i].GameObject().activeInHierarchy) continue;
                _tilesPool[i].SetType(_tileTypes[Random.Range(0, _tileTypes.Length)]);
                _tilesPool[i].GameObject().transform.position = position;
                _tilesPool[i].GameObject().SetActive(true);
                return  _tilesPool[i];
            }
            var tile = CreateTile(position, parent);
            tile.GameObject().SetActive(true);
            return tile;
        }

        public Tile CreateBlankTile(Vector3 position, Transform parent)
        {
            Tile tile = _objectResolver.Instantiate(_tilePrefab, position, Quaternion.identity, parent);
            tile.SetType(_blank);
            return tile;
        }

        private Tile CreateTile(Vector3 position, Transform parent)
        {
            Tile tile = _objectResolver.Instantiate(_tilePrefab, position, Quaternion.identity, parent);
            tile.SetType(_tileTypes[Random.Range(0, _tileTypes.Length)]);
            _tilesPool.Add(tile);
            return tile;
        }
    }
}