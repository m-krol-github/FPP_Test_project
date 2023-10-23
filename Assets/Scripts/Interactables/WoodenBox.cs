using Gameplay.Pool;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Interactables
{
    public class WoodenBox : BaseInteractable
    {
        protected void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Bullets"))
            {
                coll.enabled = false;

                ContactPoint contact = collision.contacts[0];
                Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
                Vector3 pos = contact.point;
                PoolManager.Instance.UseObject(hitEffect, pos, rot);
                hitPoints--;

                hitEvent.Invoke();

                bar.value = hitPoints;

                if (hitPoints == 0)
                    StartCoroutine(FinalDestroy());
            }
        }

        protected void DestroyItem()
        {
            destroyEvent.Invoke();
        }

        protected IEnumerator FinalDestroy()
        {
            DestroyItem();

            yield return new WaitForSeconds(timeTo);

            for (int i = 0; i < itemPieces.Length; i++)
            {
                itemPieces[i].isKinematic = false;
                itemPieces[i].AddForce(Vector3.up * knockForce);
            }
        }
    }
}