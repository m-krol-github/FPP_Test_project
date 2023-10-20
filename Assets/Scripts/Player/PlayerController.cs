using _Ambylon.PlayerInput;
using _Ambylon.Scripts.Player.OLD;
using TMPro;
using UnityEngine;


namespace _Ambylon.PlayerController
{
    public sealed class PlayerController : MonoBehaviour
    {
        [Header("Player components accessible references")] 
        [SerializeField] private Camera playerCamera;
        public Camera PlayerCamera => playerCamera;

        [SerializeField] private LocalPlayerControl localPlayerControl;
        public LocalPlayerControl LocalPlayerControl => localPlayerControl;
        
        [field: Header("Camera Rotation Limit")]
        [field: SerializeField] public bool IsNormalControls { get; private set; }
        [field: SerializeField] public bool IsLockedRotation { get; private set; }

        [Header("Player Move Values"), Space]
        [SerializeField] private float moveSpeed;
        
        [SerializeField] private float jumpForce;
        [SerializeField] private float gravityForce;
        
        [Header("Player References")]
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Animator playerArmatureAnimator;

        [Header("Player References")]
        [SerializeField] private Transform playerLeftHand;
        [SerializeField] private Transform playerRightHand;
        
        private float _rotateForce;
        private Vector3 _moveDirection;
        
        private XRIDefaultInputActions _userInput;
        
        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;

            localPlayerControl = GetComponentInParent<LocalPlayerControl>();    
            localPlayerControl.gameObject.AddComponent<Rigidbody>();
            localPlayerControl.gameObject.AddComponent<CharacterController>();

            rb = GetComponentInParent<Rigidbody>();
            playerArmatureAnimator = GetComponentInChildren<Animator>();
            
            _userInput = new XRIDefaultInputActions();
            
            
            
            _userInput.Actions.Jump.performed += _ => JumpPlayer();
            _userInput.Actions.SwitchControls.canceled += _ => ViewGameWithoutVRToggle();
            _userInput.Actions.LockRotation.performed += _ => ToggleLockRotation();
            
            NonVRPlayerStart();

        }
        
        private void OnEnable()
        {
            _userInput.Enable();
        }


        private void NonVRPlayerStart()
        {
            CharacterController characterController = GetComponentInParent<CharacterController>();
            characterController.GetComponent<Collider>().enabled = false;
            
            playerCamera.transform.localPosition = new Vector3(0, 1.8f, 0);
            rb.freezeRotation = true;
        }

        private void Update()
        {
            MovePlayerXR();

            if (!IsNormalControls)
                return;
            
            MovePlayer();
            rb.freezeRotation = true;
        }

        private void MovePlayerXR()
        {
            float moveXRx = _userInput.XRILeftHandLocomotion.Move.ReadValue<Vector2>().x;
            float moveXRy = _userInput.XRILeftHandLocomotion.Move.ReadValue<Vector2>().y;
            
            playerArmatureAnimator.SetFloat("Speed", (Mathf.Abs(moveXRx) + Mathf.Abs(moveXRy)) * 2.5f);
        }

        private void JumpPlayer()
        {
            playerArmatureAnimator.SetTrigger("StartJump");
            rb.AddForce(Vector3.up * jumpForce * Time.deltaTime, ForceMode.Impulse) ;
        }

        private void MovePlayer()
        {
            float yValue = _moveDirection.y;
            float xValue = _userInput.Keyboard.Move.ReadValue<Vector2>().x;
            float zValue = _userInput.Keyboard.Move.ReadValue<Vector2>().y;

            _moveDirection = (transform.forward * zValue) + (transform.right * xValue);
            _moveDirection = _moveDirection * moveSpeed;
            _moveDirection.y = yValue;

            _moveDirection.y = Physics.gravity.y * Time.deltaTime * gravityForce;

            rb.velocity = _moveDirection;
            
            playerArmatureAnimator.SetFloat("Speed", (Mathf.Abs(_moveDirection.x) + Mathf.Abs(_moveDirection.z)) * 2f);
        }

        private void ToggleLockRotation()
        {
            IsLockedRotation = !IsLockedRotation;
        }

        private void ViewGameWithoutVRToggle()
        {
            IsNormalControls = !IsNormalControls;

            if (IsNormalControls)
            {
                playerCamera.transform.localPosition = new Vector3(0, 1.8f, 0);
                jumpForce = 250f;
                playerLeftHand.gameObject.SetActive(false);
                playerRightHand.gameObject.SetActive(false);
            }
            else if (!IsNormalControls)
            {
                jumpForce = 250f;
                playerLeftHand.gameObject.SetActive(true);
                playerRightHand.gameObject.SetActive(true);
            }
        }
        private void OnDisable()
        {
            _userInput.Disable();
        }
    }
}