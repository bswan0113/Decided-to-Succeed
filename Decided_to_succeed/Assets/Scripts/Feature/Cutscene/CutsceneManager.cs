using System;
using System.Threading;
using Core.Dependency;
using Core.Logging;
using Cysharp.Threading.Tasks;
using Feature.CameraSystem;
using Feature.Character;
using Feature.Common;
using Feature.Context;
using Feature.Dialogue;
using Feature.Player;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Feature.Cutscene
{
    public class CutsceneManager : MonoBehaviour, IAsyncManager
    {
        private CancellationTokenSource _cancellationTokenSoruces;
        private bool _isPlaying;
        public bool IsPlaying => _isPlaying;
        private DialogueManager _dialogueManager;
        CameraManager _cameraManager;

        void Start()
        {
            InitializeManager();
        }

        void OnDestroy()
        {
            ShutdownManager();
        }

        public void InitializeManager()
        {
            _cancellationTokenSoruces?.Cancel();
            _cancellationTokenSoruces?.Dispose();
            _dialogueManager = ServiceLocator.Get<DialogueManager>();
            _cancellationTokenSoruces = new CancellationTokenSource();
            DontDestroyOnLoad(this);
            CLogger.Log("[CutsceneManager] Initialized");
        }

        public void ShutdownManager()
        {
            StopCutscene();
            _cancellationTokenSoruces?.Dispose();
            _cancellationTokenSoruces = null;
            CLogger.Log("[CutsceneManager] Shutdown");
        }

        public async UniTask PlayCutscene(CancellationToken token, ScriptableObjects.Cutscene cutscene, IPlayerControll playerController, Lothric lothric = null)
        {
            while (_isPlaying)
            {
                await UniTask.Yield(PlayerLoopTiming.Update, token);
            }
            if (_isPlaying)
            {
                CLogger.LogWarning("[CutsceneManager] Cutscene is already playing!");
                return;
            }

            if (cutscene == null)
            {
                CLogger.LogError("[CutsceneManager] Cutscene is null!");
                return;
            }
            var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(token, _cancellationTokenSoruces.Token);
            _isPlaying = true;
            _cameraManager = ServiceLocator.GetSceneInstance<CameraManager>();
            var context = new ActionContext(playerController, _dialogueManager, _cameraManager, lothric);

            try
            {
                CLogger.Log($"[CutsceneManager] Starting cutscene: {cutscene.name}");

                foreach (var action in cutscene.actions)
                {
                    if (action == null) continue;
                    await action.ExecuteAsync(context, linkedCts.Token);
                }

                CLogger.Log($"[CutsceneManager] Cutscene completed: {cutscene.name}");
            }
            catch (OperationCanceledException)
            {
                CLogger.Log("[CutsceneManager] Cutscene was cancelled");
            }
            finally
            {
                _isPlaying = false;
            }
        }

        private void StopCutscene()
        {
            _cancellationTokenSoruces?.Cancel();
            _isPlaying = false;
            CLogger.Log("[CutsceneManager] Cutscene stopped");
        }
    }
}