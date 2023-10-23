using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Utils
{
    public class DestroyAfter : MonoBehaviour
    {
        [SerializeField] private float timeTo = 2f;

        private void OnEnable()
        {
            StartCoroutine(DestroyItem(timeTo));
        }

        private IEnumerator DestroyItem(float t) 
        {
            yield return new WaitForSeconds(t);
            Destroy(this.gameObject);
        }
    }
}


public enum DestroyType
{
    Timed,
    ManyHits
}