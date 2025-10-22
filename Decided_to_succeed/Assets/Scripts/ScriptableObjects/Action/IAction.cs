using System.Threading;
using Cysharp.Threading.Tasks;
using Feature.Context;


namespace ScriptableObjects.Action
{
    public interface IAction
    {
        UniTask ExecuteAsync(ActionContext context, CancellationToken token);
    }
}