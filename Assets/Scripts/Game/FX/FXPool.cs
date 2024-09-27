using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using VContainer;
using VContainer.Unity;
using IInitializable = VContainer.Unity.IInitializable;

namespace Game.FX
{
    public class FXPool: IInitializable
    {
        private List<GameObject> _FXPool = new List<GameObject>();
        private GameObject _prefabFX;
        private  IObjectResolver _objectResolver;
        
        public FXPool(IObjectResolver objectResolver) => _objectResolver = objectResolver;
        public async void Initialize()
        {
            await LoadFXPrefab();
            if(_prefabFX == null)
                Debug.Log("FX is null");
            if(_objectResolver == null)
                Debug.Log("resolver null");
        }
        
        

        public GameObject GetFXFromPool(Vector3 position, Transform parent)
        {
            for (int i = 0; i < _FXPool.Count; i++)
            {
                if(_FXPool[i].activeInHierarchy) continue;
                _FXPool[i].GameObject().transform.position = position;
                return  _FXPool[i];
            }
            var fx = CreateFX(position, parent);
            fx.SetActive(true);
            return fx;
        }
        private GameObject CreateFX(Vector3 position, Transform parent)
        {
            var fxPrefab = _objectResolver.Instantiate(_prefabFX, position + new Vector3(0,0,1f), Quaternion.identity);
            _FXPool.Add(fxPrefab);
            return fxPrefab;
        }
        
        public async UniTask LoadFXPrefab()
        {
            var fx = Addressables.LoadAssetAsync<GameObject>("FXPrefab");
            await fx.ToUniTask();
            if (fx.Status == AsyncOperationStatus.Succeeded)
            {
                _prefabFX = fx.Result;
                Addressables.Release(fx);
            }
            else
            {
                Debug.Log("FX is null");
            }
        }
    }
}