using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Menu.Levels
{
    public class SetupLevelSequence
    {
        public LevelsSequence CurrentLevelsSequence { get; private set; } 
        
        public async UniTask Setup(int currentLevel)
        {
            if (currentLevel <= 5)
                await LoadLevelsSequence("Levels1-5");
            else
                await LoadLevelsSequence("Levels6-10");
        }
        
        private async UniTask LoadLevelsSequence(string key)
        {
            AsyncOperationHandle<LevelsSequence> levels = Addressables.LoadAssetAsync<LevelsSequence>(key);
            await levels.ToUniTask();
            if (levels.Status == AsyncOperationStatus.Succeeded)
            {
                CurrentLevelsSequence = levels.Result;
                Addressables.Release(levels);
            }
        }
    }
}