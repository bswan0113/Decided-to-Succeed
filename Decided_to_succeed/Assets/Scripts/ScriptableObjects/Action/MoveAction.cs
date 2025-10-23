using System;
using System.Threading;
using Core.Logging;
using Cysharp.Threading.Tasks;
using Feature.Context;
using UnityEngine;

namespace ScriptableObjects.Action
{
    [Serializable]
    public class MoveAction : IAction
    {
        [SerializeField] private Waypoint _waypointData;
        [SerializeField] private float _duration = 1f;
        [SerializeField] private Vector3 _offset = Vector3.zero;

        public async UniTask ExecuteAsync(ActionContext context, CancellationToken token)
        {
            if (_waypointData == null)
            {
                CLogger.LogError("[MoveAction] WaypointData is not assigned!");
                return;
            }
            context.PlayerController.SetControllable(false);
            Vector3 targetPosition = _waypointData.Position + _offset;
            await MoveToPosition(context.PlayerController.transform, targetPosition, _duration, token);
            context.PlayerController.SetControllable(true);
        }

        private async UniTask MoveToPosition(Transform target, Vector3 destination, float duration, CancellationToken token)
        {
            Vector3 startPosition = target.position;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                token.ThrowIfCancellationRequested();

                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / duration);
                target.position = Vector3.Lerp(startPosition, destination, t);

                await UniTask.Yield(token);
            }

            target.position = destination;
        }
    }
}