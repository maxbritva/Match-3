using System.Collections.Generic;
using Level;
using Unity.VisualScripting;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Tiles
{
    public class TilePool
    {
        private LevelConfiguration _levelConfiguration;
        private List<Tile> _tilesPool = new List<Tile>();
        private IObjectResolver _objectResolver;
        private Tile _tilePrefab;
        private List<TileType> _tileTypes;
        private TileType _blank;

        public TilePool(IObjectResolver objectResolver, LevelConfiguration levelConfiguration)
        {
            _levelConfiguration = levelConfiguration;
            _objectResolver = objectResolver;
            _tilePrefab = Resources.Load<Tile>("Prefabs/TilePrefab");
            _tileTypes = _levelConfiguration.TilesSet;
            _blank = Resources.Load<TileType>("Tiles/Blank");
        }

        public Tile GetTileFromPool(Vector3 position, Transform parent)
        {
            for (int i = 0; i < _tilesPool.Count; i++)
            {
                if(_tilesPool[i].GameObject().activeInHierarchy) continue;
                _tilesPool[i].SetType(_tileTypes[Random.Range(0, _tileTypes.Count)]);
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
            tile.SetType(_tileTypes[Random.Range(0, _tileTypes.Count)]);
            _tilesPool.Add(tile);
            return tile;
        }
    }
}