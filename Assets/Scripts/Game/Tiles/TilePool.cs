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
        private GameResourcesLoader _gameResourcesLoader;

        public TilePool(IObjectResolver objectResolver, GameResourcesLoader gameResourcesLoader)
        {
            _gameResourcesLoader = gameResourcesLoader;
            _objectResolver = objectResolver;
        }

        public Tile GetTileFromPool(Vector3 position, Transform parent)
        {
            for (int i = 0; i < _tilesPool.Count; i++)
            {
                if(_tilesPool[i].GameObject().activeInHierarchy) continue;
                _tilesPool[i].SetType(GetRandomType());
                _tilesPool[i].GameObject().transform.position = position;
                return  _tilesPool[i];
            }
            var tile = CreateTile(position, parent);
            tile.GameObject().SetActive(true);
            return tile;
        }
        
        public Tile CreateBlankTile(Vector3 position, Transform parent)
        {
            var tilePrefab = _objectResolver.Instantiate(_gameResourcesLoader.TilePrefab, position, Quaternion.identity, parent);
            var tile = tilePrefab.GameObject().GetComponent<Tile>();
            tile.SetType(_gameResourcesLoader.BlankTile);
            return tile;
        }

        private Tile CreateTile(Vector3 position, Transform parent)
        {
            var tilePrefab = _objectResolver.Instantiate(_gameResourcesLoader.TilePrefab, position, Quaternion.identity, parent);
            var tile = tilePrefab.GameObject().GetComponent<Tile>();
            tile.SetType(GetRandomType());
            _tilesPool.Add(tile);
            return tile;
        }
        private TileType GetRandomType() => _gameResourcesLoader.CurrentTilesSet[Random.Range(0, _gameResourcesLoader.CurrentTilesSet.Count)];
    }
}