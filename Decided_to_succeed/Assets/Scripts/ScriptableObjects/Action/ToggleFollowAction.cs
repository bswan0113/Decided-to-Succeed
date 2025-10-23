using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Feature.Context;
using ScriptableObjects.Action;
using UnityEngine;

[Serializable]
public class ToggleFollowAction : IAction
{
    [SerializeField] private bool _isFollowing;

    public async UniTask ExecuteAsync(ActionContext context, CancellationToken token)
    {
        if (context.Lothric != null) context.Lothric.SetFollowing(_isFollowing);
        await UniTask.Yield(token);
    }
}