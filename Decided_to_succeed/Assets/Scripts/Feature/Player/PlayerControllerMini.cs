using Core.Logging;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Feature.Player
{
    public class PlayerControllerMini : MonoBehaviour, IPlayerControll
    {
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private bool _canControl = true;
        [SerializeField] private float jumpForce = 7f;
        private bool _isGrounded;
        private Vector2 _moveInput;
        private Rigidbody2D _rb;
        private SpriteRenderer _spriteRenderer;
        [SerializeField] private TextMeshProUGUI jumpText;
        private int _jumpCount = 0;

        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>(); 
        }
        void FixedUpdate()
        {
            if (!_canControl)
            {
                _rb.velocity = Vector2.zero;
                return;
            }
            _rb.velocity = new Vector2(_moveInput.x * moveSpeed, _rb.velocity.y);
        }


        public void OnMove(InputValue value)
        {
            _moveInput = value.Get<Vector2>();
            if (_moveInput.x != 0)
            {
                _spriteRenderer.flipX = _moveInput.x < 0;
            }

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _isGrounded = true;
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            _isGrounded = false;
        }

        public void OnJump(InputValue value)
        {
            if (!_isGrounded) return;
            _rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            _jumpCount++;
            jumpText.text = _jumpCount.ToString();

        }

        public void SetControllable(bool isControllable)
        {
        }
    }
}