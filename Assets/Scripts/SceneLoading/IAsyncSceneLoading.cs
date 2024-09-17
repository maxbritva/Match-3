using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace SceneLoading
{
    public interface IAsyncSceneLoading
    {
        UniTask LoadAsync(Scene scene);
        
        UniTask UnloadAsync(Scene scene);
    }
}