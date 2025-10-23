using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Feature.Context;
using ScriptableObjects.Action;
using UnityEngine;

namespace Feature.Interaction
{
    public class InteractionObject : MonoBehaviour, IInteractable
    {
        [SerializeReference] private List<IAction> _actions;
        private bool _isInteracting = false;

        public void Interact(ActionContext context)
        {
            if (_isInteracting) return;
            _isInteracting = true;
            foreach (var action in _actions)
            {
                var token = this.GetCancellationTokenOnDestroy();
                action.ExecuteAsync(context,token);
            }
            _isInteracting = false;
        }
    }
}