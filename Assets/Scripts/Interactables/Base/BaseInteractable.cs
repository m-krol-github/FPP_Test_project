using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Gameplay.Interactables
{
    public abstract class BaseInteractable : MonoBehaviour
    {

        [Header("Destroable Object Pieces")]
        [SerializeField] protected Rigidbody[] itemPieces;
        [SerializeField] protected float knockForce;

        [Header("Destroable Object Time And Way of Dissapearing")]
        [SerializeField] protected float timeTo;
        [SerializeField] protected int hitPoints;
        [SerializeField] protected float gravityForce;

        [Header("Destroable Object Destroy Effects and Addons")]
        [SerializeField] protected UnityEvent hitEvent;
        [SerializeField] protected UnityEvent destroyEvent;
        [SerializeField] protected GameObject hitEffect;

        [Header("Destroable Object Components")]
        [SerializeField] protected Collider coll;
        [SerializeField] protected Slider bar;

    }
}