using Gameplay.Inputs;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.PlayerControl
{
    public class PlayerMouseLook : MonoBehaviour
    {
        [field: SerializeField] public PlayerController PlayerController { get; private set; }

        [SerializeField] private float maxAngle;
        [SerializeField] private float mouseSmooth = 0.03f;
        [SerializeField] private float mouseSensitivity = 3.5f;

        private float _cameraPitch;
        private Vector2 _currentMouseDelta = Vector2.zero;
        private Vector2 _currentMouseDeltaVelocity = Vector2.zero;

        private UserInput _userInput;

        private void Awake()
        {
            _userInput = new UserInput();
        }

        private void OnEnable()
        {
            _userInput.Enable();
        }

        private void Update()
        {
            MouseControls();
            MouseCameraRotation();
        }

        private void MouseControls()
        {
            transform.rotation = Quaternion.Euler(0f, PlayerController.PlayerCamera.transform.rotation.eulerAngles.y, 0f);
        }

        private void MouseCameraRotation()
        {
            Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = false;

            Vector2 targetMouseDelta = _userInput.Look.MouseDelta.ReadValue<Vector2>() * Time.smoothDeltaTime;

            _currentMouseDelta = Vector2.SmoothDamp(_currentMouseDelta, targetMouseDelta, ref _currentMouseDeltaVelocity, mouseSmooth);

            _cameraPitch -= _currentMouseDelta.y * mouseSensitivity;
            _cameraPitch = Mathf.Clamp(_cameraPitch, -40.0f, 40.0f);

            PlayerController.PlayerCamera.transform.localEulerAngles = Vector3.right * _cameraPitch;
            transform.Rotate(Vector3.up * _currentMouseDelta.x * mouseSensitivity);
        }
    }
}