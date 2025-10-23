using System.Linq;
using Core.Logging;
using Feature.Context;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Feature.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField]private float interactionDistance = 1.5f;
        [SerializeField] private bool _canControl = true;

        private Vector2 _moveInput;
        private Rigidbody2D rb;
        private bool _canInteract = true;
        private Vector2 _lastNonZeroMoveDirection = Vector2.down;
        [Range(-1f, 1f)]
        private float interactionDotProductThreshold = 0.5f;

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

        /// <summary>
        /// 주변 상호작용 오브젝트를 찾고 상호작용을 시도합니다.
        /// </summary>
        private void TryInteract()
        {
            if (!_canInteract)
            {
                CLogger.LogDebug("Interaction currently blocked. (_canInteract is false)", this);
                return;
            }

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactionDistance);

            var nearestInteractable = colliders
                .Select(c => c.GetComponent<IInteractable>())
                .Where(i => i != null)
                .OfType<MonoBehaviour>()
                .Where(m => {
                    Vector2 directionToObject = ((Vector2)m.transform.position - (Vector2)transform.position).normalized;
                    float dotProduct = Vector2.Dot(_lastNonZeroMoveDirection, directionToObject);
                    return dotProduct > interactionDotProductThreshold;
                })
                .OrderBy(m => Vector2.Distance(transform.position, m.transform.position))
                .Select(m => m.GetComponent<IInteractable>())
                .FirstOrDefault();

            if (nearestInteractable != null)
            {
                CLogger.LogDebug($"<color=green>Nearest interactable found: {(nearestInteractable as MonoBehaviour)?.gameObject.name}. Calling Interact()...</color>", this);
                nearestInteractable.Interact(new ActionContext(this));
            }
            else
            {
                CLogger.LogDebug("There is no InteractableObject.", this);
            }
        }

        private void OnInteraction()
        {
            TryInteract();
        }

    }
}