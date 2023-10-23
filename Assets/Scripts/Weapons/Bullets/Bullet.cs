using Gameplay.Pool;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Weapons
{
    public class Bullet : GenericBullet, IReturnToPool
    {
        public void ReturnToPool(GameObject Item)
        {
            PoolManager.Instance.ReturnObject(Item, 1);
        }

        protected override void OnCollisionEnter(Collision collision)
        {
            base.OnCollisionEnter(collision);

            ReturnToPool(this.gameObject);
        }
    }
}