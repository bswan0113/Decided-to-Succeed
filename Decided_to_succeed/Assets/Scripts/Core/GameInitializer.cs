// C:\Workspace\Tomorrow Never Comes\Core\LifetimeScope\Parent\GameInitializer.cs (REFACTORED & POLISHED)

using System;
using System.Threading;
using Core.Dependency;
using Core.Logging;
using Core.Resource;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core
{
    public class GameInitializer
    {

        public GameInitializer()
        {
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            CLogger.Log("[GameInitializer] Starting game initialization sequence...");

            try
            {
                var gameManager = new GameManager();
                ServiceLocator.Register(gameManager);

                var resourceManager = new ResourceManager();
                ServiceLocator.Register(resourceManager);
                CLogger.Log("[GameInitializer] Game initialization sequence completed successfully. All systems nominal.");
            }
            catch (OperationCanceledException)
            {
                CLogger.LogWarning("[GameInitializer] Game initialization was cancelled.");
            }
            catch (Exception ex)
            {
                CLogger.LogCritical($"[GameInitializer] CRITICAL: A fatal exception occurred during game initialization. Boot sequence aborted. \nException: {ex.Message}\n{ex.StackTrace}");
                #if UNITY_EDITOR
                Debug.Break();
                #endif
            }
        }

    }
}