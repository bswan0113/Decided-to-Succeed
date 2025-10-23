using System.Threading;
using Core;
using Core.Dependency;
using Cysharp.Threading.Tasks;
using Feature.Context;
using ScriptableObjects.Action;
using UnityEngine;

namespace ScriptableObjects
{
    public class SceneTransitionAction : IAction
    {
        [SerializeField] private string sceneName;
        public async UniTask ExecuteAsync(ActionContext context, CancellationToken token)
        {
            var sceneManager = ServiceLocator.Get<SceneTransitionManager>();
            await sceneManager.FadeAndLoadScene(sceneName);
        }
    }
}