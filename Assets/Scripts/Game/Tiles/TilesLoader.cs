using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Data;
using Level;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Game.Tiles
{
    public class TilesLoader
    {
        public Tile TilePrefab { get; private set; }
        public TileType BlankTile { get; private set; }
        public List<TileType> CurrentTilesSet { get; private set; }
        private GameData _gameData;

        public async void Load()
        {
            CurrentTilesSet = new List<TileType>();
            if (_gameData.CurrentLevel.TileSets == TileSets.Kingdom) 
                await LoadSet("KingdomSet");
            if (_gameData.CurrentLevel.TileSets == TileSets.Gem) 
                await LoadSet("GemSet");
            await LoadTilePrefab("TilePrefab");
            await LoadBlankTile("Blank");
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
        private async UniTask LoadTilePrefab(string key)
        {
            AsyncOperationHandle<Tile> tile = Addressables.LoadAssetAsync<Tile>(key);
            await tile.ToUniTask();
            if (tile.Status == AsyncOperationStatus.Succeeded)
            {
                TilePrefab = tile.Result;
                Addressables.Release(tile);
            } 
        }
        private async UniTask LoadBlankTile(string key)
        {
            AsyncOperationHandle<TileType> blankTile = Addressables.LoadAssetAsync<TileType>(key);
            await blankTile.ToUniTask();
            if (blankTile.Status == AsyncOperationStatus.Succeeded)
            {
                BlankTile = blankTile.Result;
                Addressables.Release(blankTile);
            } 
        }
    }
}