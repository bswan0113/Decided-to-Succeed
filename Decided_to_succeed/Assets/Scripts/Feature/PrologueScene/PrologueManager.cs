using Core.Dependency;
using Feature.Context;
using Feature.Cutscene;
using Feature.Dialogue;
using Feature.NPC;
using Feature.Player;
using UnityEngine;

namespace Feature.PrologueScene
{
    public class PrologueManager : MonoBehaviour
    {
        [SerializeField] private ScriptableObjects.Cutscene _introCutscene;
        [SerializeField] private PlayerController _playerController;
        [SerializeReference] private Lothric _lothric;
        private CutsceneManager _cutsceneManager;

        public PrologueManager()
        {
            _cutsceneManager = ServiceLocator.Get<CutsceneManager>();
        }

        async void Start()
        {
            await _cutsceneManager.PlayCutscene(_introCutscene, _playerController, _lothric);
        }
    }
}