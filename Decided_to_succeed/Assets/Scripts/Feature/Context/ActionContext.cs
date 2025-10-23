using System;
using Feature.CameraSystem;
using Feature.Character;
using Feature.Dialogue;
using Feature.Player;

namespace Feature.Context
{
    public class ActionContext
    {
        private DialogueManager _dialogueManager;
        private IPlayerControll _playerController;
        private Lothric _lothric;
        private CameraManager _cameraManager;

        public DialogueManager DialogueManager => _dialogueManager;
        public IPlayerControll PlayerController => _playerController;
        public Lothric Lothric => _lothric;
        public CameraManager CameraManager => _cameraManager;

        public ActionContext(IPlayerControll playerController, DialogueManager dialogueManager, CameraManager cameraManager, Lothric lothric = null)
        {
            _cameraManager = cameraManager;
            _playerController = playerController;
            _dialogueManager = dialogueManager;
            _lothric = lothric;
        }

        public ActionContext(IPlayerControll playerController, Lothric lothric = null)
        {
            _playerController = playerController;
        }
    }
}