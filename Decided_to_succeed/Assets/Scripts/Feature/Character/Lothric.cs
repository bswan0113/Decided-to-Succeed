using UnityEngine;

namespace Feature.NPC
{
    public class Lothric : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _followDistance = 0.3f;
        [SerializeField] private float _moveSpeed = 3f;
        [SerializeField] private bool _isFollowing = false;

        private Vector3 _lastTargetPosition;
        private Vector3 _playerMoveDirection;
        private bool _isInitialized = false;

        void Start()
        {
            if (_target != null)
            {
                _lastTargetPosition = _target.position;
                _isInitialized = true;
            }
        }

        void LateUpdate()
        {
            if (!_isFollowing || _target == null) return;

            // 플레이어 이동 방향 감지
            if (_isInitialized)
            {
                Vector3 movement = _target.position - _lastTargetPosition;
                if (movement.magnitude > 0.01f) // 충분히 움직였을 때만 방향 업데이트
                {
                    _playerMoveDirection = movement.normalized;
                }
                _lastTargetPosition = _target.position;
            }

            // 플레이어 이동 방향의 반대쪽에 위치하도록 계산
            Vector3 desiredPosition = _target.position - _playerMoveDirection * _followDistance;
            Debug.DrawLine(_target.position, _target.position + _playerMoveDirection, Color.red, 0.1f);

            // 부드럽게 이동
            transform.position = Vector3.MoveTowards(transform.position, desiredPosition, _moveSpeed * Time.deltaTime);
        }

        public void SetFollowing(bool follow)
        {
            _isFollowing = follow;
        }
    }
}