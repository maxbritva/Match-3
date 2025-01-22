using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Data;
using Level;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Game.Tiles
{
    public class GameResourcesLoader: IDisposable
    {
        public GameResourcesLoader(GameData gameData) => _gameData = gameData;

        public GameObject TilePrefab { get; private set; }
        
        public GameObject BackgroundTilePrefab { get; private set; }
        public TileType BlankTile { get; private set; }
        
        public Sprite LightTile { get; private set; }
        public Sprite DarkTile { get; private set; }
        
        public GameObject FX { get; private set; }
        public List<TileType> CurrentTilesSet { get; private set; }
        private GameData _gameData;

        private CancellationTokenSource _cts;

        public void Dispose() => _cts?.Dispose();
        public async UniTask Load()
        {
            _cts = new CancellationTokenSource();
            CurrentTilesSet = new List<TileType>();
            await LoadTileSet();
            await LoadTilePrefabs();
            BlankTile = await Loader<TileType>("Blank");
            await LoadBackgroundTiles();
            _cts.Cancel();
        }

        private async Task LoadTileSet()
        {
            switch (_gameData.CurrentLevel.TileSets)
            {
                case TileSets.Kingdom:
                {
                    var tileSets = await Loader<TilesSetSo>("KingdomSet");
                    CurrentTilesSet = tileSets.Set;
                    break;
                }
                case TileSets.Gem:
                {
                    var tileSets = await Loader<TilesSetSo>("GemSet");
                    CurrentTilesSet = tileSets.Set;
                    break;
                }
            }
        }

        private async UniTask<T> Loader<T>(string key)
        {
            var assetHandler = Addressables.LoadAssetAsync<T>(key);
            var asset = await assetHandler.ToUniTask();
            return assetHandler.Status == AsyncOperationStatus.Succeeded ? asset : default;
        }
        
        private async UniTask LoadTilePrefabs()
        {
            TilePrefab = await Loader<GameObject>("TilePrefab");
            BackgroundTilePrefab = await Loader<GameObject>("BackgroundPrefab");
            FX = await Loader<GameObject>("FXPrefab");
        }
    
        private async UniTask LoadBackgroundTiles()
        {
            LightTile = await Loader<Sprite>("BGLightTile");
            DarkTile = await Loader<Sprite>("BGDarkTile");
     
        }

     
    }
}