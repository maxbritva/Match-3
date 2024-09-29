using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Data;
using Level;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Game.Tiles
{
    public class GameResourcesLoader
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

        public async UniTask Load()
        {
            CurrentTilesSet = new List<TileType>();
            if (_gameData.CurrentLevel.TileSets == TileSets.Kingdom) 
                await LoadSet("KingdomSet");
            if (_gameData.CurrentLevel.TileSets == TileSets.Gem) 
                await LoadSet("GemSet");
            await LoadTilePrefabs();
            await LoadBlankTile();
            await LoadBackgroundTiles();
        }
        private async UniTask LoadSet(string key)
        {
            AsyncOperationHandle<TilesSetSo> set = Addressables.LoadAssetAsync<TilesSetSo>(key);
            await set.ToUniTask();
            if (set.Status == AsyncOperationStatus.Succeeded)
            {
                CurrentTilesSet = set.Result.Set;
                Addressables.Release(set);
            }
        }
        private async UniTask LoadTilePrefabs()
        {
            var tile = Addressables.LoadAssetAsync<GameObject>("TilePrefab");
            var backgroundTile = Addressables.LoadAssetAsync<GameObject>("BackgroundPrefab");
            var fxPrefab = Addressables.LoadAssetAsync<GameObject>("FXPrefab");
            await tile.ToUniTask();
            await backgroundTile.ToUniTask();
            await fxPrefab.ToUniTask();
            if (tile.Status == AsyncOperationStatus.Succeeded && backgroundTile.Status == AsyncOperationStatus.Succeeded && fxPrefab.Status == AsyncOperationStatus.Succeeded)
            {
                TilePrefab = tile.Result;
                BackgroundTilePrefab = backgroundTile.Result;
                FX = fxPrefab.Result;
                Addressables.Release(tile);
                Addressables.Release(backgroundTile);
                Addressables.Release(fxPrefab);
            } 
        }
        private async UniTask LoadBlankTile()
        {
            var blankTile = Addressables.LoadAssetAsync<TileType>("Blank");
            await blankTile.ToUniTask();
            if (blankTile.Status == AsyncOperationStatus.Succeeded)
            {
                BlankTile = blankTile.Result;
                Addressables.Release(blankTile);
            } 
        }
        private async UniTask LoadBackgroundTiles()
        {
            var lightTile = Addressables.LoadAssetAsync<Sprite>("BGLightTile");
            var darkTile = Addressables.LoadAssetAsync<Sprite>("BGDarkTile");
            await lightTile.ToUniTask();
            await darkTile.ToUniTask();
            if (lightTile.Status == AsyncOperationStatus.Succeeded && darkTile.Status == AsyncOperationStatus.Succeeded)
            {
                LightTile = lightTile.Result;
                Addressables.Release(LightTile);
                DarkTile = darkTile.Result;
                Addressables.Release(darkTile);
            } 
        }
    }
}