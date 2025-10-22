// PlayerController.cs

using UnityEngine;
using UnityEngine.InputSystem;

namespace Feature.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 3f;
        private Vector2 _moveInput;
        [SerializeField] private bool _canControl = true;

        void Update()
        {
            if (!_canControl) return;
            transform.position += new Vector3(_moveInput.x, _moveInput.y, 0) * moveSpeed * Time.deltaTime;
        }

        public void OnMove(InputValue value)
        {
            _moveInput = value.Get<Vector2>();
        }

        public void SetControllable(bool isControllable)
        {
            _canControl = isControllable;
            if (!isControllable)
            {
                _moveInput = Vector2.zero;
            }
        }

    }
}