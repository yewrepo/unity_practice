using System;
using TMPro.EditorUtilities;
using UnityEngine;

namespace PixelCrew
{
    public class Hero : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float jumpSpeed;
        [SerializeField] private LayerCheck layerCheck;

        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _body;
        private Vector2 _direction;
        private Animator _animator;
        private bool _isGrounded;
        private bool _allowDoubleJump;

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
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            var xVelocity = _direction.x * speed;
            var yVelocity = CalculateYVelocity();
            _body.velocity = new Vector2(xVelocity, yVelocity);

            _animator.SetFloat(velocityProp, _body.velocity.y);
            _animator.SetBool(isRunningProp, _direction.x != 0);
            _animator.SetBool(isGroundProp, _isGrounded);

            UpdateSpriteDirection();
        }

        private void Update()
        {
            _isGrounded = IsGrounded();
        }

        private float CalculateYVelocity()
        {
            var yVelocity = _body.velocity.y;
            var isJumpPressed = _direction.y > 0;

            if (_isGrounded)
            {
                _allowDoubleJump = true;
            }

            if (isJumpPressed)
            {
                yVelocity = CalculateJumpVelocity(yVelocity);
            }
            else if (_body.velocity.y > 0)
            {
                yVelocity *= 0.5f;
            }

            return yVelocity;
        }

        private float CalculateJumpVelocity(float yVelocity)
        {
            var isFalling = _body.velocity.y <= 0.001f;
            if (!isFalling)
            {
                return yVelocity;
            }

            if (_isGrounded)
            {
                yVelocity += jumpSpeed;
            }
            else if (_allowDoubleJump)
            {
                _allowDoubleJump = false;
                yVelocity = jumpSpeed;
            }

            return yVelocity;
        }

        private void UpdateSpriteDirection()
        {
            if (_direction.x > 0)
            {
                _spriteRenderer.flipX = false;
            }
            else if (_direction.x < 0)
            {
                _spriteRenderer.flipX = true;
            }
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