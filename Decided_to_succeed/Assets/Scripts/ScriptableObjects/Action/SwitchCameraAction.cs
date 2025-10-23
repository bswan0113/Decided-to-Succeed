// Assets/Scripts/ScriptableObjects/Action/SwitchCameraAction.cs
using System;
using System.Threading;
using Core.Dependency;
using Cysharp.Threading.Tasks;
using Feature.Camera; // CameraManager를 사용하기 위해 추가
using Feature.Context;
using ScriptableObjects.Action;
using UnityEngine;

[Serializable]
public class SwitchCameraAction : IAction
{
    [SerializeField] private string _targetCameraId;

    public async UniTask ExecuteAsync(ActionContext context, CancellationToken token)
    {
        context.CameraManager.SwitchToCamera(_targetCameraId);
    }
}