using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Feature.Context;
using UnityEngine;
// CameraManager를 사용하기 위해 추가

namespace ScriptableObjects.Action
{
    [Serializable]
    public class SwitchCameraAction : IAction
    {
        [SerializeField] private string _targetCameraId;

        public async UniTask ExecuteAsync(ActionContext context, CancellationToken token)
        {
            context.CameraManager.SwitchToCamera(_targetCameraId);
        }
    }
}