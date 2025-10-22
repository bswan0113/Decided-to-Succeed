using Feature.Dialogue;
using Feature.NPC;
using Feature.Player;

namespace Feature.Context
{
    public class ActionContext
    {
        private DialogueManager _dialogueManager;
        private PlayerController _playerController;
        private Lothric _lothric;

        public DialogueManager DialogueManager => _dialogueManager;
        public PlayerController PlayerController => _playerController;
        public Lothric Lothric => _lothric;

        public ActionContext(PlayerController playerController, DialogueManager dialogueManager,  Lothric lothric = null)
        {
            _playerController = playerController;
            _dialogueManager = dialogueManager;
            _lothric = lothric;
        }
    }
}