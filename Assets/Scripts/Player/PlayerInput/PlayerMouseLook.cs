using _Ambylon.PlayerInput;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Ambylon.PlayerController
{

    public sealed class PlayerMouseLook : MonoBehaviour
    {
        [field: SerializeField] public PlayerController PlayerController { get; private set; }

        [SerializeField] private float maxAngle;
        [SerializeField] private float mouseSmooth = 0.03f;
        [SerializeField] private float mouseSensitivity = 3.5f;

        private float _cameraPitch;
        private Vector2 _currentMouseDelta = Vector2.zero;
        private Vector2 _currentMouseDeltaVelocity = Vector2.zero;

        private XRIDefaultInputActions _userInput;

        private void Awake()
        {
            _userInput = new XRIDefaultInputActions();
            var pos = transform.position;
        }

        private void OnEnable()
        {
            _userInput.Enable();
        }

        private void Update()
        {
            if (!PlayerController.IsNormalControls)
                return;

            MouseControls();

            if (!PlayerController.IsLockedRotation)
            {
                MouseCameraRotation();
            }
            else
            {
                MouseLockRotation();
            }
        }

        private void MouseControls()
        {
            transform.rotation = Quaternion.Euler(0f, PlayerController.PlayerCamera.transform.rotation.eulerAngles.y, 0f);
        }

        private void MouseCameraRotation()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
            Vector2 targetMouseDelta = _userInput.Mouse.MouseDelta.ReadValue<Vector2>() * Time.smoothDeltaTime;
            
            _currentMouseDelta = Vector2.SmoothDamp(_currentMouseDelta, targetMouseDelta, ref _currentMouseDeltaVelocity, mouseSmooth);

            _cameraPitch -= _currentMouseDelta.y * mouseSensitivity;
            _cameraPitch = Mathf.Clamp(_cameraPitch, -90.0f, 90.0f);
            PlayerController.PlayerCamera.transform.localEulerAngles = Vector3.right * _cameraPitch;

            transform.Rotate(Vector3.up * _currentMouseDelta.x * mouseSensitivity);
            
            CheckRotationAngle();
        }

        private void MouseLockRotation()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;    
        }
        
        private void CheckRotationAngle()
        {
            Vector3 playerEuelerAngles = PlayerController.PlayerCamera.transform.rotation.eulerAngles;

            playerEuelerAngles.x = (playerEuelerAngles.x > 180) ? playerEuelerAngles.x - 360 : playerEuelerAngles.x;
            playerEuelerAngles.x = Mathf.Clamp(playerEuelerAngles.x, -maxAngle, maxAngle);

            playerEuelerAngles.z = 0;

            PlayerController.PlayerCamera.transform.rotation = Quaternion.Euler(playerEuelerAngles);
        }
    }
}