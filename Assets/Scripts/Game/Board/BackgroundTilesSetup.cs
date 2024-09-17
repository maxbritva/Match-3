using DG.Tweening;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Board
{
    public class BackgroundTilesSetup
    {
        private GameObject _backGroundTilePrefab;
        private Sprite _lightTile;
        private Sprite _darkTile;
        
        private IObjectResolver _objectResolver;
        
        public BackgroundTilesSetup(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
            _backGroundTilePrefab = Resources.Load<GameObject>("Prefabs/backgroundPrefab");
            _lightTile = Resources.Load<Sprite>("Sprites/Background/Light");
            _darkTile = Resources.Load<Sprite>("Sprites/Background/Dark");
        }
        public void SetupBackground(Transform parent, bool[,] blanks, int width, int height)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (blanks[x, y]) continue;
                    var backgroundTile = CreateBackgroundTile(
                        new Vector3(x + 0.5f, y + 0.5f, 0.1f), parent);
                    if (x % 2 == 0 && y % 2 == 0 || x % 2 != 0 && y % 2 != 0)
                        backgroundTile.GetComponent<SpriteRenderer>().sprite = _darkTile;
                    else
                        backgroundTile.GetComponent<SpriteRenderer>().sprite = _lightTile;
                    AnimateBackground(backgroundTile);
                }
            }
        }
        public GameObject CreateBackgroundTile(Vector3 position, Transform parent) => 
            _objectResolver.Instantiate(_backGroundTilePrefab, position, Quaternion.identity, parent);

        private void AnimateBackground(GameObject target)
        {
            target.transform.localScale = Vector3.one * 0.1f;
            target.transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutBounce);
        }
    }
}