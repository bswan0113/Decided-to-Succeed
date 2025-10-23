using System.Threading;
using Core.Dependency;
using Cysharp.Threading.Tasks;
using Feature.Character;
using Feature.Common;
using Feature.Cutscene;
using Feature.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Feature.PrologueScene
{
    public class BridgeManager : MonoBehaviour, IAsyncManager
    {
        [SerializeField] private ScriptableObjects.Cutscene _introCutscene;
        [SerializeField] private PlayerController _playerController;
        [SerializeReference] private Lothric _lothric;
        private CutsceneManager _cutsceneManager;
        private Scene _currentScene;
        private CancellationTokenSource _cancellationTokenSource;
        private void Awake()
        {
            _currentScene = SceneManager.GetActiveScene();
        }

        private void Start()
        {

            InitializeManager();
        }

        public void InitializeManager()
        {
            UniTask.Yield(PlayerLoopTiming.Update);
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();
            _playerController.SetControllable(false);
            _cutsceneManager = ServiceLocator.Get<CutsceneManager>();
            if (_cutsceneManager != null)
            {
                var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(
                    _cancellationTokenSource.Token,
                    this.GetCancellationTokenOnDestroy());

               _cutsceneManager.PlayCutscene(linkedCts.Token, _introCutscene, _playerController, _lothric).Forget();
            }
        }

        public void ShutdownManager()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }

        private void OnDestroy()
        {

            ServiceLocator.OnSceneUnloaded(_currentScene);
            ShutdownManager();
        }
    }
}