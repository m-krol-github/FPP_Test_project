using _Ambylon.PlayerInput;
using _Ambylon.Scripts.Objects.Interactable;
using UnityEngine;

namespace _Ambylon.PlayerController
{
    public sealed class CameraRaycast : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private float rayDistance = 2f;
        [SerializeField] private Camera cam;
        
        private XRIDefaultInputActions _userInput;

        private bool _isPerformed;

        private void Awake()
        {
            _userInput = new XRIDefaultInputActions();
            _userInput.Actions.RaycastHitButton.performed += _ => _isPerformed = true;
            
            _isPerformed = false;
            cam = Camera.main;
        }

        private void OnEnable()
        {
            _userInput.Enable();
        }

        private void Update()
        {
            if(!playerController.IsNormalControls)
                return;
            
            Vector3 forward = transform.TransformDirection(Vector3.forward) * rayDistance;
            Debug.DrawRay(transform.position, forward, Color.green);

            RaycastHit[] hits;

            hits = Physics.RaycastAll(cam.transform.position, cam.transform.forward, rayDistance);

            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];
                //if (hit.collider.gameObject.GetComponent<LoadSceneObject>() && _isPerformed)
                {
                    _isPerformed = false;
                  //  hit.collider.gameObject.GetComponent<LoadSceneObject>().Invoke("OnClickBehaviour", .1f);
                }
            }
        }
        
        private void OnDisable()
        {
            _userInput.Disable();
        }
    }
}