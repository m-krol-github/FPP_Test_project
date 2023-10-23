using Gameplay.Inputs;
using Gameplay.Pool;
using Gameplay.Scripts.Player.OLD;

using System;
using System.Collections;

using TMPro;
using UnityEngine;


namespace Gameplay.PlayerControl
{
    public sealed class PlayerController : MonoBehaviour
    {
        [Header("Player components accessible references")] 
        [SerializeField] private Camera playerCamera;
        public Camera PlayerCamera => playerCamera;

        [SerializeField] private PlayerWeaponShooting weaponShooting;
        public PlayerWeaponShooting WeaponShooting => weaponShooting;
        
        [field: Header("Camera Rotation Limit")]
        [field: SerializeField] public bool IsLockedRotation { get; private set; }

        [Header("Player Move Values"), Space]
        [SerializeField] private float moveSpeed;
        
        [SerializeField] private float jumpSpeed;
        [SerializeField] private float gravityForce;
        
        [Header("Player References")]
        [SerializeField] private Animator playerArmatureAnimator;
        [SerializeField] private CharacterController charController;

        private Vector3 _moveDirection;

        private float default_ControllerHeight;
        private bool is_Crouching;
        private Vector3 default_CamPos;
        private float camHeight;
        private Transform firstPerson_View;

        private UserInput _userInput;
        
        private void Awake()
        {
            _userInput = new UserInput();
            
            _userInput.Actions.PrimaryAction.canceled += _ => PlayerShoot();
            _userInput.Actions.SecondaryAction.canceled += _ => PlayerReloadWeapon();
        }
        
        private void OnEnable()
        {
            _userInput.Enable();
        }

        private void Update()
        {
            MovePlayer();
        }

        private void MovePlayer()
        {
            float yValue = _moveDirection.y;
            float xValue = _userInput.Translate.Move.ReadValue<Vector2>().x;
            float zValue = _userInput.Translate.Move.ReadValue<Vector2>().y;

            _moveDirection = (transform.forward * zValue) + (transform.right * xValue);
            _moveDirection = _moveDirection * moveSpeed;
            _moveDirection.y = yValue;

            _moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityForce;

            if (_userInput.Actions.Jump.IsPressed() && charController.isGrounded)
            {
                 _moveDirection.y = jumpSpeed * Time.deltaTime;
            }

            charController.Move(_moveDirection);
        }

        private void PlayerShoot()
        {
            weaponShooting.ShootWeapon();
        }

        private void PlayerReloadWeapon()
        {
            weaponShooting.ReloadWeapon();
        }

        private void OnDisable()
        {
            _userInput.Disable();
        }
    }
}