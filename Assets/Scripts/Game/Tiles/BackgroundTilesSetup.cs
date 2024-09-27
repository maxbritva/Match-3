using System;
using System.Threading;
using Animations;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Tiles
{
    public class BackgroundTilesSetup: IDisposable
    {
        private readonly TilesLoader _tilesLoader;
        private CancellationTokenSource _сts;
        
        private IObjectResolver _objectResolver;
        public BackgroundTilesSetup(IObjectResolver objectResolver, TilesLoader tilesLoader)
        {
            _objectResolver = objectResolver;
            _tilesLoader = tilesLoader;
        }
        public async UniTask SetupBackground(Transform parent, bool[,] blanks, int width, int height, IAnimation animationManager)
        {
            _сts = new CancellationTokenSource();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (blanks[x, y]) continue;
                    var backgroundTile = CreateBackgroundTile(
                        new Vector3(x + 0.5f, y + 0.5f, 0.1f), parent);
                    if (x % 2 == 0 && y % 2 == 0 || x % 2 != 0 && y % 2 != 0)
                        backgroundTile.GetComponent<SpriteRenderer>().sprite = _tilesLoader.DarkTile;
                    else
                        backgroundTile.GetComponent<SpriteRenderer>().sprite = _tilesLoader.LightTile;
                    animationManager.Reveal(backgroundTile, 1f);
                }
            }
            await UniTask.Delay(TimeSpan.FromSeconds(0.3f), _сts.IsCancellationRequested);
            _сts.Cancel();
        }
        public GameObject CreateBackgroundTile(Vector3 position, Transform parent) => 
            _objectResolver.Instantiate(_tilesLoader.BackgroundTilePrefab, position, Quaternion.identity, parent);
        
        public void Dispose()
        {
            _сts?.Dispose();
            _objectResolver?.Dispose();
        }
    }
}