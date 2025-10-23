using System;
using System.Threading;
using Core.Logging;
using Cysharp.Threading.Tasks;
using Feature.Context;
using UnityEngine;

namespace ScriptableObjects.Action
{
    [Serializable]
    public class ChangeSpriteAction : IAction
    {
        [SerializeField] private string _targetObjectName;

        [SerializeField] private Sprite _newSprite;

        public async UniTask ExecuteAsync(ActionContext context, CancellationToken token)
        {

            GameObject targetObject = GameObject.Find(_targetObjectName);

            SpriteRenderer renderer = targetObject.GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                renderer.sprite = _newSprite;
            }
            else
            {
                GameObject.Destroy(targetObject);
            }
            await UniTask.Delay(TimeSpan.FromSeconds(3), cancellationToken: token);
            await UniTask.Yield(token);
        }
    }
}