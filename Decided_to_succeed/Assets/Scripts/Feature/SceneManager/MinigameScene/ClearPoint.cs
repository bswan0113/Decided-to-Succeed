using System;
using Feature.Player;
using UnityEngine;

namespace Feature.PrologueScene
{
    public class ClearPoint :MonoBehaviour
    {
        public static event Action OnPlayerReachedGoal;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<PlayerControllerMini>() != null)
            {
                OnPlayerReachedGoal?.Invoke();
                gameObject.GetComponent<Collider2D>().enabled = false;
            }
        }
    }
}