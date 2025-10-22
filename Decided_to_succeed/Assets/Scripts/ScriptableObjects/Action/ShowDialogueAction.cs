using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEditor.Timeline.Actions;
using UnityEngine;

namespace ScriptableObjects.Action
{
    [Serializable]
    public class ShowDialogueAction : IAction
    {
        [SerializeField, TextArea(3, 5)] private string _dialogueText;


        public async UniTask ExecuteAsync(ActionContext context, CancellationToken token)
        {
        }
    }
}