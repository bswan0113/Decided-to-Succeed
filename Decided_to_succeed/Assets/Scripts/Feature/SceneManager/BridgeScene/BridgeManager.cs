using Core.Dependency;
using Feature.Character;
using Feature.Context;
using Feature.Cutscene;
using Feature.Dialogue;
using Feature.Player;
using UnityEngine;

namespace Feature.PrologueScene
{
    public class BridgeManager : MonoBehaviour
    {
        [SerializeField] private ScriptableObjects.Cutscene _introCutscene;
        [SerializeField] private PlayerController _playerController;
        [SerializeReference] private Lothric _lothric;
        private CutsceneManager _cutsceneManager;

        async void Start()
        {
            _cutsceneManager = ServiceLocator.Get<CutsceneManager>();
            if (_cutsceneManager != null)
            {
                await _cutsceneManager.PlayCutscene(_introCutscene, _playerController, _lothric);
            }

        }
    }
}