using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace SceneLoading
{
    public interface IAsyncSceneLoading
    {
        UniTask LoadAsync(string sceneName);
        
        UniTask UnloadAsync(string sceneName);
    }
}