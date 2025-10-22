using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEditor.Timeline.Actions;

namespace ScriptableObjects.Action
{
    public interface IAction
    {
        UniTask ExecuteAsync(ActionContext context, CancellationToken token);
    }
}