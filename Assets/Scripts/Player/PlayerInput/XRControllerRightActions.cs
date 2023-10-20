using _Ambylon.PlayerInput;
using _Ambylon.Scripts.Objects.Interactable;
using UnityEngine;

namespace _Ambylon.PlayerController
{
    public sealed class XRControllerRightActions : MonoBehaviour
    {
        [SerializeField] private float rayDistance;
        [SerializeField] private Transform rayItem;

        private bool _isPerformed;
        
        private void Awake()
        {
            //_userInput.Actions.RaycastHitButton.performed += _ => _isPerformed = true;
        }

        private void OnEnable()
        {
            //_userInput.Enable();
        }

        private void Update()
        {
            RaycastHit hit;

            Vector3 forward = transform.TransformDirection(Vector3.forward) * rayDistance;
            Debug.DrawRay(rayItem.transform.position, rayItem.transform.forward, Color.red, rayDistance);

            if (Physics.Raycast(rayItem.transform.position, rayItem.transform.forward, out hit, rayDistance))
            {
                //if (hit.collider.gameObject.GetComponent<LoadSceneObject>() && _isPerformed)
                {
                    _isPerformed = false;
                  //  hit.collider.gameObject.GetComponent<LoadSceneObject>().Invoke("OnClickBehaviour", .1f);
                }
            }
            
            
        }

    }
}