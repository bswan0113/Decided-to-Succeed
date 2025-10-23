using System.Collections.Generic;
using Cinemachine;
using Core.Dependency;
using Core.Logging;
using UnityEngine;

namespace Feature.CameraSystem
{
    public class CameraManager : MonoBehaviour
    {
        [System.Serializable]
        public class CameraEntry
        {
            public string id;
            public CinemachineVirtualCamera virtualCamera;
        }

        [SerializeField] private List<CameraEntry> _cameraEntries;
        private Dictionary<string, CinemachineVirtualCamera> _cameras = new Dictionary<string, CinemachineVirtualCamera>();

        private CinemachineBrain _cinemachineBrain;

        void Awake()
        {
            ServiceLocator.RegisterScene(this);
            foreach (var entry in _cameraEntries)
            {
                if (!_cameras.ContainsKey(entry.id) && entry.virtualCamera != null)
                {
                    _cameras.Add(entry.id, entry.virtualCamera);
                    entry.virtualCamera.Priority = 0;
                }
            }
            _cinemachineBrain = UnityEngine.Camera.main.GetComponent<CinemachineBrain>();
            if (_cinemachineBrain == null)
            {
                CLogger.LogError("[CameraManager] CinemachineBrain not found");
            }
        }

        public void SwitchToCamera(string id)
        {
            CLogger.Log("[CameraManager] SwitchToCamera " + id);
            if (!_cameras.ContainsKey(id))
            {
                CLogger.LogWarning($"[CameraManager] '{id}' Not found!");
                return;
            }

            foreach (var cam in _cameras.Values)
            {
                cam.Priority = 0;
            }
            _cameras[id].Priority = 100;
            CLogger.Log($"[CameraManager] Switched to camera: {id}");
        }

        public CinemachineBrain GetBrain()
        {
            return _cinemachineBrain;
        }
    }
}