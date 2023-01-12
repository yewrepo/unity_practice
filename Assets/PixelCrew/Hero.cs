using UnityEngine;

namespace PixelCrew
{
    public class Hero : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float jumpSpeed;
        [SerializeField] private LayerCheck LayerCheck;

        private Rigidbody2D _body;
        private Vector2 _direction;

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        private void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _body.velocity = new Vector2(_direction.x * speed, _body.velocity.y);

            var isJumping = _direction.y > 0;
            if (isJumping)
            {
                if (IsGrounded())
                {
                    _body.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
                }
            }
            else if (_body.velocity.y > 0)
            {
                _body.velocity = new Vector2(_body.velocity.x, _body.velocity.y * 0.5f);
            }
        }

        private bool IsGrounded()
        {
            return LayerCheck.isTouchingLayer;
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