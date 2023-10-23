using TMPro;

using UnityEngine;

namespace Gameplay.Weapons
{
    public abstract class GenericBullet : MonoBehaviour
    {
        [Header("Base References and varaibles")]
        [SerializeField] protected Rigidbody rb;
        [SerializeField] protected Collider coll;

        [SerializeField] protected LayerMask avoidLayer;

        [SerializeField] protected bool useCollision;

        protected virtual void OnCollisionEnter(Collision collision)
        {
            if (!useCollision)
                return;
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (useCollision)
                return;
        }
    }
}