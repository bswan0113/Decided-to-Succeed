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
        private Rigidbody2D rb;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        void FixedUpdate()
        {
            if (!_canControl)
            {
                rb.velocity = Vector2.zero;
                return;
            }
            rb.velocity = _moveInput * moveSpeed;
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
                rb.velocity = Vector2.zero;
            }
        }

    }
}