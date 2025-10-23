// C:\Workspace\Tomorrow Never Comes\Assets\Scripts\Core\SceneTransitionView.cs (REVISED)

using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class SceneTransitionView : MonoBehaviour
    {
        [SerializeField] private Image fadeImage;

        public Image FadeImage => fadeImage;

        private void Awake()
        {
            var rootCanvas = GetComponentInParent<Canvas>(true)?.transform.root;
            if (rootCanvas != null)
            {
                DontDestroyOnLoad(rootCanvas.gameObject);
            }
            else
            {
                DontDestroyOnLoad(this.gameObject);
            }

            if (fadeImage == null)
            {
                fadeImage = GetComponent<Image>();
                Debug.LogError("SceneTransitionView: FadeImage is Not Assigned To Image.");
            }
        }
    }
}