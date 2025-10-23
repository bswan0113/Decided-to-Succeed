using System;
using System.Threading;
using Core.Logging;
using Cysharp.Threading.Tasks;
using Feature.Context;
using UnityEngine;
using UnityEngine.Video;

namespace ScriptableObjects.Action
{
    [Serializable]
    public class PlayVideoAction : IAction
    {
        [SerializeField] private string _videoPlayerObjectName;
        public async UniTask ExecuteAsync(ActionContext context, CancellationToken token)
        {
            GameObject videoPlayerGameObject = GameObject.Find(_videoPlayerObjectName);
            VideoPlayer videoPlayer = videoPlayerGameObject.GetComponent<VideoPlayer>();
            videoPlayer.gameObject.SetActive(true);
            videoPlayer.Play();
            await UniTask.WaitUntil(() => videoPlayer.isPrepared, cancellationToken: token);
            await UniTask.WaitWhile(() => videoPlayer.isPlaying, cancellationToken: token);
        }
    }
}