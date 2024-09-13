using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Game.Pool
{
    public class GamePool
    {
        private Dictionary<string, List<GameObject>> _pool = new Dictionary<string, List<GameObject>>();

        private GameObject Create(GameObject prefab)
        {
            var newObject = _diContainer.InstantiatePrefab(prefab);
            newObject.SetActive(false);
            _pool[prefab.name].Add(newObject);
            newObject.transform.SetParent(_gameManager.transform);
            return newObject;
        }
        
        public GameObject GetFromPool(GameObject prefab)
        {
            if (_pool.ContainsKey(prefab.name) == false)
                _pool[prefab.name] = new List<GameObject>();

            if (_pool[prefab.name].Count > 0)
            {
                for (int i = 0; i < _pool[prefab.name].Count ; i++)
                {
                    var gameObject = _pool[prefab.name][i];
                    if(gameObject.activeInHierarchy) continue;
                    gameObject.SetActive(true);
                    return gameObject;
                }
            }
            var newObject = Create(prefab);
            newObject.SetActive(true);
            return newObject;
        }
        
        [Inject] private void Construct(DiContainer diContainer, GameManager gameManager)
        {
            _diContainer = diContainer;
            _gameManager = gameManager;
        }
    }
}