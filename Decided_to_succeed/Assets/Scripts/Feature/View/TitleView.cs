using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Feature.Title
{
    public class TitleView : MonoBehaviour
    {
        private TextMeshProUGUI _pressAnyKeyText;

        private void Awake()
        {
            _pressAnyKeyText = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            BlinkLoop().Forget();
        }

        private async UniTaskVoid BlinkLoop()
        {
            var token = this.GetCancellationTokenOnDestroy();

            float fadeDuration = 1.0f;
            Color color = _pressAnyKeyText.color;

            try
            {
                while (!token.IsCancellationRequested)
                {
                    float elapsedTime = 0f;
                    while (elapsedTime < fadeDuration)
                    {
                        float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
                        _pressAnyKeyText.color = new Color(color.r, color.g, color.b, alpha);

                        elapsedTime += Time.deltaTime;
                        await UniTask.Yield(cancellationToken: token);
                    }
                    _pressAnyKeyText.color = new Color(color.r, color.g, color.b, 0f);

                    elapsedTime = 0f;
                    while (elapsedTime < fadeDuration)
                    {
                        float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
                        _pressAnyKeyText.color = new Color(color.r, color.g, color.b, alpha);

                        elapsedTime += Time.deltaTime;
                        await UniTask.Yield(cancellationToken: token);
                    }
                    _pressAnyKeyText.color = new Color(color.r, color.g, color.b, 1f);
                }
            }
            catch (OperationCanceledException) { }
        }
    }
}