
using Cysharp.Threading.Tasks;
using Feature.Context;

public interface IInteractable
{
    UniTask Interact(ActionContext context);
}