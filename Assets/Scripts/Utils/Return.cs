using Gameplay.Pool;
using UnityEngine;

public class Return : MonoBehaviour
{
    private void OnEnable()
    {
        PoolManager.Instance.ReturnObject(this.gameObject, 1);
    }
}
