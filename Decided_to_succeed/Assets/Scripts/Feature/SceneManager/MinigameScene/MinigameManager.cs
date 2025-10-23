using System.Threading;
using Core.Dependency;
using Cysharp.Threading.Tasks;
using Feature.Character;
using Feature.Common;
using Feature.Context;
using Feature.Cutscene;
using Feature.Dialogue;
using Feature.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Feature.PrologueScene
{
    public class MinigameManager : MonoBehaviour, IAsyncManager
    {
        [SerializeField] private ScriptableObjects.Cutscene _introCutscene;
        [SerializeField]private PlayerControllerMini _playerController;
        private CutsceneManager _cutsceneManager;
        [SerializeField] private ScriptableObjects.Cutscene nextCutscene;
        private Scene _currentScene;
        private CancellationTokenSource _cancellationTokenSource;
        private void OnEnable()
        {
            ClearPoint.OnPlayerReachedGoal += HandleClear;
        }

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

                _cutsceneManager.PlayCutscene(linkedCts.Token, _introCutscene, _playerController).Forget();
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
            ShutdownManager();
            ServiceLocator.OnSceneUnloaded(_currentScene);
        }
        private void HandleClear()
        {
            _cutsceneManager.PlayCutscene(_cancellationTokenSource.Token, nextCutscene, _playerController).Forget();
        }

    }
}