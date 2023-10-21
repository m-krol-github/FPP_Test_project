using Gameplay.Inputs;
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

        [SerializeField] private LocalPlayerControl localPlayerControl;
        public LocalPlayerControl LocalPlayerControl => localPlayerControl;

        [SerializeField] private PlayerWeaponShooting weaponShooting;
        public PlayerWeaponShooting WeaponShooting => weaponShooting;
        
        [field: Header("Camera Rotation Limit")]
        [field: SerializeField] public bool IsLockedRotation { get; private set; }

        [Header("Player Move Values"), Space]
        [SerializeField] private float moveSpeed;
        
        [SerializeField] private float jumpSpeed;
        [SerializeField] private float gravityForce;
        
        [Header("Player References")]
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Animator playerArmatureAnimator;
        [SerializeField] private CharacterController charController;
        [SerializeField] private bool isGrounded;

        private float _rotateForce;
        private Vector3 _moveDirection;
        
        private UserInput _userInput;
        
        private void Awake()
        {
            _userInput = new UserInput();
            
            _userInput.Actions.PrimaryAction.canceled += _ => PlayerShoot();
            _userInput.Actions.SecondaryAction.canceled += _ => PlayerReloadWeapon();

            PlayerStart();
        }
        
        private void OnEnable()
        {
            _userInput.Enable();
        }

        private void PlayerStart()
        {
            //playerCamera.transform.localPosition = new Vector3(0, 1.8f, 0.6f);
        }

        private void Update()
        {
            isGrounded = charController.isGrounded;
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

            //rb.velocity = _moveDirection;

            //playerArmatureAnimator.SetFloat("Move", Mathf.Abs(xValue) + Mathf.Abs(zValue));

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

        private IEnumerator JumpFalling()
        {
            yield return new WaitForSeconds(jumpSpeed);
            playerArmatureAnimator.SetFloat("VelocityY", 0f);
        }


        private float default_ControllerHeight;
        private bool is_Crouching;
        private Vector3 default_CamPos;
        private float camHeight;
        private Transform firstPerson_View;

        private IEnumerator MoveCameraCrouch()
        {
            charController.height = is_Crouching ? default_ControllerHeight / 1.5f : default_ControllerHeight;
            charController.center = new Vector3(0f, charController.height / 2f, 0f);

            camHeight = is_Crouching ? default_CamPos.y / 1.5f : default_CamPos.y;

            while (Mathf.Abs(camHeight - firstPerson_View.localPosition.y) > 0.01f)
            {
                firstPerson_View.localPosition = Vector3.Lerp(firstPerson_View.localPosition, new Vector3(default_CamPos.x, camHeight, default_CamPos.z), Time.deltaTime * 11f);

                yield return null;
            }
        }

        private void OnDisable()
        {
            _userInput.Disable();
        }
    }
}