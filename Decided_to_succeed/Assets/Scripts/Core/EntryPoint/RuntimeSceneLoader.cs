using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Core.EntryPoint
{
    public class RuntimeSceneLoader : MonoBehaviour
    {
        [SerializeField] private string defaultNextSceneName;
        private CancellationTokenSource _cancellationTokenSource;

        async UniTaskVoid Start(){

            _cancellationTokenSource = new CancellationTokenSource();
            var gameInitializer = new GameInitializer();
            await gameInitializer.StartAsync(_cancellationTokenSource.Token);
            LoadNextScene();
        }

        private void LoadNextScene()
        {
            string sceneToLoad = RuntimeInitializer.SceneToLoadAfterInitialization;

            if (!string.IsNullOrEmpty(sceneToLoad))
            {
                SceneManager.LoadScene(sceneToLoad);
            }
            else
            {
                SceneManager.LoadScene(defaultNextSceneName);
            }
        }
        
        private void OnDestroy()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }
    }
}