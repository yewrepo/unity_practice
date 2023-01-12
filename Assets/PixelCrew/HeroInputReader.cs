using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PixelCrew
{
    public class HeroInputReader : MonoBehaviour
    {
        [SerializeField] private Hero _hero;
        private HeroInputActions _inputActions;

        private void Awake()
        {
            _hero = GetComponent<Hero>();
            _inputActions = new HeroInputActions();

            _inputActions.Hero.movement.performed += OnHorizontalMovement;
            _inputActions.Hero.movement.canceled += OnHorizontalMovement;

            _inputActions.Hero.OnSaySomething.performed += OnSaySomething;
        }

        private void OnEnable()
        {
            _inputActions.Enable();
        }

        private void OnHorizontalMovement(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<Vector2>();
            _hero.SetDirection(direction);
        }

        private void OnSaySomething(InputAction.CallbackContext context)
        {
            _hero.SaySomething();
        }
    }
}