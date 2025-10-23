using UnityEngine;

namespace Feature.Player
{
    public interface IPlayerControll
    {

        public void SetControllable(bool isControllable);
        Transform transform { get; }
        GameObject gameObject { get; }
    }
}