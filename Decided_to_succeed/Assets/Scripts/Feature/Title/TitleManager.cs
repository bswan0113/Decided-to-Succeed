using System;
using System.Threading;
using Core.Logging;
using Cysharp.Threading.Tasks;
using Feature.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Feature.Title
{
    public class TitleManager : MonoBehaviour, IAsyncManager
    {
        [SerializeField] private string nextSceneName = "PrologueScene";
        private bool _isSceneLoading = false;

        private CancellationTokenSource _cancellationTokenSource;

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
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();

            _cancellationTokenSource = new CancellationTokenSource();
            LoadSceneWhenInput(_cancellationTokenSource.Token).Forget();
            CLogger.Log("[TitleManager]: Async operations initialized.");
        }

        public void ShutdownManager()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
            CLogger.Log("[TitleManager]: Async operations shutdown.");
        }

        private async UniTask LoadSceneWhenInput(CancellationToken cancellationToken)
        {
            try
            {
                await UniTask.WaitUntil(() => Input.anyKeyDown, cancellationToken: cancellationToken);

                cancellationToken.ThrowIfCancellationRequested();

                if (_isSceneLoading) return;

                _isSceneLoading = true;
                SceneManager.LoadScene(nextSceneName);
            }
            catch (OperationCanceledException)
            {
                CLogger.Log("[TitleManager]: LoadSceneWhenInput operation cancelled.");
            }
            catch (Exception e)
            {
                CLogger.LogError($"[TitleManager]: LoadSceneWhenInput error - {e.Message}");
            }
        }
    }
}