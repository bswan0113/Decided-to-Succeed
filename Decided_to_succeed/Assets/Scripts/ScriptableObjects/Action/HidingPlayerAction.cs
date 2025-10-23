using System;
using System.Threading;
using Cinemachine;
using Core.Logging;
using Cysharp.Threading.Tasks;
using Feature.Context;
using Unity.VisualScripting;
using UnityEngine;

namespace ScriptableObjects.Action
{
    [Serializable]
    public class HidingPlayerAction : IAction
    {

        public async UniTask ExecuteAsync(ActionContext context, CancellationToken token)
        {
            var player = context.PlayerController.gameObject.GetComponent<SpriteRenderer>();
            Color color = player.color;
            player.color = new Color(color.r, color.g, color.b, 0);
        }
    }
}