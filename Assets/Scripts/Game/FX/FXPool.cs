using System.Collections.Generic;
using Game.Tiles;
using Unity.VisualScripting;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.FX
{
    public class FXPool
    {
        private readonly List<GameObject> _FXPool = new List<GameObject>();
        private GameObject _prefabFX;
        private readonly IObjectResolver _objectResolver;
        private readonly GameResourcesLoader _gameResourcesLoader;
        
        public FXPool(IObjectResolver objectResolver, GameResourcesLoader gameResourcesLoader)
        {
            _gameResourcesLoader = gameResourcesLoader;
            _objectResolver = objectResolver;
        }

        public GameObject GetFXFromPool(Vector3 position, Transform parent)
        {
            for (int i = 0; i < _FXPool.Count; i++)
            {
                if(_FXPool[i].activeInHierarchy) continue;
                _FXPool[i].GameObject().transform.position = position;
                _FXPool[i].GameObject().SetActive(true);
                return  _FXPool[i];
            }
            var fx = CreateFX(position, parent);
            fx.SetActive(true);
            return fx;
        }
        private GameObject CreateFX(Vector3 position, Transform parent)
        {
            var fxPrefab = _objectResolver.Instantiate(_gameResourcesLoader.FX, position + Vector3.forward, Quaternion.identity);
            fxPrefab.transform.SetParent(parent);
            _FXPool.Add(fxPrefab);
            return fxPrefab;
        }
    }
}