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
        private Rigidbody2D rb;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
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

            if (_isInitialized)
            {
                Vector3 movement = _target.position - _lastTargetPosition;
                if (movement.magnitude > 0.01f)
                {
                    _playerMoveDirection = movement.normalized;
                }
                _lastTargetPosition = _target.position;
            }
            Vector3 desiredPosition = _target.position - _playerMoveDirection * _followDistance;
            Vector3 newPosition = Vector3.MoveTowards(rb.position, desiredPosition, _moveSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPosition);
        }

        public void SetFollowing(bool follow)
        {
            _isFollowing = follow;
        }
    }
}