using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEditor.Timeline.Actions;
using UnityEngine;

namespace ScriptableObjects.Action
{
    [Serializable]
    public class MoveAction : IAction
    {
        [SerializeField] private Transform _destination;

        public async UniTask ExecuteAsync(ActionContext context, CancellationToken token)
        {
        }
    }
}