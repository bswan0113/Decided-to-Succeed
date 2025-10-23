using Feature.CameraSystem;
using Feature.Character;
using Feature.Dialogue;
using Feature.Player;

namespace Feature.Context
{
    public class ActionContext
    {
        private DialogueManager _dialogueManager;
        private PlayerController _playerController;
        private Lothric _lothric;
        private CameraManager _cameraManager;

        public DialogueManager DialogueManager => _dialogueManager;
        public PlayerController PlayerController => _playerController;
        public Lothric Lothric => _lothric;
        public CameraManager CameraManager => _cameraManager;

        public ActionContext(PlayerController playerController, DialogueManager dialogueManager, CameraManager cameraManager, Lothric lothric = null)
        {
            _cameraManager = cameraManager;
            _playerController = playerController;
            _dialogueManager = dialogueManager;
            _lothric = lothric;
        }
    }
}