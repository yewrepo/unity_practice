using UnityEngine;

namespace PixelCrew
{
    public class Hero : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float jumpSpeed;
        [SerializeField] private LayerCheck layerCheck;

        private Rigidbody2D _body;
        private Vector2 _direction;
        private Animator _animator;
        private static readonly int velocityProp = Animator.StringToHash("vertical-velocity");
        private static readonly int isRunningProp = Animator.StringToHash("is-running");
        private static readonly int isGroundProp = Animator.StringToHash("is-ground");

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        private void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            _body.velocity = new Vector2(_direction.x * speed, _body.velocity.y);

            var isGrounded = IsGrounded();
            var isJumping = _direction.y > 0;
            if (isJumping)
            {
                if (IsGrounded() && _body.velocity.y <= 0)
                {
                    _body.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
                }
            }
            else if (_body.velocity.y > 0)
            {
                _body.velocity = new Vector2(_body.velocity.x, _body.velocity.y * 0.5f);
            }

            _animator.SetFloat(velocityProp, _body.velocity.y);
            _animator.SetBool(isRunningProp, _direction.x != 0);
            _animator.SetBool(isGroundProp, isGrounded);
        }

        private bool IsGrounded()
        {
            return layerCheck.isTouchingLayer;
        }

        public void SaySomething()
        {
            Debug.Log("Fire blyat");
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = IsGrounded() ? Color.green : Color.red;
            Gizmos.DrawSphere(transform.position, 0.3f);
        }
    }
}