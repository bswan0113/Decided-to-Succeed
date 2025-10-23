using System;
using System.Threading;
using Core.Logging;
using Cysharp.Threading.Tasks;
using Feature.Context;
using UnityEngine;

namespace ScriptableObjects.Action
{
    [Serializable]
    public class ShowDialogueAction : IAction
    {
        [SerializeField, TextArea(3, 5)] private string _dialogueText;
        [SerializeField] private float _dialogueDuration = 2;
        [SerializeField] private Vector3 _bubbleOffset = new Vector3(1f, 2f, 0);

        public async UniTask ExecuteAsync(ActionContext context, CancellationToken token)
        {
            await context.DialogueManager.ShowDialogue(_dialogueText, context.PlayerController.transform, _dialogueDuration,_bubbleOffset , token);
        }
    }
}