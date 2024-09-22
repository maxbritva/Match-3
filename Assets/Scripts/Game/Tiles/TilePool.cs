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
        private TilesLoader _tilesLoader;

        public TilePool(IObjectResolver objectResolver, TilesLoader tilesLoader)
        {
            _tilesLoader = tilesLoader;
            _objectResolver = objectResolver;
        }

        public Tile GetTileFromPool(Vector3 position, Transform parent)
        {
            for (int i = 0; i < _tilesPool.Count; i++)
            {
                if(_tilesPool[i].GameObject().activeInHierarchy) continue;
                _tilesPool[i].SetType(GetRandomType());
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
            var tilePrefab = _objectResolver.Instantiate(_tilesLoader.TilePrefab, position, Quaternion.identity, parent);
            var tile = tilePrefab.GameObject().GetComponent<Tile>();
            tile.SetType(_tilesLoader.BlankTile);
            return tile;
        }

        private Tile CreateTile(Vector3 position, Transform parent)
        {
            var tilePrefab = _objectResolver.Instantiate(_tilesLoader.TilePrefab, position, Quaternion.identity, parent);
            var tile = tilePrefab.GameObject().GetComponent<Tile>();
            tile.SetType(GetRandomType());
            _tilesPool.Add(tile);
            return tile;
        }
        private TileType GetRandomType() => _tilesLoader.CurrentTilesSet[Random.Range(0, _tilesLoader.CurrentTilesSet.Count)];
    }
}