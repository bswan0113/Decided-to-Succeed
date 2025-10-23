using Core.Dependency;
using Feature.Cutscene;
using Feature.Player;
using UnityEngine;

namespace Feature.Character
{
    public class Lorian : MonoBehaviour
    {
        [SerializeField] private ScriptableObjects.Cutscene _lorianCutscene;


        private bool _hasBeenTriggered = false;
        private CutsceneManager _cutsceneManager;

        void Start()
        {
            _cutsceneManager = ServiceLocator.Get<CutsceneManager>();
        }

        private async void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Lorian triggered");
            if (_cutsceneManager.IsPlaying)
            {
                return;
            }
            var player = other.GetComponent<PlayerController>();
            player.SetControllable(false);
            if (player != null)
            {
                _hasBeenTriggered = true;
               await _cutsceneManager.PlayCutscene(_lorianCutscene, player);

            }
        }

    }
}