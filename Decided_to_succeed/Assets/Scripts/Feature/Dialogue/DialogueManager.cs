// Feature/Dialogue/DialogueManager.cs
using Cysharp.Threading.Tasks;
using System.Threading;
using Feature.Common;
using TMPro;
using UnityEngine;

namespace Feature.Dialogue
{
    public class DialogueManager : MonoBehaviour, IAsyncManager
    {
        [SerializeField] private GameObject _speechBubblePrefab;
        private GameObject _currentBubble;

        void Start()
        {
            InitializeManager();
        }

        void OnDestroy()
        {
            ShutdownManager();
        }

        public void InitializeManager()
        {
            DontDestroyOnLoad(this);
        }

        public void ShutdownManager()
        {
            HideDialogue();
        }

        public async UniTask ShowDialogue(string text, Transform target, float duration, Vector3 offset, CancellationToken token)
        {
            HideDialogue();
            _currentBubble = Instantiate(_speechBubblePrefab, target);
            _currentBubble.transform.localPosition = offset;
            var textComponent = _currentBubble.GetComponentInChildren<TextMeshPro>();
            if (textComponent != null)
            {
                textComponent.text = text;
            }

            await UniTask.Delay((int)(duration * 1000), cancellationToken: token);

            HideDialogue();
        }

        public void HideDialogue()
        {
            if (_currentBubble != null)
            {
                Destroy(_currentBubble);
                _currentBubble = null;
            }
        }
    }
}